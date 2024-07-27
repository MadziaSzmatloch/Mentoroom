import {
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { Subscription } from 'rxjs';
import { CourseAttributeService } from '../../../services/course-attribute.service';
import {
  Degree,
  Department,
  Semester,
  StudyProgram,
  Year,
} from '../../../interfaces/tags.interface';
import { CourseFilter } from '../../../models/course-filter.model';
import { CourseSearcherService } from '../../../services/course-searcher.service';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-course-searcher',
  templateUrl: './course-searcher.component.html',
  styleUrl: './course-searcher.component.css',
  providers: [CourseAttributeService],
})
export class CourseSearcherComponent implements OnInit, OnDestroy {
  name: string;
  departments: Department[];
  studyPrograms: StudyProgram[];
  years = [1, 2, 3];
  semesters = [1, 2];
  degrees: Degree[] = [];

  userRole: string | null;

  subscriptions: Subscription = new Subscription();

  filter: CourseFilter;

  @Output() showModal = new EventEmitter<boolean>();

  constructor(
    private courseSearcherService: CourseSearcherService,
    private courseAttributeService: CourseAttributeService,
    private authService: AuthService
  ) {
    this.filter = new CourseFilter();
  }

  ngOnInit() {
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

    this.subscriptions.add(
      this.authService.user.subscribe((user) => {
        this.userRole = user?.getUserRole;
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  OnFiltersChanged() {
    this.courseSearcherService.setFilter(this.filter);
  }

  OnShowModal() {
    this.showModal.emit(true);
  }
}
