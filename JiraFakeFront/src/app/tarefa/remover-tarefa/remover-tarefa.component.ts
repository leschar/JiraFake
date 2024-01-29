import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Tarefa } from '../tarefa';
import { EMPTY, catchError } from 'rxjs';
import { TarefaService } from '../tarefa.service';

@Component({
  selector: 'app-remover-tarefa',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, RouterModule],
  templateUrl: './remover-tarefa.component.html',
})
export class RemoverTarefaComponent {
  id!: string;
  tarefa!: Tarefa;
  form!: FormGroup;
  errors: any[] = [];
  statusOptions = [
    { id: 0, label: 'Fechado' },
    { id: 1, label: 'Aberto' },
    { id: 2, label: 'Para Fazer' },
    { id: 3, label: 'Em Progresso' },
    { id: 4, label: 'Em Testes' },
    { id: 5, label: 'Testes Finalizados' },
    { id: 6, label: 'Concluído' },
  ];

  constructor(
    public tarefaService: TarefaService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.submit = this.submit.bind(this);
  }

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
        this.tarefa = {
          ...data,
          status: this.tarefaService.getStatusLabelById(data.status),
        };
      });
  }

  submit() {
    this.tarefaService.delete(this.id).subscribe(
      (res: any) => {
        alert('Excluido com sucesso');
        this.router.navigateByUrl('/tarefa/index');
      },
      (error) => {
        if (error && Array.isArray(error)) {
          this.errors = error;
        } else if (error && error.errors) {
          this.errors = error.errors;
        } else {
          this.errors = [
            'Ocorreu um erro. Por favor, tente novamente mais tarde.',
          ];
        }
      }
    );
  }
}
