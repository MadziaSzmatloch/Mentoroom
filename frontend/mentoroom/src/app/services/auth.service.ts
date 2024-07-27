import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, pipe, take, tap, throwError } from 'rxjs';
import { User } from '../models/user.model';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import Tokens from '../interfaces/tokens.interface';
import UserData from '../interfaces/user-data.interface';

@Injectable({ providedIn: 'root' })
export class AuthService {
  user = new BehaviorSubject<User>(null);
  constructor(private httpClient: HttpClient, private router: Router) {}

  signUp(
    firstName: string,
    lastName: string,
    email: string,
    password: string,
    passwordConfirm: string
  ) {
    return this.httpClient
      .post<Tokens>(`${environment.apiUrl}auth/sign-up`, {
        fistName: firstName,
        lastName: lastName,
        email: email,
        password: password,
        passwordConfirm: passwordConfirm,
      })
      .pipe(
        catchError(this.handleError),
        tap((response) => {
          this.handleAuthentication(response);
          this.handleRedirecting();
        })
      );
  }

  signIn(email: string, password: string) {
    return this.httpClient
      .post<Tokens>(`${environment.apiUrl}auth/sign-in`, {
        email: email,
        password: password,
      })
      .pipe(
        catchError(this.handleError),
        tap((response) => {
          this.handleAuthentication(response);
          this.handleRedirecting();
        })
      );
  }

  logout() {
    return this.httpClient.post(`${environment.apiUrl}auth/logout`, {}).pipe(
      tap(() => {
        this.user.next(null);
        localStorage.removeItem('tokens');
      })
    );
  }

  refreshTokens() {
    const tokens = this.user.value.tokens;
    return this.httpClient
      .post<Tokens>(`${environment.apiUrl}auth/refresh`, { ...tokens })
      .pipe(
        catchError(this.handleError),
        tap((response) => this.handleAuthentication(response))
      );
  }

  promoteToLecturerWithAccessCode(accessCode: string) {
    const params = new HttpParams().set('accessCode', accessCode);

    return this.httpClient
      .post<UserData>(
        `${environment.apiUrl}auth/promote-to-lecturer-with-access-code`,
        null,
        { params }
      )
      .pipe(take(1))
      .pipe(catchError(this.handleError));
  }

  promoteToAdmin(userId: string) {
    const params = new HttpParams().set('userId', userId);

    return this.httpClient
      .post<UserData>(`${environment.apiUrl}auth/promote-to-admin`, null, {
        params,
      })
      .pipe(take(1))
      .pipe(catchError(this.handleError));
  }

  promoteToLecturer(userId: string) {
    const params = new HttpParams().set('userId', userId);

    return this.httpClient
      .post<UserData>(`${environment.apiUrl}auth/promote-to-lecturer`, null, {
        params,
      })
      .pipe(take(1))
      .pipe(catchError(this.handleError));
  }

  promoteToStudent(userId: string, studentIndex: string) {
    const params = new HttpParams()
      .set('userId', userId)
      .set('studentIndex', studentIndex);

    return this.httpClient
      .post<UserData>(`${environment.apiUrl}auth/promote-to-student`, null, {
        params,
      })
      .pipe(take(1))
      .pipe(catchError(this.handleError));
  }

  autoLogin() {
    const tokens: Tokens = JSON.parse(localStorage.getItem('tokens'));
    if (!tokens) {
      return;
    }

    const loadedUser = new User(tokens);
    this.user.next(loadedUser);

    this.refreshTokens().subscribe({
      next: () => {
        if (this.IsGuest) {
          this.router.navigate(['/select-role']);
        }
      },
      error: () => {
        this.logout().subscribe();
      },
    });
  }

  isAuthenticated() {
    return this.user.value !== null;
  }

  get accessTokens(): Tokens {
    return this.user.value.tokens;
  }

  get userID() {
    return this.user.value?.userData?.Id;
  }

  get IsGuest() {
    return this.user.value?.userData?.Role === null;
  }

  get isAdmin() {
    return this.user.value?.userData?.Role === 'Admin';
  }

  get isStudent() {
    return this.user.value?.userData?.Role === 'Student';
  }

  get isLecturer() {
    return this.user.value?.userData?.Role === 'Lecturer';
  }

  private handleAuthentication(tokens: Tokens) {
    localStorage.removeItem('tokens');
    const user = new User(tokens);
    this.user.next(user);
    localStorage.setItem('tokens', JSON.stringify(user.tokens));
  }

  private handleRedirecting() {
    if (this.isAdmin) {
      this.router.navigate(['/admin-dashboard']);
    } else if (this.isStudent) {
      this.router.navigate(['/courses']);
    } else if (this.isLecturer) {
      this.router.navigate(['/my-courses']);
    } else {
      this.router.navigate(['/select-role']);
    }
  }

  private handleError(errorResponse: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';
    if (!errorResponse.error) {
      return throwError(() => errorMessage);
    }
    errorMessage = errorResponse.error;
    return throwError(() => errorMessage);
  }
}
