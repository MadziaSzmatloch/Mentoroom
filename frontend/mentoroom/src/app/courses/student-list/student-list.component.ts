import { Component, OnInit } from '@angular/core';
import {
  Student,
  StudentCourseService,
} from '../../services/student-course.service';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrl: './student-list.component.css',
  providers: [StudentCourseService],
})
export class StudentListComponent implements OnInit {
  courseId: string;
  studentList: Student[];

  constructor(
    private studentCourseService: StudentCourseService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.courseId = params['id'];
      this.updateStudentList(this.courseId);
    });
  }

  updateStudentList(courseId: string) {
    this.studentCourseService.getStudentListByCourseId(courseId).subscribe({
      next: (response: Student[]) => {
        this.studentList = response;
      },
      error: (message) => {
        this.studentList = null;
      },
    });
  }

  acceptStudent(studentId: string) {
    this.studentCourseService
      .acceptStudent(this.courseId, studentId)
      .subscribe((response) => {
        console.log(response);
        this.updateStudentList(this.courseId);
      });
  }

  declineStudent(studentId: string) {
    this.studentCourseService
      .declineStudent(this.courseId, studentId)
      .subscribe((response) => {
        console.log(response);
        this.updateStudentList(this.courseId);
      });
  }
}
