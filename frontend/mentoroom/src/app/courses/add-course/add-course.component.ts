import { Component, OnDestroy, OnInit } from '@angular/core';
import { CourseAttributeService } from '../../services/course-attribute.service';
import {
  Degree,
  Department,
  Semester,
  StudyProgram,
  Year,
} from '../../interfaces/tags.interface';
import { Subscription } from 'rxjs';
import { UsersService } from '../../services/users.service';
import UserData from '../../interfaces/user-data.interface';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CourseService } from '../../services/course.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-add-course',
  templateUrl: './add-course.component.html',
  styleUrl: './add-course.component.css',
  providers: [CourseAttributeService, UsersService],
})
export class AddCourseComponent implements OnInit, OnDestroy {
  degrees: Degree[] = [];

  departments: Department[] = [];
  studyPrograms: StudyProgram[] = [];
  years: Year[] = [];
  semesters: Semester[] = [];

  filteredYears: Year[] = [];
  filteredStudyPrograms: StudyProgram[] = [];
  filteredSemesters: Semester[] = [];

  lecturers: UserData[] = [];
  subscriptions: Subscription = new Subscription();

  addCourseForm: FormGroup;

  isLoading: boolean = false;
  isAdded: boolean = false;
  errorOccured: boolean = false;
  errorMessage: string;

  addedCourseId: string;

  constructor(
    private authService: AuthService,
    private courseService: CourseService,
    private courseAttributeService: CourseAttributeService,
    private usersService: UsersService
  ) {}

  ngOnInit() {
    this.addCourseForm = new FormGroup({
      step1: new FormGroup({
        degreeId: new FormControl(null, Validators.required),
        name: new FormControl(null, Validators.required),
        description: new FormControl(null, Validators.required),
      }),
      step2: new FormGroup({
        departmentId: new FormControl(null, Validators.required),
        studyProgramId: new FormControl(
          { value: null, disabled: true },
          Validators.required
        ),
        yearId: new FormControl(null, Validators.required),
        semesterId: new FormControl(
          { value: null, disabled: true },
          Validators.required
        ),
      }),
      step3: new FormGroup({
        coAuthors: new FormControl(null),
      }),
    });

    //#region Subscriptions
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
    this.subscriptions.add(
      this.courseAttributeService.getYears().subscribe((years) => {
        this.years = years;
      })
    );
    this.subscriptions.add(
      this.courseAttributeService.getSemesters().subscribe((semesters) => {
        this.semesters = semesters;
      })
    );

    this.subscriptions.add(
      this.usersService.getLecturers().subscribe((lecturers: UserData[]) => {
        this.lecturers = lecturers.filter(
          (user) => user.id !== this.authService.userID
        );
      })
    );

    this.subscriptions.add(
      this.addCourseForm
        .get('step2.departmentId')
        .valueChanges.subscribe((departmentId) => {
          const studyProgramControl = this.addCourseForm.get(
            'step2.studyProgramId'
          );
          if (departmentId) {
            studyProgramControl?.enable();
          } else {
            studyProgramControl?.disable();
          }
        })
    );

    this.subscriptions.add(
      this.addCourseForm
        .get('step2.yearId')
        .valueChanges.subscribe((yearId) => {
          const semesterControl = this.addCourseForm.get('step2.semesterId');
          if (yearId) {
            semesterControl?.enable();
          } else {
            semesterControl?.disable();
          }
        })
    );
    //#endregion
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  onDegreeSelected(degreeId: { value: string }) {
    this.filteredYears = this.years.filter(
      (year) => year.degreeId == degreeId.value
    );
  }

  onDepartamentSelected(departamentId: { value: string }) {
    this.filteredStudyPrograms = this.studyPrograms.filter(
      (year) => year.departmentId == departamentId.value
    );
  }

  onYearSelected(yearId: { value: string }) {
    this.filteredSemesters = this.semesters.filter(
      (year) => year.yearId == yearId.value
    );
  }

  onCreateNewCourse() {
    const formValues = this.addCourseForm.value;
    const courseData = {
      name: formValues.step1.name,
      description: formValues.step1.description,
      degreeId: formValues.step1.degreeId,
      yearId: formValues.step2.yearId,
      semesterId: formValues.step2.semesterId,
      departmentId: formValues.step2.departmentId,
      majorId: formValues.step2.studyProgramId,
      authorId: this.authService.userID,
      coAuthorsId: formValues.step3.coAuthors || [],
      isActive: true,
    };
    this.isLoading = true;
    this.courseService.addCourse(courseData).subscribe({
      next: (res) => {
        this.addedCourseId = res.id;
        this.isLoading = false;
        this.errorOccured = false;
        this.errorMessage = '';
        this.addCourseForm.reset();
        this.isAdded = true;
      },
      error: (message) => {
        this.isLoading = false;
        this.errorOccured = true;
        this.errorMessage = message;
      },
    });
  }

  onRedirectToMyCourses() {
    console.log('Redirect to my courses');
  }
}
