import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { SubTarefa } from '../../tarefa/tarefa';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { SubTarefaService } from '../subtarefa.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-editar-sub-tarefa',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule, FormsModule],
  templateUrl: './editar-sub-tarefa.component.html',
  styleUrl: './editar-sub-tarefa.component.css',
})
export class EditarSubTarefaComponent {
  id!: string;
  tarefaId!: string;
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
  ) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.params['subTarefaId'];
    this.tarefaId = this.route.snapshot.params['tarefaId'];

    this.subTarefaService.find(this.id).subscribe((data: SubTarefa) => {
      this.subTarefa = data;
      const statusValue = this.subTarefa.status;
      this.form.get('tarefaId')?.setValue(this.subTarefa.tarefaId);
      this.form.get('nome')?.setValue(this.subTarefa.nome);
      this.form.get('descricao')?.setValue(this.subTarefa.descricao);
      this.form.get('status')?.setValue(statusValue);

      this.form.get('status')?.setValue(statusValue);
    });
    this.form = new FormGroup({
      id: new FormControl(''),
      tarefaId: new FormControl(''),
      nome: new FormControl('', [Validators.required]),
      descricao: new FormControl('', [Validators.required]),
      status: new FormControl('', Validators.required),
    });
  }
  get formulario() {
    return this.form.controls;
  }

  submit() {
    if (this.form.valid) {
      this.subTarefaService.update(this.form.value).subscribe(
        (res: any) => {
          alert('Edição feita com sucesso.');
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
}
