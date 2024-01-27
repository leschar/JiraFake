import { Injectable } from '@angular/core';
import { SubTarefa } from '../tarefa/tarefa';
import { BaseService } from '../baseservice/base.service';
import { EntityType } from '../uteis/enum';

@Injectable({
  providedIn: 'root',
})
export class SubTarefaService extends BaseService<SubTarefa> {
  getResourceUrl(): string {
    return EntityType.SubTarefa;
  }
}
