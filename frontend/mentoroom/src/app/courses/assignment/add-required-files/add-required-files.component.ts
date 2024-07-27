import { Component, Input, OnInit } from '@angular/core';
import { AssignmentService } from '../../../services/assignment.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-required-files',
  templateUrl: './add-required-files.component.html',
  styleUrl: './add-required-files.component.css',
})
export class AddRequiredFilesComponent implements OnInit {
  @Input() files: any;
  @Input() assignnmentId: string;
  requiredFilesForm: FormGroup;
  extensions: string[];
  constructor(private assignmentService: AssignmentService) {}
  ngOnInit(): void {
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
  }

  OnAddFile() {
    this.assignmentService
      .addRequiredFile(
        this.assignnmentId,
        this.requiredFilesForm.value.extension,
        this.requiredFilesForm.value.maxSizeInKb,
        this.requiredFilesForm.value.fileNameSuffix
      )
      .subscribe(() => {
        this.files.push({
          fileNameSuffix: this.requiredFilesForm.value.fileNameSuffix,
          extension: this.requiredFilesForm.value.extension,
          maxSizeInKb: this.requiredFilesForm.value.maxSizeInKb,
        });
        this.requiredFilesForm.reset();
      });
  }
}
