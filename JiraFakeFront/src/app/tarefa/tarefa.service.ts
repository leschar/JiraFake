import { Injectable } from '@angular/core';
import { Tarefa } from './tarefa';
import { Observable } from 'rxjs';
import { BaseService } from '../baseservice/base.service';
import { EntityType } from '../uteis/enum';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class TarefaService extends BaseService<Tarefa> {
  getResourceUrl(): string {
    return EntityType.Tarefa;
  }

  //get
  getAll(): Observable<any> {
    return this.httpClient
      .get(`${this.apiUrl}${this.getResourceUrl()}/`)
      .pipe(catchError(this.handleError));
  }
}
