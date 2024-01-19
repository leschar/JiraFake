import { Routes } from '@angular/router';
import { IndexComponent } from './tarefa/index/index.component';
import { NovaTarefaComponent } from './tarefa/nova-tarefa/nova-tarefa.component';
import { HomeComponent } from './navegacao/home/home.component';
import { DetalhesComponent } from './tarefa/detalhes/detalhes.component';
import { NovaSubTarefaComponent } from './subtarefa/nova-sub-tarefa/nova-sub-tarefa.component';
import { EditarTarefaComponent } from './tarefa/editar-tarefa/editar-tarefa.component';
import { RemoverTarefaComponent } from './tarefa/remover-tarefa/remover-tarefa.component';
import { EditarSubTarefaComponent } from './subtarefa/editar-sub-tarefa/editar-sub-tarefa.component';
import { RemoverSubTarefaComponent } from './subtarefa/remover-sub-tarefa/remover-sub-tarefa.component';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'tarefa/index', component: IndexComponent },
  { path: 'tarefa/nova-tarefa', component: NovaTarefaComponent },
  { path: 'tarefa/:tarefaId/detalhes', component: DetalhesComponent },
  { path: 'tarefa/:tarefaId/editar-tarefa', component: EditarTarefaComponent },
  {
    path: 'tarefa/:tarefaId/remover-tarefa',
    component: RemoverTarefaComponent,
  },

  { path: 'subtarefa/nova-sub-tarefa', component: NovaSubTarefaComponent },
  { path: 'subtarefa/editar-sub-tarefa', component: EditarSubTarefaComponent },
  {
    path: 'subtarefa/remover-sub-tarefa',
    component: RemoverSubTarefaComponent,
  },

  { path: 'home', component: HomeComponent },
];
