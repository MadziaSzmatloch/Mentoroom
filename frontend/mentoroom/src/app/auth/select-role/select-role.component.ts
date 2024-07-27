import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Observable } from 'rxjs';
import UserData from '../../interfaces/user-data.interface';

@Component({
  selector: 'app-select-role',
  templateUrl: './select-role.component.html',
  styleUrl: './select-role.component.css',
})
export class SelectRoleComponent {
  selectedRole: string | null;
  visible: boolean = false;
  buttonDisabled = true;

  code: number;
  studentIndex: string;

  isLoading = false;
  errorMessage: string | null = null;

  constructor(private authService: AuthService, private router: Router) {}

  selectRole(role: string) {
    if (this.selectedRole === role) {
      this.selectedRole = null;
    } else {
      this.selectedRole = role;
    }
  }

  onContinue() {
    console.log(this.studentIndex);
    this.isLoading = true;
    let obs: Observable<UserData>;
    if (this.selectedRole === 'Lecturer') {
      obs = this.authService.promoteToLecturerWithAccessCode(
        this.code.toString()
      );
    } else if (this.selectedRole === 'Student') {
      obs = this.authService.promoteToStudent(
        this.authService.userID,
        this.studentIndex
      );
    }

    obs.subscribe({
      next: () => {
        this.authService.refreshTokens().subscribe();
        this.router.navigate(['/courses']);
      },
      error: (errorMessage) => {
        console.log(errorMessage);
        this.errorMessage = errorMessage;
      },
    });
    this.isLoading = false;
  }

  codeChanged() {
    if (this.code.toString().length === 9) {
      this.buttonDisabled = false;
    } else {
      this.buttonDisabled = true;
    }
  }

  showModal() {
    this.visible = true;
  }

  hideModal() {
    this.visible = false;
  }

  onHide() {
    this.selectedRole = null;
    this.errorMessage = null;
    this.code = undefined;
    this.studentIndex = undefined;
  }

  onPaste(event: ClipboardEvent) {
    this.code = +event.clipboardData.getData('text');
  }
}
