import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterOutlet } from '@angular/router';
import { Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './navegacao/home/home.component';
import { FooterComponent } from './navegacao/footer/footer.component';
import { MenuComponent } from './navegacao/menu/menu.component';
import { IndexComponent } from './tarefa/index/index.component';
import { NovaTarefaComponent } from './tarefa/nova-tarefa/nova-tarefa.component';
import { NovaSubTarefaComponent } from './subtarefa/nova-sub-tarefa/nova-sub-tarefa.component';
import { EditarTarefaComponent } from './tarefa/editar-tarefa/editar-tarefa.component';
import { RemoverTarefaComponent } from './tarefa/remover-tarefa/remover-tarefa.component';
import { EditarSubTarefaComponent } from './subtarefa/editar-sub-tarefa/editar-sub-tarefa.component';
import { RemoverSubTarefaComponent } from './subtarefa/remover-sub-tarefa/remover-sub-tarefa.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    HttpClientModule,
    RouterModule,
    HomeComponent,
    MenuComponent,
    FooterComponent,
    IndexComponent,
    NovaTarefaComponent,
    EditarTarefaComponent,
    RemoverTarefaComponent,
    NovaSubTarefaComponent,
    EditarSubTarefaComponent,
    RemoverSubTarefaComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'Teste lista de tarefas';

  constructor(private router: Router) {}
}
