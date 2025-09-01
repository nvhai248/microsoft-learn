import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {GameServices} from '../../services/game.services';
import {Game} from '../../interfaces/game';

@Component({
  selector: 'app-game',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'],
})
export class GameComponent implements OnInit {
  games: Game[] = [];
  loading = true;
  error: string | null = null;

  constructor(private gameService: GameServices) {
  }

  ngOnInit(): void {
    this.gameService.getGames().subscribe({
      next: (data) => {
        this.games = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load games';
        this.loading = false;
      }
    });
  }
}
