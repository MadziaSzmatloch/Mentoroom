import { Injectable } from '@angular/core';
import { Course } from '../models/course.model';
import { CourseFilter } from '../models/course-filter.model';
import {
  BehaviorSubject,
  Subject,
  catchError,
  map,
  tap,
  throwError,
} from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';

@Injectable()
export class CourseService {
  private coursesSubject = new BehaviorSubject<Course[]>([]);
  private courses: Course[];

  private selectedId: string | null;
  selectedIdChanged = new Subject<string>();

  setSelectedCourseIndex(id: string) {
    this.selectedId = id;
    this.selectedIdChanged.next(this.selectedId);
  }

  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) {
    this.authService.user.subscribe((user) => {
      if (user.getUserRole === 'Student')
        this.loadStudentCourses(user.userData.Id);
      else if (user.getUserRole === 'Lecturer')
        this.loadLecturerCourses(user.userData.Id);
    });
  }

  private loadStudentCourses(studentId: string) {
    this.httpClient
      .get<Course[]>(`${environment.apiUrl}studentcourse/students/${studentId}`)
      .pipe(
        catchError(this.handleError),
        tap((response: Course[]) => {
          this.courses = response.filter((x) => x.isConfirmed === true);
          this.coursesSubject.next(this.courses);
        })
      )
      .subscribe();
  }

  private loadLecturerCourses(lecturerId: string) {
    this.httpClient
      .get<Course[]>(`${environment.apiUrl}course/${lecturerId}`)
      .pipe(
        catchError(this.handleError),
        tap((response: Course[]) => {
          this.courses = response;
          this.coursesSubject.next(this.courses);
        })
      )
      .subscribe();
  }

  getCourses(filter: CourseFilter) {
    if (!filter) {
      return this.coursesSubject;
    }

    return this.coursesSubject.pipe(
      map((courses) =>
        courses.filter(
          (course) =>
            course.name.toLowerCase().includes(filter.name.toLowerCase()) &&
            (filter.selectedDepartment === null ||
              course.department === filter.selectedDepartment) &&
            (filter.selectedStudyProgram === null ||
              course.major === filter.selectedStudyProgram) &&
            (filter.selectedDegree === null ||
              course.degree === filter.selectedDegree) &&
            (filter.selectedYear === null ||
              +course.year === +filter.selectedYear) &&
            (filter.selectedSemester === null ||
              +course.semester === +filter.selectedSemester) &&
            (filter.showInactive || course.isActive)
        )
      )
    );
  }

  getCourse(index: string) {
    return this.courses.find((x) => x.id === index);
  }

  getAllCourses() {
    return this.httpClient.get<Course[]>(`${environment.apiUrl}course`);
  }

  addCourse(courseData: {
    name: string;
    description: string;
    isActive?: boolean;
    degreeId: string;
    yearId: string;
    semesterId: string;
    departmentId: string;
    majorId: string;
    authorId: string;
    coAuthorsId: string[];
  }) {
    return this.httpClient
      .post<Course>(`${environment.apiUrl}/course`, courseData)
      .pipe(
        catchError(this.handleError),
        tap((response: Course) => {
          this.courses.push(response);
          this.coursesSubject.next(this.courses);
        })
      );
  }

  editCourse(edited: any) {
    return this.httpClient
      .patch<Course>(`${environment.apiUrl}course`, edited)
      .pipe(
        tap((updatedCourse) => {
          this.courses = this.courses.map((course) =>
            course.id === updatedCourse.id ? updatedCourse : course
          );
          this.coursesSubject.next(this.courses);
        })
      );
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
