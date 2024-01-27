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
import { SubTarefaService } from '../../subtarefa.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

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
      this.form.get('tarefaId')?.setValue(this.subTarefa.tarefaId);
      this.form.get('nome')?.setValue(this.subTarefa.nome);
      this.form.get('descricao')?.setValue(this.subTarefa.descricao);
    });
    this.form = new FormGroup({
      id: new FormControl(''),
      tarefaId: new FormControl(''),
      nome: new FormControl('', [Validators.required]),
      descricao: new FormControl('', [Validators.required]),
    });
  }
  get formulario() {
    return this.form.controls;
  }
  submit() {
    this.subTarefaService.update(this.form.value).subscribe(
      (res: any) => {
        alert('Edição feita com sucesso.');
        this.router.navigateByUrl(`tarefa/${this.tarefaId}/detalhes`);
      },
      (error) => {
        console.error('Erro na requisição:', error);

        if (error instanceof HttpErrorResponse) {
          console.error('Status:', error.status);
          console.error('Mensagem:', error.statusText);
          console.error('Detalhes do erro:', error.error);
        }
      }
    );
  }
}
