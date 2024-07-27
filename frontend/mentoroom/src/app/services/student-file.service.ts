import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { map, Observable } from 'rxjs';
import {
  Assignment,
  AssignmentFile,
  RequiredFile,
} from '../models/assignment.model';

@Injectable()
export class StudentFileService {
  constructor(private httpClient: HttpClient) {}

  addStudentFile(
    studentId: string,
    assignmentFileId: string,
    file: any
  ): Observable<Assignment> {
    const formData: FormData = new FormData();
    formData.append('StudentId', studentId);
    formData.append('AssignmentFileId', assignmentFileId);
    formData.append('File', file);

    const headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');

    return this.httpClient
      .post<any>(`${environment.apiUrl}StudentFile`, formData, {
        headers,
      })
      .pipe(
        map((data) => {
          const assignmentFiles = data.assignmentFiles.map(
            (file: any) => new AssignmentFile(file.id, file.name)
          );
          const requiredFiles = data.requiredFiles.map(
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
            data.id,
            data.courseId,
            data.name,
            data.description,
            new Date(data.createdDate),
            new Date(data.deadlineDate),
            data.isActive,
            data.isCompleted,
            assignmentFiles,
            requiredFiles
          );
        })
      );
  }

  getStudentsFilesByCourse(courseId: String): Observable<Blob> {
    return this.httpClient.get(
      `${environment.apiUrl}StudentFile/course/${courseId}`,
      {
        responseType: 'blob',
      }
    );
  }

  getStudentsFilesByAssignment(assignmentId: String): Observable<Blob> {
    return this.httpClient.get(
      `${environment.apiUrl}StudentFile/assignment/${assignmentId}`,
      {
        responseType: 'blob',
      }
    );
  }

  downloadSendedFile(id: String) {
    return this.httpClient.get(`${environment.apiUrl}StudentFile/${id}`, {
      responseType: 'blob',
    });
  }

  deleteSendedFile(id: String) {
    return this.httpClient.delete(`${environment.apiUrl}StudentFile/${id}`);
  }
}
