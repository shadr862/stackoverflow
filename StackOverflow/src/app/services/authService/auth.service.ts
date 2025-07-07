import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isLoggedIn: boolean = false;
  userId:string='';
  userName:string='';
  bio:string='';

  constructor(private http:HttpClient) { }

  SignUp(User:any)
  {
    return this.http.post<any>('https://localhost:44341/api/app/app-user',User)
  }

  Login(email:string,password:string)
  {
    const url = `https://localhost:44341/api/app/app-user/check-user`;
    const params = { email, password };

    return this.http.post<any>(url, null, { params });

  }

  clear()
  {
    this.isLoggedIn = false;
    this.userId='';
    this.userName='';
    this.bio='';

    localStorage.removeItem('isLoggedIn');
    localStorage.removeItem('userId');
    localStorage.removeItem('userName');
    localStorage.removeItem('bio');
  }
}
