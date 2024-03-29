import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TarefaService } from '../tarefa.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Tarefa } from '../tarefa';

@Component({
  selector: 'app-editar-tarefa',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule, FormsModule],
  templateUrl: './editar-tarefa.component.html',
})
export class EditarTarefaComponent {
  id!: string;
  nome!: string;
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
  ) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.params['tarefaId'];
    this.tarefaService.find(this.id).subscribe((data: Tarefa) => {
      this.tarefa = data;
      const statusValue = this.tarefa.status;
      this.form = new FormGroup({
        id: new FormControl(this.tarefa.id),
        nome: new FormControl(this.tarefa.nome, [Validators.required]),
        descricao: new FormControl(this.tarefa.descricao, Validators.required),
        status: new FormControl(this.tarefa.status, Validators.required),
      });

      this.form.get('status')?.setValue(statusValue);
    });
  }

  get formulario() {
    return this.form.controls;
  }

  submit() {
    if (this.form.valid) {
      this.tarefaService.update(this.form.value).subscribe(
        (res: any) => {
          alert('Edição feita com sucesso!');
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
}
