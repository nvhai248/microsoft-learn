import {Component, signal} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {NavMenuComponent} from './components/nav-menu.component/nav-menu.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavMenuComponent],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('game-store-fe');
}
