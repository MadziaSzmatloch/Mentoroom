import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  signupForm: FormGroup;
  isLoading = false;
  errorMessage: string = null;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.signupForm = new FormGroup({
      firstName: new FormControl(null, Validators.required),
      lastName: new FormControl(null, Validators.required),
      email: new FormControl(null, [Validators.required, Validators.email]),
      passwordGroup: new FormGroup(
        {
          password: new FormControl(null, [
            Validators.required,
            Validators.pattern(
              /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$/
            ),
          ]),
          passwordConfirm: new FormControl(null, [
            Validators.required,
            Validators.pattern(
              /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$/
            ),
          ]),
        },
        this.passwordMatchValidator
      ),
    });
  }

  passwordMatchValidator(group: FormGroup): { [s: string]: boolean } {
    return group.get('password').value === group.get('passwordConfirm').value
      ? null
      : { mismatch: true };
  }

  onSubmit() {
    if (!this.signupForm.valid) {
      return;
    }

    const firstName = this.signupForm.value.firstName;
    const lastName = this.signupForm.value.lastName;
    const email = this.signupForm.value.email;
    const password = this.signupForm.get('passwordGroup').value.password;
    const passwordConfirm =
      this.signupForm.get('passwordGroup').value.passwordConfirm;

    this.isLoading = true;

    this.authService
      .signUp(firstName, lastName, email, password, passwordConfirm)
      .subscribe({
        next: () => {
          this.isLoading = false;
        },
        error: (message) => {
          this.errorMessage = message;
          this.isLoading = false;
        },
      });
    this.signupForm.reset();
  }
}
