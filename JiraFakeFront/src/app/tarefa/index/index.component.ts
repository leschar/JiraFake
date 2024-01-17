import { Component } from '@angular/core';
import { TarefaService } from '../../tarefa.service';
import { Tarefa } from '../tarefa';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-index',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './index.component.html',
  styleUrl: './index.component.css',
})
export class IndexComponent {
  tarefas: Tarefa[] = [];
  constructor(public tarefaService: TarefaService) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.tarefaService.getAll().subscribe((data: Tarefa[]) => {
      this.tarefas = data;
      console.log(this.tarefas);
    });
  }
}
