import {
  Component,
  Input,
  OnDestroy,
  OnInit,
  ViewEncapsulation,
} from '@angular/core';
import {
  ActivatedRoute,
  NavigationEnd,
  Params,
  Route,
  Router,
} from '@angular/router';
import { Subscription, filter } from 'rxjs';
import { Course } from '../../../../models/course.model';
import { CourseService } from '../../../../services/course.service';

@Component({
  selector: 'app-course-item',
  templateUrl: './course-item.component.html',
  styleUrl: './course-item.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class CourseItemComponent implements OnInit, OnDestroy {
  @Input() course: Course;
  @Input() id: string;

  selected: Boolean = false;
  selectedIndexSub: Subscription;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private courseService: CourseService
  ) {}

  ngOnInit(): void {
    this.selectedIndexSub = this.courseService.selectedIdChanged.subscribe(
      (id: string) => {
        if (id !== this.id) {
          this.selected = false;
        }
      }
    );
  }

  ngOnDestroy(): void {
    this.selectedIndexSub.unsubscribe();
  }

  OnCourseClick() {
    if (!this.selected) {
      this.router.navigate([this.id], { relativeTo: this.route });
    } else {
      this.router.navigate(['/my-courses']);
    }

    this.selected = !this.selected;
  }
}
