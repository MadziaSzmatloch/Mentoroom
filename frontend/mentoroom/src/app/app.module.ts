import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { HeroComponent } from './hero/hero.component';
import { HeaderComponent } from './header/header.component';

import { CoursesComponent } from './courses/courses.component';
import { CourseItemComponent } from './courses/my-courses/course-list/course-item/course-item.component';
import { AddCourseComponent } from './courses/add-course/add-course.component';

import { CourseSearcherService } from './services/course-searcher.service';

import { RegisterComponent } from './auth/register/register.component';
import { LoginComponent } from './auth/login/login.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AuthInterceptor } from './auth/auth-interceptor.service';

import { LoadingSpinnerComponent } from './shared/loading-spinner/loading-spinner.component';
import { PrimeNgModule } from './modules/prime-ng-module';
import { AccessCodeComponent } from './admin-dashboard/access-code/access-code.component';
import { UsersComponent } from './admin-dashboard/users/users.component';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { AccessDeniedComponent } from './auth/access-denied/access-denied.component';
import { SelectRoleComponent } from './auth/select-role/select-role.component';
import { AccessCodeHistoryComponent } from './admin-dashboard/access-code/access-code-history/access-code-history.component';
import { ConfirmationService } from 'primeng/api';
import { CourseService } from './services/course.service';
import { AddAssignmentComponent } from './courses/assignment/add-assignment/add-assignment.component';
import { StudentListComponent } from './courses/student-list/student-list.component';
import { MyCoursesComponent } from './courses/my-courses/my-courses.component';
import { CourseSearcherComponent } from './courses/my-courses/course-searcher/course-searcher.component';
import { CourseListComponent } from './courses/my-courses/course-list/course-list.component';
import { CourseDetailComponent } from './courses/my-courses/course-detail/course-detail.component';
import { AddRequiredFilesComponent } from './courses/assignment/add-required-files/add-required-files.component';
import { EditCourseComponent } from './courses/edit-course/edit-course.component';
@NgModule({
  declarations: [
    AppComponent,
    HeroComponent,
    HeaderComponent,
    CourseItemComponent,
    CourseListComponent,
    CourseDetailComponent,
    CourseSearcherComponent,
    RegisterComponent,
    LoginComponent,
    CoursesComponent,
    AddCourseComponent,
    LoadingSpinnerComponent,
    AdminDashboardComponent,
    AccessCodeComponent,
    UsersComponent,
    AccessDeniedComponent,
    SelectRoleComponent,
    AccessCodeHistoryComponent,
    AddAssignmentComponent,
    StudentListComponent,
    MyCoursesComponent,
    AddRequiredFilesComponent,
    EditCourseComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    PrimeNgModule,
    ClipboardModule,
  ],
  providers: [
    CourseSearcherService,
    ConfirmationService,
    CourseService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
