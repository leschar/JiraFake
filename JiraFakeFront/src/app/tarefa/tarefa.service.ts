import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Tarefa } from './tarefa';

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
    return this.httpClient
      .get(this.apiUrl + 'tarefa/')
      .pipe(catchError(this.handleError));
  }
  //find data
  find(id: string): Observable<any> {
    return this.httpClient
      .get(this.apiUrl + 'tarefa/detalhes/' + id)
      .pipe(catchError(this.handleError));
  }
  //create
  create(post: Tarefa): Observable<any> {
    return this.httpClient
      .post(this.apiUrl + 'tarefa/', JSON.stringify(post), this.httpOptions)
      .pipe(catchError(this.handleError));
  }

  //edit
  update(tarefa: Tarefa): Observable<any> {
    return this.httpClient
      .put(this.apiUrl + 'tarefa/', JSON.stringify(tarefa), this.httpOptions)
      .pipe(catchError(this.handleError));
  }

  //delete
  delete(id: string): Observable<any> {
    return this.httpClient
      .delete(this.apiUrl + 'tarefa?id=' + id)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
    } else if (error?.error && error?.error?.errors) {
      return throwError(error?.error?.errors);
    } else {
      console.error(
        `Código do erro: ${error.status}, ` + `mensagem: ${error.error.message}`
      );
    }
    return throwError(
      'Ocorreu um erro. Por favor, tente novamente mais tarde.'
    );
  }
}
