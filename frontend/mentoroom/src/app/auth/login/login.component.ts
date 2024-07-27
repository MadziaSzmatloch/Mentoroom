import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  signinForm: FormGroup;
  isLoading = false;
  errorMessage: string = null;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.signinForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, Validators.required),
    });
  }

  onSubmit() {
    const email = this.signinForm.value.email;
    const password = this.signinForm.value.password;

    this.isLoading = true;

    this.authService.signIn(email, password).subscribe({
      next: () => {
        this.isLoading = false;
      },
      error: (message) => {
        this.errorMessage = message;
        this.isLoading = false;
      },
    });

    this.signinForm.reset();
  }
}
