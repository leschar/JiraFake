import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { EMPTY, catchError } from 'rxjs';
import { SubTarefaService } from '../subtarefa.service';
import { SubTarefa } from '../../tarefa/tarefa';

@Component({
  selector: 'app-remover-sub-tarefa',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, RouterModule],
  templateUrl: './remover-sub-tarefa.component.html',
  styleUrl: './remover-sub-tarefa.component.css',
})
export class RemoverSubTarefaComponent {
  id!: string;
  tarefaId!: string | null;
  subTarefa!: SubTarefa;
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
    public subTarefaService: SubTarefaService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.submit = this.submit.bind(this);
  }

  ngOnInit() {
    this.id = this.route.snapshot.params['subTarefaId'];
    this.tarefaId = this.route.snapshot.params['tarefaId'];
    console.log(this.tarefaId);

    this.subTarefaService
      .find(this.id)
      .pipe(
        catchError((error) => {
          if (error.status === 500 || error.status === 404) {
            alert('Sub Tarefa não encontrada');
          } else {
            alert('Erro ao carregar a sub tarefa.');
          }
          return EMPTY;
        })
      )
      .subscribe((data: SubTarefa) => {
        this.subTarefa = {
          ...data,
          status: this.subTarefaService.getStatusLabelById(data.status),
        };
      });
  }

  submit() {
    debugger;
    this.subTarefaService.delete(this.id).subscribe(
      (res: any) => {
        //abrir caixa de texto para motivo do cancelamento
        alert('Excluido com sucesso');
        this.router.navigateByUrl(`tarefa/${this.tarefaId}/detalhes`);
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
