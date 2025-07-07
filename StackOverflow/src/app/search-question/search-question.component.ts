import { Component } from '@angular/core';
import { QuestionAndanswerService } from '../services/questionAndanswerService/question-andanswer.service';
import { AuthService } from '../services/authService/auth.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SearchService } from '../services/searchService/search.service';

@Component({
  selector: 'app-search-question',
  imports: [CommonModule,RouterModule],
  templateUrl: './search-question.component.html',
  styleUrl: './search-question.component.css'
})
export class SearchQuestionComponent {
    questions: any = [];
    userid='';
    searchTerm!:string

  
    constructor(private Service: QuestionAndanswerService,
      public AuthService: AuthService,
      private SearchService:SearchService) { }
  
    ngOnInit(): void {
      this.userid=localStorage.getItem('userId')!;

      this.SearchService.search$.subscribe((val)=>{
        this.searchTerm=val;
        
        this.SearchService.searchProducts(this.searchTerm).subscribe(results => {
        this.questions = results;
        });
    })
  }
  
    
    deleteQuestion(id: any) {
      this.Service.delteQuestion(id).subscribe(() => {
        this.Service.triggeredRefresh();
      })
    }

}
