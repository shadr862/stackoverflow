import { Component, OnInit } from '@angular/core';
import { QuestionAndanswerService } from '../services/questionAndanswerService/question-andanswer.service';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Vote } from './vote.model';
import { comment } from './comment.model';
import { AuthService } from '../services/authService/auth.service';
import { VoteService } from '../services/voteService/vote.service';
import { FormsModule } from '@angular/forms';
import { CommentService } from '../services/commentService/comment.service';

@Component({
  selector: 'app-detail-question',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './detail-question.component.html',
  styleUrl: './detail-question.component.css'
})
export class DetailQuestionComponent implements OnInit {

  questionId!: string;
  isLoggdin = false;
  userId!: string;
  question: any;
  totalUpvote = 0;
  totalDownvote = 0;
  comments: any;
  newCommentText: string = '';


  constructor(private Service: QuestionAndanswerService,
    private router: ActivatedRoute,
    private AuthService: AuthService,
    private VoteService: VoteService,
    private CommentService: CommentService) { }


  ngOnInit(): void {

    this.isLoggdin = localStorage.getItem('isLisLoggedIn') === 'true';
    this.userId = localStorage.getItem('userId') ?? '';
    this.questionId = this.router.snapshot.paramMap.get('id') ?? '';
    this.Service.getQuestionById(this.questionId).subscribe((data) => {
      this.question = data;
    })

    this.VoteService.OnRefresh().subscribe(() => {
      this.loadVoteCount();
    })
    this.CommentService.OnRefresh().subscribe(() => {
      this.loadComments();
    })

    this.loadVoteCount();
    this.loadComments();
  }

  loadVoteCount() {
    this.VoteService.getVoteCount(this.questionId).subscribe((data) => {
      this.totalUpvote = data.upvoteCount;
      this.totalDownvote = data.downvoteCount;
    })
  }

  loadComments() {
    this.CommentService.getComments(this.questionId).subscribe((data) => {
      this.comments = data;
    })
  }


  upvote() {
    const vote: Vote = {
      appUserId: localStorage.getItem('userId') ?? '',
      postId: this.questionId,
      voteType: 0
    };

    this.VoteService.postVote(vote).subscribe(() => {
      this.VoteService.RefreshNeeded();
    });
  }

  downvote() {
    const vote: Vote = {
      appUserId: localStorage.getItem('userId') ?? '',
      postId: this.questionId,
      voteType: 1
    };

    this.VoteService.postVote(vote).subscribe(() => {
      this.VoteService.RefreshNeeded();
    });
  }

  unvote() {
    const unvote = {
      postId: this.questionId,
      appUserId: localStorage.getItem('userId')!
    }

    this.VoteService.unVote(unvote.postId, unvote.appUserId).subscribe(() => {
      this.VoteService.RefreshNeeded();
    });
  }


  addComment() {
    const now = new Date();
    const bangladeshOffsetMs = 6 * 60 * 60 * 1000; // 6 hours in ms
    const bangladeshTime = new Date(now.getTime() + bangladeshOffsetMs);

    const cmt: comment = {
      postId: this.questionId,
      appUserId: localStorage.getItem('userId') ?? '',
      name: localStorage.getItem('userName') ?? '',
      commentText: this.newCommentText,
      created: bangladeshTime.toISOString() // Convert to ISO string
    };

    this.CommentService.postComment(cmt).subscribe(() => {
      this.CommentService.RefreshNeeded();
      this.newCommentText = '';
    })

  }

  deleteComment(id: any) {
    this.CommentService.deleteComment(id).subscribe(() => {
      this.CommentService.RefreshNeeded();
    })
  }


}
