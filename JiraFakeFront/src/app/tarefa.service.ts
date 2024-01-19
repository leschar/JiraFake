import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Tarefa } from './tarefa/tarefa';

@Injectable({
  providedIn: 'root',
})
export class TarefaService {
  private apiUrl = 'https://localhost:7212/api/';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(private httpClient: HttpClient) {}

  //get
  getAll(): Observable<any> {
    return this.httpClient.get(this.apiUrl + 'tarefa/').pipe(
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  //find data
  find(id: string): Observable<any> {
    return this.httpClient.get(this.apiUrl + 'tarefa/detalhes/' + id).pipe(
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
  //create
  create(post: Tarefa): Observable<any> {
    return this.httpClient
      .post(this.apiUrl + 'tarefa/', JSON.stringify(post), this.httpOptions)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })
      );
  }
  //edit
  update(tarefa: Tarefa): Observable<any> {
    return this.httpClient
      .put(this.apiUrl + 'tarefa/', JSON.stringify(tarefa), this.httpOptions)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })
      );
  }

  //delete
  delete(id: string) {
    return this.httpClient.delete(this.apiUrl + 'tarefa?id=' + id).pipe(
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    );
  }
}
