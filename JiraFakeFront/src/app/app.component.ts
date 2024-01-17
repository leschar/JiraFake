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
    NovaTarefaComponent,
  ],
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'Teste lista de tarefas';

  constructor(private router: Router) {}
}
