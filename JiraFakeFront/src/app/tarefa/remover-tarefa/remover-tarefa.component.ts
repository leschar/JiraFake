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
