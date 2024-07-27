import { Component } from '@angular/core';
import { CourseService } from '../../services/course.service';

@Component({
  selector: 'app-my-courses',
  templateUrl: './my-courses.component.html',
  styleUrl: './my-courses.component.css',
  providers: [CourseService],
})
export class MyCoursesComponent {
  modalVisibility: boolean = false;

  OnShowModal(modalVisibility: boolean) {
    this.modalVisibility = modalVisibility;
  }
}
