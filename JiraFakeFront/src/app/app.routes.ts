import { Routes } from '@angular/router';
import { IndexComponent } from './tarefa/index/index.component';
import { NovaTarefaComponent } from './tarefa/nova-tarefa/nova-tarefa.component';
import { HomeComponent } from './navegacao/home/home.component';
import { DetalhesComponent } from './tarefa/detalhes/detalhes.component';
import { NovaSubTarefaComponent } from './subtarefa/nova-sub-tarefa/nova-sub-tarefa.component';

export const routes: Routes = [
  //{ path: 'tarefa', redirectTo: 'tarefa/index', pathMatch: 'full' },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'tarefa/index', component: IndexComponent },
  { path: 'tarefa/nova-tarefa', component: NovaTarefaComponent },
  { path: 'tarefa/:tarefaId/detalhes', component: DetalhesComponent },

  { path: 'subtarefa/nova-sub-tarefa', component: NovaSubTarefaComponent },

  { path: 'home', component: HomeComponent },
];
