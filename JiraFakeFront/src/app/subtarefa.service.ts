import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { SubTarefa } from './tarefa/tarefa';

@Injectable({
  providedIn: 'root',
})
export class SubTarefaService {
  private apiUrl = 'https://localhost:7212/api/';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(private httpClient: HttpClient) {}

  //create
  create(post: SubTarefa): Observable<any> {
    console.log('-----------------------', JSON.stringify(post));
    return this.httpClient
      .post(this.apiUrl + 'subtarefa/', JSON.stringify(post), this.httpOptions)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })
      );
  }
}
