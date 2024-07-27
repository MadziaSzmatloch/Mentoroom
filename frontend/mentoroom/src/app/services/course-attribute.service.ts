import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import {
  Degree,
  Department,
  Semester,
  StudyProgram,
  Year,
} from '../interfaces/tags.interface';
import { catchError, throwError } from 'rxjs';

@Injectable()
export class CourseAttributeService {
  constructor(private httpClient: HttpClient) {}
  getDegrees() {
    return this.httpClient
      .get<Degree[]>(`${environment.apiUrl}tags/degree`)
      .pipe(catchError(this.handleError));
  }

  getDepartments() {
    return this.httpClient
      .get<Department[]>(`${environment.apiUrl}tags/Department`)
      .pipe(catchError(this.handleError));
  }

  getStudyPrograms() {
    return this.httpClient
      .get<StudyProgram[]>(`${environment.apiUrl}tags/Major`)
      .pipe(catchError(this.handleError));
  }

  getSemesters() {
    return this.httpClient
      .get<Semester[]>(`${environment.apiUrl}tags/Semester`)
      .pipe(catchError(this.handleError));
  }

  getYears() {
    return this.httpClient
      .get<Year[]>(`${environment.apiUrl}tags/Year`)
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
