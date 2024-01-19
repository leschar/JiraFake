import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TarefaService } from '../../tarefa.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Tarefa } from '../tarefa';
import { EMPTY, catchError } from 'rxjs';

@Component({
  selector: 'app-remover-tarefa',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './remover-tarefa.component.html',
})
export class RemoverTarefaComponent {
  id!: string;
  tarefa!: Tarefa;
  form!: FormGroup;

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
    this.tarefaService.delete(this.id).subscribe((res: any) => {
      console.log(res);
      alert('Excluido com sucesso');
      this.router.navigateByUrl('tarefa/index');
    });
  }
}
