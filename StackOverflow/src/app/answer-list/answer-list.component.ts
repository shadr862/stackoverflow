import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { QuestionAndanswerService } from '../services/questionAndanswerService/question-andanswer.service';

@Component({
  selector: 'app-answer-list',
  imports: [CommonModule,RouterModule],
  templateUrl: './answer-list.component.html',
  styleUrl: './answer-list.component.css'
})
export class AnswerListComponent {
  answers: any[] = [];
  postId!: string;
  userId!: string;
  question: any;

  constructor(
    private route: ActivatedRoute,
    private qaService: QuestionAndanswerService
  ) {}

  ngOnInit(): void {
    this.userId = localStorage.getItem('userId')!;
    this.postId = this.route.snapshot.paramMap.get('id')!;
    this.qaService.onRefresh().subscribe(() => {  
      this.loadQuestion();
      this.loadAnswers();
    });
    
    this.loadAnswers();
    this.loadQuestion();
  }

  loadAnswers(): void {
    this.qaService.getAnswerById(this.postId).subscribe((data: any) => {
      this.answers = data;
    });
  }

  loadQuestion(): void {
    this.qaService.getQuestionById(this.postId).subscribe((data: any) => {
      this.question = data;
    });
  }

  acceptAnswer(id:any){
      this.qaService.acceptAnswer(this.postId, id).subscribe((data:any)=>{
        this.qaService.triggeredRefresh();
      })
  }

  editAnswer(answer:any){

  }

  deleteAnswer(id: any): void {
    this.qaService.deleteAnswer(id).subscribe(() => {
      this.qaService.triggeredRefresh();
    });
  }

}
