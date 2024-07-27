import { Component, OnDestroy, OnInit } from '@angular/core';
import { CourseService } from '../services/course.service';
import { Course } from '../models/course.model';
import { StudentCourseService } from '../services/student-course.service';
import { AuthService } from '../services/auth.service';
import {
  Degree,
  Department,
  Semester,
  StudyProgram,
  Year,
} from '../interfaces/tags.interface';
import { CourseFilter } from '../models/course-filter.model';
import { CourseAttributeService } from '../services/course-attribute.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrl: './courses.component.css',
  providers: [CourseService, StudentCourseService, CourseAttributeService],
})
export class CoursesComponent implements OnInit, OnDestroy {
  courses: Course[];
  filtredCourses: Course[];

  selectedCourse: Course | null;
  modalVisibility = false;
  userId: string;

  subscriptions: Subscription = new Subscription();
  filter: CourseFilter;
  name: string;
  departments: Department[];
  studyPrograms: StudyProgram[];
  years = [1, 2, 3];
  semesters = [1, 2];
  degrees: Degree[] = [];

  constructor(
    private courseAttributeService: CourseAttributeService,
    private courseService: CourseService,
    private studentCourseService: StudentCourseService,
    private authService: AuthService
  ) {
    this.filter = new CourseFilter();
  }

  ngOnInit(): void {
    this.courseService.getAllCourses().subscribe((courses: Course[]) => {
      this.courses = courses;
      this.courseService.getCourses(null).subscribe((myCourses: Course[]) => {
        this.courses = this.courses.filter(
          (course) => !myCourses.some((myCourse) => myCourse.id === course.id)
        );
        this.filtredCourses = this.courses;
        this.filterCourses();
      });
    });

    this.subscriptions.add(
      this.courseAttributeService.getDegrees().subscribe((degrees) => {
        this.degrees = degrees;
      })
    );
    this.subscriptions.add(
      this.courseAttributeService.getDepartments().subscribe((departments) => {
        this.departments = departments;
      })
    );
    this.subscriptions.add(
      this.courseAttributeService
        .getStudyPrograms()
        .subscribe((studyPrograms) => {
          this.studyPrograms = studyPrograms;
        })
    );
    // this.subscriptions.add(
    //   this.courseAttributeService.getYears().subscribe((years) => {
    //     this.years = years;
    //   })
    // );
    // this.subscriptions.add(
    //   this.courseAttributeService.getSemesters().subscribe((semesters) => {
    //     this.semesters = semesters;
    //   })
    // );
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  OnFiltersChanged() {
    this.filterCourses();
    console.log(this.filter);
  }

  filterCourses() {
    console.log(this.courses);
    this.filtredCourses = this.courses.filter(
      (course) =>
        course.name.toLowerCase().includes(this.filter.name.toLowerCase()) &&
        (this.filter.selectedDepartment === null ||
          course.department === this.filter.selectedDepartment) &&
        (this.filter.selectedStudyProgram === null ||
          course.major === this.filter.selectedStudyProgram) &&
        (this.filter.selectedDegree === null ||
          course.degree === this.filter.selectedDegree) &&
        (this.filter.selectedYear === null ||
          +course.year === +this.filter.selectedYear) &&
        (this.filter.selectedSemester === null ||
          +course.semester === +this.filter.selectedSemester) &&
        (this.filter.showInactive || course.isActive)
    );
  }

  onJoinCourse(course: Course) {
    if (this.authService.isStudent) {
      this.selectedCourse = course;
      this.modalVisibility = true;
    }
  }

  cancel() {
    this.selectedCourse = null;
    this.modalVisibility = false;
  }

  join() {
    this.studentCourseService
      .joinCourse(this.selectedCourse.id, this.authService.userID)
      .subscribe((resposnes) => {});
    this.selectedCourse = null;
    this.modalVisibility = false;
  }
}
