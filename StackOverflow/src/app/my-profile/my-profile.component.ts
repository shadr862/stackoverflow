import { Component } from '@angular/core';
import { QuestionAndanswerService } from '../services/questionAndanswerService/question-andanswer.service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/authService/auth.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-my-profile',
  imports: [CommonModule, RouterModule],
  templateUrl: './my-profile.component.html',
  styleUrl: './my-profile.component.css'
})
export class MyProfileComponent {
  questions: any = [];
  userid = '';
  user = {
    fullName: '',
    bio: '',
    avatarUrl: 'https://i.pravatar.cc/150?img=3'
  };

  constructor(private Service: QuestionAndanswerService,
    public AuthService: AuthService) { }

  ngOnInit(): void {
    this.user.bio=localStorage.getItem('bio')!;
    this.user.fullName=localStorage.getItem('userName')!;
    this.userid = localStorage.getItem('userId')!;
    
    this.Service.onRefresh().subscribe(() => {
      this.loadQuestions();
    })
    this.loadQuestions();
  }

  loadQuestions() {
    this.Service.getQuestionByUserId(this.userid).subscribe((data) => {
      this.questions = data;
    })
  }

  deleteQuestion(id: any) {
    this.Service.delteQuestion(id).subscribe(() => {
      this.Service.triggeredRefresh();
    })
  }

}
