import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import UserData from '../interfaces/user-data.interface';

@Injectable()
export class UsersService {
  constructor(private httpClient: HttpClient) {}

  getUsers() {
    return this.httpClient.get<UserData[]>(`${environment.apiUrl}user`);
  }

  getLecturers() {
    return this.httpClient.get<UserData[]>(
      `${environment.apiUrl}user/lecturers`
    );
  }
}
