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
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-editar-tarefa',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './editar-tarefa.component.html',
})
export class EditarTarefaComponent {
  form!: FormGroup;
  id!: string;

  constructor(
    public tarefaService: TarefaService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      id: new FormControl(''),
      nome: new FormControl('', [Validators.required]),
      descricao: new FormControl('', Validators.required),
    });
    this.id = this.route.snapshot.params['tarefaId'];
  }

  get formulario() {
    return this.form.controls;
  }
  /*submit() {
    console.log(this.form.valid);

    this.tarefaService.update(this.form.value).subscribe(
      (res: any) => {
        // Log da resposta de sucesso
        console.log('Resposta da API:', res);

        alert('Edição feita com sucesso.');
        this.router.navigateByUrl('tarefa/index');
      },
      (error) => {
        // Log do erro
        console.error('Erro na requisição:', error);
      }
    );
  }*/
  submit() {
    console.log(this.form.valid);

    this.tarefaService.update(this.form.value).subscribe(
      (res: any) => {
        // Log da resposta de sucesso
        console.log('Resposta da API:', res);

        alert('Edição feita com sucesso.');
        this.router.navigateByUrl('tarefa/index');
      },
      (error) => {
        // Log do erro
        console.error('Erro na requisição:', error);

        if (error instanceof HttpErrorResponse) {
          console.error('Status:', error.status);
          console.error('Mensagem:', error.statusText);
          console.error('Detalhes do erro:', error.error);
        }

        // Aqui você pode adicionar lógica adicional para lidar com o erro, se necessário
      }
    );
  }
}
