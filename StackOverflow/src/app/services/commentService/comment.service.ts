import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  private triggerRefesh=new Subject<void>();

  constructor(private http:HttpClient) { }

  RefreshNeeded()
  {
    this.triggerRefesh.next();
  }

  OnRefresh()
  {
    return this.triggerRefesh.asObservable();
  }

  getComments(id:any)
  {
    return this.http.get<any>(`https://localhost:44341/api/app/comment/comments-by-post-id/${id}`);
  }

  postComment(comment:any):Observable<any>
  {
    return this.http.post<any>("https://localhost:44341/api/app/comment", comment);
  }

  deleteComment(id:any)
  {
    return this.http.delete(`https://localhost:44341/api/app/comment/${id}`);
  }
}
