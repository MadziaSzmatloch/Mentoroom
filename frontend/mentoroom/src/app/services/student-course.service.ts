import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Course } from '../models/course.model';

export interface Student {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  isConfirmed: boolean | null;
}

@Injectable()
export class StudentCourseService {
  constructor(private httpClient: HttpClient) {}

  getStudentListByCourseId(courseId: string) {
    return this.httpClient.get<Student[]>(
      `${environment.apiUrl}studentcourse/courses/${courseId}`
    );
  }

  joinCourse(courseId: string, studentId: string) {
    return this.httpClient.post(`${environment.apiUrl}studentcourse`, {
      studentId: studentId,
      courseId: courseId,
    });
  }

  acceptStudent(courseId: string, studentId: string) {
    return this.httpClient.patch(`${environment.apiUrl}studentcourse/confirm`, {
      studentId: studentId,
      courseId: courseId,
    });
  }

  declineStudent(courseId: string, studentId: string) {
    return this.httpClient.delete(
      `${environment.apiUrl}studentcourse/${studentId}/${courseId}`
    );
  }
}
