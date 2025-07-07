import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/authService/auth.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-signup',
  imports: [CommonModule,FormsModule,RouterModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {
  successMessage: string = '';

  signUpObj = {
    DisplayName: '',
    Email: '',
    PasswordHash: '',
    Bio: '',
    Location: ''
  };
  showPassword = false;

  constructor(private AuthService:AuthService){}
  

  togglePassword() {
    this.showPassword = !this.showPassword;
  }

  onSubmit() {
    this.AuthService.SignUp(this.signUpObj).subscribe({
      next: () => {
        this.successMessage = 'Account created successfully!';
        this.signUpObj = {
          DisplayName: '',
          Email: '',
          PasswordHash: '',
          Bio: '',
          Location: ''
        };
      },
      error: () => {
        this.successMessage = ''; // you can also show an error message here
      }
    });
  }

}
