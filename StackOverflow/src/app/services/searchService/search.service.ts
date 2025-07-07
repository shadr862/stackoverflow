import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  private searchSubject = new BehaviorSubject<string>('');
  search$ = this.searchSubject.asObservable();



  constructor(private http: HttpClient) { }

  setSearch(value: string) {
    this.searchSubject.next(value);
  }

 searchProducts(term: string) {
  const params = new HttpParams().set('keyword', term);
  return this.http.post<any>(
    'https://localhost:44341/api/app/post/search-posts',
    null,         // empty body
    { params }    // keyword as query param
  );
}

}

