import { Component } from '@angular/core';
import { Tarefa } from '../tarefa';
import { TarefaService } from '../../tarefa.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-detalhes',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './detalhes.component.html',
  styleUrls: ['./detalhes.component.css'],
})
export class DetalhesComponent {
  id!: string;
  tarefa!: Tarefa;

  constructor(
    public tarefaService: TarefaService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.id = this.route.snapshot.params['tarefaId'];
    this.tarefaService.find(this.id).subscribe((data: Tarefa) => {
      this.tarefa = data;
      console.log(this.tarefa);
    });
  }
}
