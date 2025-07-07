import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VoteService {

  private triggerResfresh=new Subject<void>();

  constructor(private http:HttpClient) { }
  
  RefreshNeeded()
  {
    this.triggerResfresh.next();
  }

  OnRefresh():Observable<void>
  {
    return this.triggerResfresh.asObservable()
  }

  postVote(vote:any)
  {
    return this.http.post<any>("https://localhost:44341/api/app/vote",vote);
  }

  getVoteCount(id:any)
  {
     return this.http.get<any>(`https://localhost:44341/api/app/vote/vote-count-by-post-id/${id}`)
  }

  unVote(postId: string, appUserId: string) {
  const params = new HttpParams()
    .set('postId', postId)
    .set('appUserId', appUserId);

  return this.http.post<any>(
    'https://localhost:44341/api/app/vote/unvote',
    null, // body is empty
    { params }
  );
}


}
