import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AssignmentService } from '../../../services/assignment.service';
import { ActivatedRoute, Params } from '@angular/router';
import { Assignment } from '../../../models/assignment.model';

interface RequiredFile {
  fileNameSuffix: string;
  extension: string;
  maxSizeInKb: number;
}

@Component({
  selector: 'app-add-assignment',
  templateUrl: './add-assignment.component.html',
  styleUrl: './add-assignment.component.css',
  providers: [AssignmentService],
})
export class AddAssignmentComponent implements OnInit {
  courseId: string;
  form: FormGroup;
  requiredFilesForm: FormGroup;

  extensions: string[];

  isLoading = false;

  @Output() addedAssignment = new EventEmitter<Assignment>();

  files: RequiredFile[] = [];

  constructor(
    private assignmentService: AssignmentService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl(null, Validators.required),
      deadLineDate: new FormControl(null, Validators.required),
      isActive: new FormControl(false, Validators.required),
      description: new FormControl(null, Validators.required),
    });

    this.requiredFilesForm = new FormGroup({
      fileNameSuffix: new FormControl(null, Validators.required),
      maxSizeInKb: new FormControl(null, Validators.required),
      extension: new FormControl(null, Validators.required),
    });

    this.extensions = [
      'PNG',
      'JPG',
      'DOC',
      'DOCX',
      'JS',
      'MP3',
      'MP4',
      'PDF',
      'PPT',
      'PPTX',
      'TXT',
      'XLS',
      'XLSX',
      'ZIP',
    ];

    this.route.params.subscribe((params: Params) => {
      this.courseId = params['id'];
    });
  }

  OnAddFile() {
    this.files.push({
      fileNameSuffix: this.requiredFilesForm.value.fileNameSuffix,
      extension: this.requiredFilesForm.value.extension,
      maxSizeInKb: this.requiredFilesForm.value.maxSizeInKb,
    });
    this.requiredFilesForm.reset();
  }

  deleteFile(fileToDelete: any) {
    this.files = this.files.filter((file) => file !== fileToDelete);
  }

  onCreate() {
    this.isLoading = true;
    const formValues = this.form.value;

    const data = {
      name: formValues.name,
      deadlineDate: new Date(formValues.deadLineDate),
      isActive: formValues.isActive === true,
      description: formValues.description,
      courseId: this.courseId,
      files: this.files,
    };

    this.assignmentService.add(data).subscribe({
      next: (res: Assignment) => {
        this.addedAssignment.emit(res);
        this.isLoading = false;
        //this.errorOccured = false;
        //this.errorMessage = '';
        this.form.reset();
        this.requiredFilesForm.reset();
        this.files = [];
        //this.isAdded = true;
      },
      error: (message) => {
        console.log(message);
        this.isLoading = false;
        //this.errorOccured = true;
        //this.errorMessage = message;
      },
    });
  }
}
