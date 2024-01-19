import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TarefaService } from '../../tarefa.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-nova-tarefa',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './nova-tarefa.component.html',
  styleUrl: './nova-tarefa.component.css',
})
export class NovaTarefaComponent {
  form!: FormGroup;

  constructor(
    public postService: TarefaService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      nome: new FormControl('', [Validators.required]),
      descricao: new FormControl('', Validators.required),
    });
  }

  get formulario() {
    return this.form.controls;
  }

  submit() {
    this.postService.create(this.form.value).subscribe((res: any) => {
      alert('Tarefa criada com sucesso!');
      this.router.navigateByUrl('tarefa/index');
    });
  }
}
