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
    return this.httpClient
      .post(this.apiUrl + 'subtarefa/', JSON.stringify(post), this.httpOptions)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })
      );
  }

  //edit
  update(put: SubTarefa): Observable<any> {
    return this.httpClient
      .put(this.apiUrl + 'subtarefa/', JSON.stringify(put), this.httpOptions)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })
      );
  }

  //delete
  delete(id: string, tarefaId: string) {
    return this.httpClient
      .delete(this.apiUrl + 'subtarefa?id=' + id + '&tarefaId=' + tarefaId)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })
      );
  }
}
