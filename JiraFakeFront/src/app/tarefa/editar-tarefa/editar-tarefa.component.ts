import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TarefaService } from '../../tarefa.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
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

  constructor(
    public tarefaService: TarefaService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.params['tarefaId'];
    this.tarefaService.find(this.id).subscribe((data: Tarefa) => {
      this.tarefa = data;
      this.form.get('nome')?.setValue(this.tarefa.nome);
      this.form.get('descricao')?.setValue(this.tarefa.descricao);
    });
    this.form = new FormGroup({
      id: new FormControl(''),
      nome: new FormControl('', [Validators.required]),
      descricao: new FormControl('', Validators.required),
    });
  }

  get formulario() {
    return this.form.controls;
  }

  submit() {
    if (this.form.valid) {
      this.tarefaService.update(this.form.value).subscribe(
        (res: any) => {
          alert('Tarefa criada com sucesso!');
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
