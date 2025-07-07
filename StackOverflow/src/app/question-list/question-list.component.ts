import { Component, OnInit } from '@angular/core';
import { QuestionAndanswerService } from '../services/questionAndanswerService/question-andanswer.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService } from '../services/authService/auth.service';

@Component({
  selector: 'app-question-list',
  imports: [CommonModule,RouterModule],
  templateUrl: './question-list.component.html',
  styleUrl: './question-list.component.css'
})
export class QuestionListComponent implements OnInit{
   questions: any = [];
   userid='';

   constructor(private Service:QuestionAndanswerService,
    public AuthService:AuthService){}

   ngOnInit(): void {
    this.userid=localStorage.getItem('userId')!;
     this.Service.onRefresh().subscribe(()=>{
       this.loadQuestions();
     })
     this.loadQuestions();
   }

   loadQuestions()
   {
     this.Service.getQuestion().subscribe((data)=>{
        this.questions=data;
     })
   }

   deleteQuestion(id:any)
   {
      this.Service.delteQuestion(id).subscribe(()=>{
          this.Service.triggeredRefresh();
      })
   }
}
