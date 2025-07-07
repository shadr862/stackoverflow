import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionAndanswerService } from '../services/questionAndanswerService/question-andanswer.service';
import { AuthService } from '../services/authService/auth.service';

@Component({
  selector: 'app-post-answer',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './post-answer.component.html',
  styleUrl: './post-answer.component.css'
})
export class PostAnswerComponent {
  answerForm!: FormGroup;
  postId!: string;
  appUserId: string = 'your-user-id'; // Get from AuthService
  userName: string = 'Your Name';     // Get from AuthService

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private qaService: QuestionAndanswerService,
    private router: Router) { }

  ngOnInit(): void {
    this.postId = this.route.snapshot.paramMap.get('id')!;
    this.appUserId = localStorage.getItem('userId')!;

    this.answerForm = this.fb.group({
      answerText: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.answerForm.invalid) return;

    const now = new Date();
    const bangladeshOffsetMs = 6 * 60 * 60 * 1000; // 6 hours in ms
    const bangladeshTime = new Date(now.getTime() + bangladeshOffsetMs);

    const answerDto = {
      postId: this.postId,
      appUserId: this.appUserId,
      name: localStorage.getItem('userName'),
      answerText: this.answerForm.value.answerText,
      created: bangladeshTime.toISOString()
    };

    this.qaService.postAnswer(answerDto).subscribe(() => {
      this.answerForm.reset();
      this.router.navigateByUrl(`/dashboard/questionList/details/${this.postId}`);
    });
  }

}
