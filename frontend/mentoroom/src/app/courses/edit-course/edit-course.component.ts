import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Course } from '../../models/course.model';
import UserData from '../../interfaces/user-data.interface';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  Degree,
  Department,
  Semester,
  StudyProgram,
  Year,
} from '../../interfaces/tags.interface';
import { forkJoin, Subscription } from 'rxjs';
import { CourseService } from '../../services/course.service';
import { AuthService } from '../../services/auth.service';
import { CourseAttributeService } from '../../services/course-attribute.service';
import { UsersService } from '../../services/users.service';

interface EditableCourse {
  id: string;
  name: string;
  description: string;
  isActive: boolean;
  department: Department;
  studyProgram: StudyProgram;
  degree: Degree;
  year: Year;
  semester: Semester;
  author: {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    role: string;
  };
  coAuthors: {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    role: string;
  }[];
}

@Component({
  selector: 'app-edit-course',
  templateUrl: './edit-course.component.html',
  styleUrl: './edit-course.component.css',
  providers: [CourseAttributeService, UsersService],
})
export class EditCourseComponent implements OnInit {
  @Input() course: Course;
  @Output() courseUpdated = new EventEmitter<Course>();
  editableCourse: EditableCourse;
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
    private courseAttributeService: CourseAttributeService,
    private usersService: UsersService,
    private courseService: CourseService
  ) {}

  ngOnInit() {
    const degrees$ = this.courseAttributeService.getDegrees();
    const departments$ = this.courseAttributeService.getDepartments();
    const studyPrograms$ = this.courseAttributeService.getStudyPrograms();
    const years$ = this.courseAttributeService.getYears();
    const semesters$ = this.courseAttributeService.getSemesters();
    const lecturers$ = this.usersService.getLecturers();

    this.subscriptions.add(
      forkJoin([
        degrees$,
        departments$,
        studyPrograms$,
        years$,
        semesters$,
        lecturers$,
      ]).subscribe(
        ([
          degrees,
          departments,
          studyPrograms,
          years,
          semesters,
          lecturers,
        ]) => {
          this.degrees = degrees;
          this.departments = departments;
          this.studyPrograms = studyPrograms;
          this.years = years;
          this.semesters = semesters;
          this.lecturers = lecturers.filter(
            (user) => user.id !== this.authService.userID
          );

          this.editableCourse = this.mapCourseToEditableCourse(this.course);

          this.filteredYears = this.years.filter(
            (year) => year.degreeId == this.editableCourse.degree.id
          );

          this.filteredStudyPrograms = this.studyPrograms.filter(
            (year) => year.departmentId == this.editableCourse.department.id
          );

          this.filteredSemesters = this.semesters.filter(
            (year) => year.yearId == this.editableCourse.year.id
          );

          this.addCourseForm = new FormGroup({
            step1: new FormGroup({
              degreeId: new FormControl(
                this.editableCourse.degree.id,
                Validators.required
              ),
              name: new FormControl(
                this.editableCourse.name,
                Validators.required
              ),
              description: new FormControl(
                this.editableCourse.description,
                Validators.required
              ),
            }),
            step2: new FormGroup({
              departmentId: new FormControl(
                this.editableCourse.department.id,
                Validators.required
              ),
              studyProgramId: new FormControl(
                this.editableCourse.studyProgram.id,
                Validators.required
              ),
              yearId: new FormControl(
                this.editableCourse.year.id,
                Validators.required
              ),
              semesterId: new FormControl(
                this.editableCourse.semester.id,
                Validators.required
              ),
            }),
            step3: new FormGroup({
              isActive: new FormControl(this.editableCourse.isActive),
              coAuthors: new FormControl(
                this.editableCourse.coAuthors.map((coAuthor) => coAuthor.id)
              ),
            }),
          });

          this.setupFormListeners();
        }
      )
    );
  }

  private setupFormListeners() {
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
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  onDegreeSelected(degreeId: { value: string }) {
    const prev = this.filteredYears;
    this.filteredYears = this.years.filter(
      (year) => year.degreeId === degreeId.value
    );
    if (!this.arraysEqual(prev, this.filteredYears)) {
      this.addCourseForm.get('step2.yearId').setValue(null);
    }
  }

  onDepartamentSelected(departamentId: { value: string }) {
    const prev = this.filteredStudyPrograms;
    this.filteredStudyPrograms = this.studyPrograms.filter(
      (program) => program.departmentId === departamentId.value
    );
    if (!this.arraysEqual(prev, this.filteredStudyPrograms)) {
      this.addCourseForm.get('step2.studyProgramId').setValue(null);
    }
  }

  onYearSelected(yearId: { value: string }) {
    const prev = this.filteredSemesters;
    this.filteredSemesters = this.semesters.filter(
      (semester) => semester.yearId === yearId.value
    );
    if (!this.arraysEqual(prev, this.filteredSemesters)) {
      this.addCourseForm.get('step2.semesterId').setValue(null);
    }
  }

  arraysEqual(a: any[], b: any[]): boolean {
    if (a === b) return true;
    if (a == null || b == null) return false;
    if (a.length !== b.length) return false;

    for (let i = 0; i < a.length; ++i) {
      if (a[i] !== b[i]) return false;
    }
    return true;
  }

  mapCourseToEditableCourse(course: Course): EditableCourse {
    console.log(this.degrees);
    const department = this.departments.find(
      (dept) => dept.name === course.department
    ) || { id: '', name: '', shortName: '' };
    const studyProgram = this.studyPrograms.find(
      (prog) => prog.name === course.major
    ) || { id: '', name: '', departmentId: '' };
    const degree = this.degrees.find((deg) => deg.name === course.degree) || {
      id: '',
      name: '',
    };
    const year = this.years.find(
      (yr) => yr.name === course.year && yr.degreeId === degree.id
    ) || { id: '', name: '', degreeId: '' };
    const semester = this.semesters.find(
      (sem) => sem.name === course.semester && sem.yearId === year.id
    ) || { id: '', name: '', yearId: '' };

    return {
      id: course.id,
      name: course.name,
      description: course.description,
      isActive: course.isActive,
      department: department,
      studyProgram: studyProgram,
      degree: degree,
      year: year,
      semester: semester,
      author: course.author,
      coAuthors: course.coAuthors,
    };
  }

  editAssignment() {
    this.isLoading = true;

    const data = this.addCourseForm.value;

    const editedCourse = {
      name: data.step1.name,
      description: data.step1.description,
      isActive: data.step3.isActive,
      degreeId: data.step1.degreeId,
      yearId: data.step2.yearId,
      semesterId: data.step2.semesterId,
      departmentId: data.step2.departmentId,
      majorId: data.step2.studyProgramId,
      authorId: this.editableCourse.author.id,
      coAuthorsId: data.step3.coAuthors,
      id: this.editableCourse.id,
    };

    this.courseService.editCourse(editedCourse).subscribe((res: Course) => {
      this.isLoading = false;
      this.courseUpdated.emit(res);
    });
  }
}
