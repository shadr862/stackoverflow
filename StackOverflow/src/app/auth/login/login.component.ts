import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/authService/auth.service';
import { J } from '@angular/cdk/keycodes';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginObj: any = {
    "EmailId": "",
    "Password": ""
  }

  successMessage: string = '';

  showPassword: boolean = false;


  constructor(private AuthService: AuthService,
    private routerService: Router) {

  }

  login() {
    this.AuthService.Login(this.loginObj.EmailId, this.loginObj.Password).subscribe((response: any) => {
      //alert(JSON.stringify(response))
      //alert(JSON.stringify(this.loginObj))
      if (response != null && response.email === this.loginObj.EmailId && response.passwordHash === this.loginObj.Password) {

        this.AuthService.isLoggedIn = true;
        this.AuthService.userId = response.id;
        this.AuthService.userName = response.displayName;
        this.AuthService.bio=response.bio;

        localStorage.setItem('isLoggedIn', 'true');
        localStorage.setItem('userName', this.AuthService.userName);
        localStorage.setItem('bio',this.AuthService.bio);
        localStorage.setItem('userId', this.AuthService.userId.toString());
        this.routerService.navigateByUrl('dashboard')

      }
      else {
        this.successMessage = "Wrong Credential";

        // Clear the message after 3 seconds (3000 milliseconds)
        setTimeout(() => {
          this.successMessage = "";
        }, 3000);

      }
    });

  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }
}