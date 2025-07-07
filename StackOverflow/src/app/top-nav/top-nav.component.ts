import { Component, OnInit } from '@angular/core';
import {  Router, RouterLink, RouterOutlet } from '@angular/router';

import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/authService/auth.service';
import { SearchService } from '../services/searchService/search.service';

@Component({
  selector: 'app-top-nav',
  standalone: true,
  imports: [
    RouterLink,
    RouterOutlet,
    ReactiveFormsModule,
    CommonModule
  ],
  templateUrl: './top-nav.component.html',
  styleUrls: ['./top-nav.component.css'],
})
export class TopNavComponent implements OnInit{
  searchfilter = new FormControl('');
  isLoggedIn = false; // Replace with your auth logic
  userAvatarUrl = 'https://i.pravatar.cc/150?img=3'; // Replace with actual user avatar URL
  userName='';

 constructor(public AuthService:AuthService,
   private RouterService:Router,
   private SearchService:SearchService ){}

 ngOnInit(): void {
   this.searchfilter.valueChanges.subscribe(value => {
      this.SearchService.setSearch(value || '');
      this.RouterService.navigate(['/dashboard/search']);
    });
   this.userName = localStorage.getItem('userName') ?? '';
   this.isLoggedIn = localStorage.getItem('isLoggedIn') === 'true';
 }
  onSearch() {
   
  }

  logout() {
    this.isLoggedIn=false;
    this.userName='';
    
    this.AuthService.clear();
    this.RouterService.navigate(['/dashboard']);
  }
}

