import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HeroComponent } from './hero/hero.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { CoursesComponent } from './courses/courses.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AccessCodeComponent } from './admin-dashboard/access-code/access-code.component';
import { UsersComponent } from './admin-dashboard/users/users.component';
import { AccessDeniedComponent } from './auth/access-denied/access-denied.component';
import {
  canActivateChildAdmin,
  canActivateSelectRole,
} from './auth/guards/auth.guard';
import { SelectRoleComponent } from './auth/select-role/select-role.component';
import { MyCoursesComponent } from './courses/my-courses/my-courses.component';
import { CourseDetailComponent } from './courses/my-courses/course-detail/course-detail.component';

const appRoutes: Routes = [
  {
    path: '',
    component: HeroComponent,
  },
  {
    path: 'courses',
    component: CoursesComponent,
  },
  {
    path: 'my-courses',
    component: MyCoursesComponent,
    children: [{ path: ':id', component: CourseDetailComponent }],
  },
  {
    path: 'sign-in',
    component: LoginComponent,
  },
  {
    path: 'sign-up',
    component: RegisterComponent,
  },

  {
    path: 'select-role',
    component: SelectRoleComponent,
    canActivate: [canActivateSelectRole],
  },
  {
    path: 'access-denied',
    component: AccessDeniedComponent,
  },
  {
    path: 'admin-dashboard',
    redirectTo: 'admin-dashboard/users',
    pathMatch: 'full',
  },
  {
    path: 'admin-dashboard',
    component: AdminDashboardComponent,
    canActivateChild: [canActivateChildAdmin],
    children: [
      { path: 'access-code', component: AccessCodeComponent },
      { path: 'users', component: UsersComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
