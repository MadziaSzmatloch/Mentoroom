import { Component, OnInit } from '@angular/core';
import { Clipboard } from '@angular/cdk/clipboard';
import { AccessCode } from '../../models/access-code.model';
import { Subscription } from 'rxjs';
import { AccessCodesService } from '../../services/access-codes.service';

@Component({
  selector: 'app-access-code',
  templateUrl: './access-code.component.html',
  styleUrl: './access-code.component.css',
})
export class AccessCodeComponent implements OnInit {
  expirationDate: Date | null;
  accessCodes: AccessCode[];

  selectedCode: AccessCode;

  accessCodeVisible: boolean;
  historyVisible: boolean;

  historyRows = 6;
  accessCodesSubscription: Subscription;

  constructor(
    private clipboard: Clipboard,
    private lecturersService: AccessCodesService
  ) {}

  ngOnInit(): void {
    this.accessCodesSubscription = this.lecturersService
      .getAccessCodes()
      .subscribe((accessCodes) => {
        this.accessCodes = accessCodes;
        this.selectedCode = accessCodes[0];
      });
  }

  ngOnDestroy(): void {
    this.accessCodesSubscription.unsubscribe();
  }

  generateNewCode() {
    this.lecturersService
      .createAccessCode(this.expirationDate)
      .subscribe((code) => {
        this.accessCodes.unshift(code);
        this.selectedCode = code;
      });
    this.expirationDate = null;
    this.accessCodeVisible = false;
  }

  onCopyCodeToClipboard() {
    this.clipboard.copy(this.selectedCode.code.toString());
  }

  showGenerateAccessCodeDialog() {
    this.accessCodeVisible = true;
  }

  hideGenerateAccessCodeDialog() {
    this.accessCodeVisible = false;
  }

  showHistoryDialog() {
    this.historyVisible = true;
  }

  hideHistoryDialog() {
    this.historyVisible = false;
  }

  onHistoryMaximize(history: { maximized: any }) {
    if (history.maximized) {
      this.historyRows = 15;
    } else {
      this.historyRows = 6;
    }
  }

  isExpired(expirationDate: Date): boolean {
    console.log('1:' + expirationDate);
    console.log('2:' + Date());
    return expirationDate < new Date();
  }
}
