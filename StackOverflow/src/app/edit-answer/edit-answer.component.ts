import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionAndanswerService } from '../services/questionAndanswerService/question-andanswer.service';

@Component({
  selector: 'app-edit-answer',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit-answer.component.html',
  styleUrl: './edit-answer.component.css'
})
export class EditAnswerComponent {
  answerForm!: FormGroup;
  postId!: string;
  answerId!: string;
  appUserId!: string;
  userName!: string;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private qaService: QuestionAndanswerService,
    private router: Router) { }

  ngOnInit(): void {
    this.postId = this.route.snapshot.paramMap.get('postId')!;
    this.answerId = this.route.snapshot.paramMap.get('answerId')!;
    this.appUserId = localStorage.getItem('userId')!;
    this.userName = localStorage.getItem('userName')!;

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
      name: this.userName,
      answerText: this.answerForm.value.answerText,
      created: bangladeshTime.toISOString(),
    };

    this.qaService.editAnswer(answerDto, this.answerId).subscribe(() => {
      this.answerForm.reset();
      this.router.navigateByUrl(`/dashboard/answerList/${this.postId}`);
    });
  }

}
