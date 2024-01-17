import { Component } from '@angular/core';
import { TarefaService } from '../../tarefa.service';
import { Tarefa } from '../tarefa';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { EMPTY } from 'rxjs';

@Component({
  selector: 'app-index',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './index.component.html',
  styleUrl: './index.component.css',
})
export class IndexComponent {
  tarefas: Tarefa[] = [];
  constructor(public tarefaService: TarefaService) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.tarefaService
      .getAll()
      .pipe(
        catchError((error) => {
          console.error('Erro ao carregar as tarefas:', error);
          if (error.status === 500 || error.status === 404) {
            alert('Tarefa não encontrada');
          } else {
            alert('Erro ao carregar a tarefa.');
          }
          return EMPTY;
        })
      )
      .subscribe((data: Tarefa[]) => {
        this.tarefas = data;
        console.log(this.tarefas);
      });
  }
}