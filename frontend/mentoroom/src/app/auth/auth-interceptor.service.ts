import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpHeaders,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  Observable,
  catchError,
  exhaustMap,
  switchMap,
  take,
  throwError,
} from 'rxjs';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  counter = 0;

  constructor(private authService: AuthService, private router: Router) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return this.authService.user.pipe(
      take(1),
      exhaustMap((user) => {
        if (!user) {
          return next.handle(req);
        }
        const modifiedReq = req.clone({
          setHeaders: {
            Authorization: `Bearer ${this.authService.accessTokens.accessToken}`,
          },
        });
        return next.handle(modifiedReq);
      }),
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          return this.authService.refreshTokens().pipe(
            switchMap(() => {
              const modifiedReq = req.clone({
                setHeaders: {
                  Authorization: `Bearer ${this.authService.accessTokens.accessToken}`,
                },
              });
              return next.handle(modifiedReq);
            }),
            catchError((error) => {
              this.authService.logout().subscribe();
              this.router.navigate(['/sign-in']);
              return throwError(() => error);
            })
          );
        }
        return throwError(() => error);
      })
    );
  }
}
