import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { SubTarefaService } from '../../subtarefa.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-nova-sub-tarefa',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule, FormsModule],
  templateUrl: './nova-sub-tarefa.component.html',
  styleUrl: './nova-sub-tarefa.component.css',
})
export class NovaSubTarefaComponent {
  form!: FormGroup;
  id!: string;
  nome!: string;
  nomeTarefa!: string;
  descricao!: string;

  constructor(
    public postService: SubTarefaService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      tarefaId: new FormControl(''),
      nome: new FormControl('', [Validators.required]),
      descricao: new FormControl('', Validators.required),
    });

    this.id = this.route.snapshot.params['tarefaId'];
    this.nomeTarefa = this.route.snapshot.params['tarefaNome'];
  }

  get formulario() {
    return this.form.controls;
  }

  submit() {
    console.log('----------', this.form.value);
    this.postService.create(this.form.value).subscribe((res: any) => {
      alert('Sub Tarefa criada com sucesso!');
    });
  }
}
