import { Component, OnDestroy, OnInit } from '@angular/core';

import { Subscription } from 'rxjs';
import { CourseSearcherService } from '../../../services/course-searcher.service';
import { CourseService } from '../../../services/course.service';
import { Course } from '../../../models/course.model';
import { CourseFilter } from '../../../models/course-filter.model';

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrl: './course-list.component.css',
})
export class CourseListComponent implements OnInit, OnDestroy {
  courses: Course[];
  private filterChangeSub: Subscription;
  isLoading: boolean;

  constructor(
    private courseService: CourseService,
    private courseSearcherService: CourseSearcherService
  ) {}

  ngOnInit() {
    this.isLoading = true;

    this.courseService
      .getCourses(this.courseSearcherService.getFilter())
      .subscribe((courses) => {
        this.courses = courses;
        this.isLoading = false;
      });

    this.filterChangeSub = this.courseSearcherService.filterChanged.subscribe(
      (filter: CourseFilter) => {
        this.courseService.getCourses(filter).subscribe((filteredCourses) => {
          this.courses = filteredCourses;
        });
      }
    );
  }

  ngOnDestroy(): void {
    this.filterChangeSub.unsubscribe();
  }
}
