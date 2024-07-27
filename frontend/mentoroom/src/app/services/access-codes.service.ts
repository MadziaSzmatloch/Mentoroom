import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';
import { catchError, take, throwError } from 'rxjs';
interface AccessCode {
  id: string;
  code: string;
  creationDate: Date;
  expirationDate: Date;
  isActive: boolean;
}

@Injectable({ providedIn: 'root' })
export class AccessCodesService {
  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) {}

  getAccessCodes() {
    return this.httpClient
      .get<AccessCode[]>(`${environment.apiUrl}accesscode`)
      .pipe(catchError(this.handleError));
  }

  deactivateAccessCode(codeId: string) {
    return this.httpClient
      .delete<AccessCode>(`${environment.apiUrl}accesscode/${codeId}`)
      .pipe(catchError(this.handleError));
  }

  createAccessCode(expirationDate: Date) {
    return this.httpClient
      .post<AccessCode>(`${environment.apiUrl}accesscode`, {
        expirationDate: expirationDate.toISOString(),
      })
      .pipe(catchError(this.handleError));
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
