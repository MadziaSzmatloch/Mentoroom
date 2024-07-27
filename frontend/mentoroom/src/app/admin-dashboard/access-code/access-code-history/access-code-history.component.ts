import { Component, EventEmitter, Input } from '@angular/core';
import { AccessCode } from '../../../models/access-code.model';
import { ConfirmationService } from 'primeng/api';
import { AccessCodesService } from '../../../services/access-codes.service';

@Component({
  selector: 'app-access-code-history',
  templateUrl: './access-code-history.component.html',
  styleUrl: './access-code-history.component.css',
})
export class AccessCodeHistoryComponent {
  @Input() accessCodes: AccessCode[];
  @Input() rows = 5;

  first = 0;

  constructor(
    private confirmationService: ConfirmationService,
    private accessCodesService: AccessCodesService
  ) {}

  next() {
    this.first = this.first + this.rows;
  }

  prev() {
    this.first = this.first - this.rows;
  }

  reset() {
    this.first = 0;
  }

  pageChange(event) {
    this.first = event.first;
    this.rows = event.rows;
  }

  isLastPage(): boolean {
    return this.accessCodes
      ? this.first === this.accessCodes.length - this.rows
      : true;
  }

  isFirstPage(): boolean {
    return this.accessCodes ? this.first === 0 : true;
  }

  deactivate(accessCode: AccessCode) {
    this.confirmationService.confirm({
      message: `Are you sure that you want to deactivate access code: ${accessCode.code}?`,
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      acceptIcon: 'none',
      rejectIcon: 'none',
      rejectButtonStyleClass: 'p-button-text',
      accept: () => {
        this.accessCodesService.deactivateAccessCode(accessCode.id).subscribe({
          next: (response: AccessCode) => {
            let index = this.accessCodes.findIndex((x) => x.id === response.id);
            if (index !== -1) {
              this.accessCodes.splice(index, 1, response);
            }
          },
          error: (errorMessage) => {
            console.log(errorMessage);
          },
        });
      },
    });
  }
}
