import { Component, OnInit } from '@angular/core';
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';
import { AssignmentService } from '../../../services/assignment.service';
import { Course } from '../../../models/course.model';
import { Assignment, RequiredFile } from '../../../models/assignment.model';
import { CourseService } from '../../../services/course.service';
import { FileUploadModule } from 'primeng/fileupload';
import { ActivatedRoute, Params } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { StudentFileService } from '../../../services/student-file.service';

interface UploadEvent {
  originalEvent: Event;
  files: File[];
}

@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrl: './course-detail.component.css',
  providers: [AssignmentService, FileUploadModule, StudentFileService],
  animations: [
    trigger('side-panel', [
      state(
        'in',
        style({
          opacity: 1,
          transform: 'translateX(0)',
        })
      ),
      transition('void => *', [
        style({
          opacity: 0,
          transform: 'translateX(500px)',
        }),
        animate(100),
      ]),
    ]),
  ],
})
export class CourseDetailComponent implements OnInit {
  id: string;
  course: Course;
  assignments: Assignment[];

  editedAssignment: Assignment;

  isLoading: boolean = false;

  userRole: string | null;

  mode: any;

  visibleEditCourse = false;
  visibleRequiredFiles = false;
  visibleEditAssignment = false;
  visibleEditResources = false;
  visibleAddAssignment = false;

  modeOptions: any[] = [
    { icon: 'pi pi-graduation-cap', type: 'DefaultMode' },
    { icon: 'pi pi-user', type: 'StudentListMode' },
  ];

  constructor(
    private courseService: CourseService,
    private route: ActivatedRoute,
    private authService: AuthService,
    private assignmentService: AssignmentService,
    private studentFileService: StudentFileService
  ) {}

  ngOnInit(): void {
    this.mode = this.modeOptions.find(
      (option) => option.type === 'DefaultMode'
    );
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
      this.course = this.courseService.getCourse(this.id);
      this.courseService.setSelectedCourseIndex(this.id);

      this.isLoading = true;

      this.authService.user.subscribe((user) => {
        this.userRole = user?.getUserRole;
      });

      if (this.userRole === 'Student') {
        console.log('Student');
        this.assignmentService
          .getStudentAssignmentsByCourseId(this.id)
          .subscribe({
            next: (assignmennts: Assignment[]) => {
              this.isLoading = false;
              this.assignments = assignmennts;
              this.createMenuItemsForAssignments();
            },
            error: (message) => {
              //this.errorMessage = message;
              this.assignments = [];
              this.isLoading = false;
            },
          });
      } else {
        console.log('Lecturer');
        this.assignmentService.getAssignmentsByCourseId(this.id).subscribe({
          next: (assignmennts: Assignment[]) => {
            this.isLoading = false;
            this.assignments = assignmennts;

            this.createMenuItemsForAssignments();
          },
          error: (message) => {
            //this.errorMessage = message;
            this.assignments = [];
            this.isLoading = false;
          },
        });
      }
    });
  }

  createMenuItemsForAssignments(): void {
    this.assignments.forEach((assignment) => {
      if (assignment.assignmentFiles && assignment.assignmentFiles.length > 0) {
        assignment.menuItems = assignment.assignmentFiles.map((file) => ({
          icon: 'pi pi-download',
          label: file.name,
          command: () => {
            this.downloadResource(file);
          },
        }));
      }
    });
  }

  downloadResource(resource): void {
    this.assignmentService
      .getAssignementResource(resource.id)
      .subscribe((blob) => {
        const a = document.createElement('a');
        const objectUrl = URL.createObjectURL(blob);
        a.href = objectUrl;
        a.download = resource.name;
        a.click();
        URL.revokeObjectURL(objectUrl);
      });
  }

  downloadStudentsFilesByAssignment(assignment: Assignment) {
    this.studentFileService
      .getStudentsFilesByAssignment(assignment.id)
      .subscribe((blob) => {
        const a = document.createElement('a');
        const objectUrl = URL.createObjectURL(blob);
        a.href = objectUrl;
        a.download = assignment.name;
        a.click();
        URL.revokeObjectURL(objectUrl);
      });
  }

  downloadStudentsFilesByCourse() {
    this.studentFileService
      .getStudentsFilesByCourse(this.course.id)
      .subscribe((blob) => {
        const a = document.createElement('a');
        const objectUrl = URL.createObjectURL(blob);
        a.href = objectUrl;
        a.download = this.course.name;
        a.click();
        URL.revokeObjectURL(objectUrl);
      });
  }

  deleteResource(resourceId: string) {
    this.assignmentService.deleteResource(resourceId).subscribe(() => {
      this.editedAssignment.assignmentFiles =
        this.editedAssignment.assignmentFiles.filter(
          (x) => x.id !== resourceId
        );
    });
  }

  deleteRequiredFile(assignmentId: string, requiredFileId: string) {
    this.assignmentService.deleteRequiredFile(requiredFileId).subscribe(() => {
      const index = this.assignments.findIndex(
        (assign) => assign.id === assignmentId
      );

      if (index !== -1) {
        this.assignments[index].requiredFiles = this.assignments[
          index
        ].requiredFiles.filter((file) => file.id !== requiredFileId);
      }
    });
  }

  OnEditAssignment(assignment: Assignment) {
    this.visibleEditAssignment = true;
    this.editedAssignment = { ...assignment };
  }

  OnEditResources(assignment: Assignment) {
    this.visibleEditResources = true;
    this.editedAssignment = { ...assignment };
  }

  OnEditAssignmentHide() {
    this.visibleEditAssignment = false;
    this.assignments = this.assignments.map((assignment) =>
      assignment.id === this.editedAssignment.id
        ? this.editedAssignment
        : assignment
    );
    this.editedAssignment = null;
    this.createMenuItemsForAssignments();
  }

  OnEditResourcesHide() {
    this.assignments = this.assignments.map((assignment) =>
      assignment.id === this.editedAssignment.id
        ? this.editedAssignment
        : assignment
    );
    this.editedAssignment = null;
    this.createMenuItemsForAssignments();
  }

  OnNewAssignment() {
    this.visibleAddAssignment = true;
  }

  addAssignment(assignmentAdded: Assignment) {
    this.assignments.push(assignmentAdded);
    this.createMenuItemsForAssignments();
    this.visibleAddAssignment = false;
  }

  editAssignment() {
    this.assignmentService.edit(this.editedAssignment).subscribe((res) => {
      this.OnEditAssignmentHide();
    });
  }

  deleteAssignment() {
    this.assignmentService.delete(this.editedAssignment).subscribe((res) => {
      this.assignments = this.assignments.filter(
        (assignment) => assignment.id !== this.editedAssignment.id
      );
      this.visibleEditAssignment = false;
    });
  }

  uploadedResourceFile: any;
  onResourceHandled(event: { files: Blob[] }) {
    const file = event.files[0];
    const nameWithoutExtension = (file as any).name.substring(
      0,
      (file as any).name.lastIndexOf('.')
    );

    this.assignmentService
      .addResource(this.editedAssignment.id, nameWithoutExtension, file)
      .subscribe((res: Assignment) => {
        this.editedAssignment = res;
      });
  }

  onStudentFileHandled(event: { files: Blob[] }, requiredFileId: string) {
    const file = event.files[0];

    this.studentFileService
      .addStudentFile(this.authService.userID, requiredFileId, file)
      .subscribe((res: Assignment) => {
        this.assignments.map((assignment) => {
          if (assignment.id === res.id) {
            assignment.requiredFiles = res.requiredFiles;
          }
        });
      });
  }

  OnEditRequiredFilesShow(assignment: Assignment) {
    this.visibleRequiredFiles = true;
    this.editedAssignment = { ...assignment };
  }

  OnEditRequiredFilesHide() {
    this.visibleRequiredFiles = false;
  }

  OnEditCourseShow() {
    this.visibleEditCourse = true;
  }

  OnEditCourseHide() {
    this.visibleEditCourse = false;
  }

  hasRequiredFiles(): boolean {
    return this.assignments?.some(
      (assignment) => assignment.requiredFiles.length > 0
    );
  }

  downloadSendedFile(requiredFile: RequiredFile) {
    this.studentFileService
      .downloadSendedFile(requiredFile.id)
      .subscribe((blob) => {
        const a = document.createElement('a');
        const objectUrl = URL.createObjectURL(blob);
        a.href = objectUrl;
        a.download =
          requiredFile.fileNameSuffix +
          '.' +
          requiredFile.extension.toLowerCase();
        a.click();
        URL.revokeObjectURL(objectUrl);
      });
  }

  deleteSendedFile(requiredFile: RequiredFile) {
    this.studentFileService
      .deleteSendedFile(requiredFile.id)
      .subscribe((res) => {
        const assignment = this.assignments.find((a) =>
          a.requiredFiles.some((f) => f.id === requiredFile.id)
        );
        if (assignment) {
          const file = assignment.requiredFiles.find(
            (f) => f.id === requiredFile.id
          );
          if (file) {
            file.isSended = false;
          }
        }
      });
  }

  handleCourseUpdated(updatedCourse: Course) {
    this.course = { ...this.course, ...updatedCourse };
  }
}
