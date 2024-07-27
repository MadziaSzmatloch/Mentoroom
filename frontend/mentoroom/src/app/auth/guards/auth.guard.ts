import { Injectable, inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateChildFn,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuardService {
  canActivate(authService: AuthService, allowedRoles: string[]): boolean {
    this.baseAuthentication(authService);
    const userRole = authService.user.value.getUserRole;
    if (userRole && allowedRoles.includes(userRole)) {
      return true;
    } else {
      this.redirectToAccessDenied();
      return false;
    }
  }

  canActivateSelectRole(authService: AuthService): boolean {
    if (!authService.isAuthenticated()) {
      inject(Router).navigate(['/sign-in']);
      return false;
    }
    if (authService.user.value.getUserRole !== null) {
      this.redirectToAccessDenied();
      return false;
    } else {
      return true;
    }
  }

  private baseAuthentication(authService: AuthService): boolean {
    if (!authService.isAuthenticated()) {
      inject(Router).navigate(['/sign-in']);
      return false;
    }

    if (!authService.user.value.getUserRole) {
      inject(Router).navigate(['/select-role']);
      return false;
    }
  }

  private redirectToAccessDenied(): void {
    inject(Router).navigate(['/access-denied']);
  }
}

export function createRoleGuard(
  allowedRoles: string[]
): CanActivateFn | CanActivateChildFn {
  return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
    return inject(AuthGuardService).canActivate(
      inject(AuthService),
      allowedRoles
    );
  };
}

export function createSelectRoleGuard(): CanActivateFn {
  return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
    return inject(AuthGuardService).canActivateSelectRole(inject(AuthService));
  };
}

export const canActivateAdmin = createRoleGuard(['Admin']);
export const canActivateChildAdmin = createRoleGuard(['Admin']);
export const canActivateLecturer = createRoleGuard(['Admin', 'Lecturer']);
export const canActivateChildLecturer = createRoleGuard(['Admin', 'Lecturer']);
export const canActivateStudent = createRoleGuard(['Admin', 'Student']);
export const canActivateChildStudent = createRoleGuard(['Admin', 'Student']);
export const canActivateSelectRole = createSelectRoleGuard();
