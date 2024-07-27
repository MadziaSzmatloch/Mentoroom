import { Injectable } from '@angular/core';
import {
  Assignment,
  AssignmentFile,
  RequiredFile,
} from '../models/assignment.model';
import { Observable, catchError, map, tap, throwError } from 'rxjs';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable()
export class AssignmentService {
  constructor(private httpClient: HttpClient) {}

  getAssignmentsByCourseId(courseId: string): Observable<Assignment[]> {
    return this.httpClient
      .get<any[]>(`${environment.apiUrl}assignment/course/${courseId}`)
      .pipe(
        map((data) => {
          return data.map((item) => {
            const assignmentFiles = item.assignmentFiles.map(
              (file: any) => new AssignmentFile(file.id, file.name)
            );
            const requiredFiles = item.requiredFiles.map(
              (file: any) =>
                new RequiredFile(
                  file.id,
                  file.fileNameSuffix,
                  file.extension,
                  file.maxSizeInKb,
                  null
                )
            );
            return new Assignment(
              item.id,
              courseId,
              item.name,
              item.description,
              new Date(item.createdDate),
              new Date(item.deadlineDate),
              item.isActive,
              null,
              assignmentFiles,
              requiredFiles
            );
          });
        })
      );
  }

  getStudentAssignmentsByCourseId(courseId: string): Observable<Assignment[]> {
    return this.httpClient
      .get<any[]>(
        `${environment.apiUrl}studentcourse/studentcourse/${courseId}`
      )
      .pipe(
        map((data) => {
          console.log(data);
          return data.map((item) => {
            const assignmentFiles = item.assignmentFiles.map(
              (file: any) => new AssignmentFile(file.id, file.name)
            );
            const requiredFiles = item.requiredFiles.map(
              (file: any) =>
                new RequiredFile(
                  file.id,
                  file.fileNameSuffix,
                  file.extension,
                  file.maxSizeInKb,
                  file.isSended
                )
            );
            return new Assignment(
              item.id,
              courseId,
              item.name,
              item.description,
              new Date(item.createdDate),
              new Date(item.deadlineDate),
              item.isActive,
              item.isCompleted,
              assignmentFiles,
              requiredFiles
            );
          });
        })
      );
  }

  getAssignementResource(id: string): Observable<Blob> {
    return this.httpClient.get(`${environment.apiUrl}assignment/file`, {
      params: { attachmentId: id },
      responseType: 'blob',
    });
  }

  add(data: {
    name: string;
    deadlineDate: Date;
    description: string;
    isActive?: boolean;
    files: {
      extension: string;
      maxSizeInKb: number;
      fileNameSuffix: string;
    }[];
  }) {
    return this.httpClient
      .post<any>(`${environment.apiUrl}assignment`, data)
      .pipe(
        map((response) => {
          const assignmentFiles = response.assignmentFiles?.map(
            (file: any) => new AssignmentFile(file.id, file.name)
          );
          const requiredFiles = response.requiredFiles?.map(
            (file: any) =>
              new RequiredFile(
                file.id,
                file.fileNameSuffix,
                file.extension,
                file.maxSizeInKb,
                null
              )
          );
          return new Assignment(
            response.id,
            response.courseId,
            response.name,
            response.description,
            new Date(response.createdDate),
            new Date(response.deadlineDate),
            response.isActive,
            assignmentFiles,
            requiredFiles
          );
        }),
        catchError(this.handleError)
      );
  }

  edit(edited: Assignment) {
    return this.httpClient.patch(`${environment.apiUrl}assignment`, edited);
  }

  delete(edited: Assignment) {
    return this.httpClient.delete(
      `${environment.apiUrl}assignment/${edited.id}`
    );
  }

  addResource(assignmentId: string, fileName: string, file: any) {
    const formData: FormData = new FormData();
    formData.append('AssignmentId', assignmentId);
    formData.append('FileName', fileName);
    formData.append('File', file, fileName);

    const headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');

    return this.httpClient.post(
      `${environment.apiUrl}assignment/file`,
      formData,
      {
        headers,
      }
    );
  }

  deleteResource(resourceId: string) {
    return this.httpClient.delete(
      `${environment.apiUrl}assignment/file/${resourceId}`
    );
  }

  addRequiredFile(
    assignmentId: string,
    extension: string,
    maxSizeInKb: number,
    fileNameSuffix: string
  ) {
    return this.httpClient.post(
      `${environment.apiUrl}assignment/requiredfile`,
      {
        assignmentId: assignmentId,
        extension: extension,
        maxSizeInKb: maxSizeInKb,
        fileNameSuffix: fileNameSuffix,
      }
    );
  }

  deleteRequiredFile(id: string) {
    return this.httpClient.delete(
      `${environment.apiUrl}assignment/requiredfile/${id}`
    );
  }

  private handleError(errorResponse: HttpErrorResponse) {
    console.log(errorResponse);
    let errorMessage = 'An unknown error occurred!';
    if (!errorResponse.error) {
      return throwError(() => errorMessage);
    }
    errorMessage = errorResponse.error;
    return throwError(() => errorMessage);
  }
}
