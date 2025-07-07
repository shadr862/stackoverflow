import { H } from '@angular/cdk/keycodes';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QuestionAndanswerService {

  private refreshNeeded$ = new Subject<void>();


  constructor(private http: HttpClient) {}

  
  triggeredRefresh()
  {
    this.refreshNeeded$.next();
  }

  onRefresh(): Observable<void> {
    return this.refreshNeeded$.asObservable();
  }


  getTags()
  {
    return this.http.get<any>('https://localhost:44341/api/app/tag');
  }

  createPost(post: any) {

    return this.http.post("https://localhost:44341/api/app/post", post);
  }
  getQuestion()
  {
    return this.http.get<any>('https://localhost:44341/api/app/post');
  }

  getQuestionById(id:any){
    return this.http.get<any>(`https://localhost:44341/api/app/post/${id}`)
  }

  getQuestionByUserId(id:any){
    return this.http.get<any>(`https://localhost:44341/api/app/post/posts-by-user-id/${id}`)
  }

  editQuestion(question:any,id:any)
  {
    return this.http.put<any>(`https://localhost:44341/api/app/post/${id}`,question);
  }

  delteQuestion(id:any)
  {
    return this.http.delete(`https://localhost:44341/api/app/post/${id}`)
  }


  postAnswer(answer:any)
  {
    return this.http.post<any>("https://localhost:44341/api/app/answer",answer)
  }

  getAnswerById(id:any){
    return this.http.get<any>(`https://localhost:44341/api/app/answer/answers-by-post-id/${id}`)
  }

  acceptAnswer(postId:any,answerId:any|null)
  {
    const params=new HttpParams()
    .set('postId',postId)
    .set('acceptedAnswerId',answerId);
   

    return this.http.post<any>(`https://localhost:44341/api/app/post/set-accepted-answer`,null,{params});
  }

  editAnswer(answer:any,id:any)
  {
    return this.http.put<any>(`https://localhost:44341/api/app/answer/${id}`,answer);
  }

  deleteAnswer(id:any)
  {
    return this.http.delete(`https://localhost:44341/api/app/answer/${id}`);
  }

}
