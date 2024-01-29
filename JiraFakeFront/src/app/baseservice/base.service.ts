import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EMPTY, Observable, catchError, of, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export abstract class BaseService<T> {
  protected apiUrl = 'https://localhost:7212/api/';
  protected httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(protected httpClient: HttpClient) {}

  abstract getResourceUrl(): string;

  //find data
  find(id: string): Observable<any> {
    return this.httpClient
      .get(`${this.apiUrl}${this.getResourceUrl()}/${id}`)
      .pipe(catchError(this.handleError));
  }

  //create
  create(post: T): Observable<any> {
    return this.httpClient
      .post(
        `${this.apiUrl}${this.getResourceUrl()}/`,
        JSON.stringify(post),
        this.httpOptions
      )
      .pipe(catchError(this.handleError));
  }

  //edit
  update(put: T): Observable<any> {
    return this.httpClient
      .put(
        `${this.apiUrl}${this.getResourceUrl()}/`,
        JSON.stringify(put),
        this.httpOptions
      )
      .pipe(catchError(this.handleError));
  }

  //delete
  delete(id: string, contexto?: any): Observable<any> {
    return this.httpClient
      .delete(`${this.apiUrl}${this.getResourceUrl()}?id=${id}`)
      .pipe(catchError((error) => this.handleError(error, contexto)));
  }

  protected handleError(error: HttpErrorResponse, contexto?: any) {
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

  getStatusLabelById(statusId: number): Observable<string> {
    const statusOptions = [
      { id: 0, label: 'Fechado' },
      { id: 1, label: 'Aberto' },
      { id: 2, label: 'Para Fazer' },
      { id: 3, label: 'Em Progresso' },
      { id: 4, label: 'Em Testes' },
      { id: 5, label: 'Testes Finalizados' },
      { id: 6, label: 'Concluído' },
    ];

    const matchingStatus = statusOptions.find(
      (option) => option.id === statusId
    );
    return matchingStatus ? of(matchingStatus.label) : EMPTY;
  }
}
