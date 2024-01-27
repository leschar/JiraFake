import { Component } from '@angular/core';
import { Tarefa } from '../tarefa';
import { TarefaService } from '../../tarefa.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { catchError, EMPTY } from 'rxjs';
@Component({
  selector: 'app-detalhes',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './detalhes.component.html',
  styleUrls: ['./detalhes.component.css'],
})
export class DetalhesComponent {
  id!: string;
  tarefa!: Tarefa;

  constructor(
    public tarefaService: TarefaService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.id = this.route.snapshot.params['tarefaId'];
    this.tarefaService
      .find(this.id)
      .pipe(
        catchError((error) => {
          if (error.status === 500 || error.status === 404) {
            alert('Tarefa não encontrada');
          } else {
            alert('Erro ao carregar a tarefa.');
          }
          return EMPTY;
        })
      )
      .subscribe((data: Tarefa) => {
        this.tarefa = data;
      });
  }
}
