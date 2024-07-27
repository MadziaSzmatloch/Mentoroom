import { Injectable } from '@angular/core';
import { CourseFilter } from '../models/course-filter.model';
import { Subject } from 'rxjs';

@Injectable()
export class CourseSearcherService {
  private filter: CourseFilter = new CourseFilter();
  filterChanged = new Subject<CourseFilter>();

  getFilter(): CourseFilter {
    return this.filter;
  }

  setFilter(filter: CourseFilter) {
    this.filter = filter;
    this.filterChanged.next(filter);
  }
}
