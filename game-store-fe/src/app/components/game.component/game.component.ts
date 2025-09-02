import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {GameServices} from '../../services/game.services';
import {Game} from '../../interfaces/game';
import {WishlistService} from '../../services/wishlist.service';

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

  constructor(private gameService: GameServices,
              private  wishlistService: WishlistService) {
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

  wishGame(gameId: number): void {
    this.wishlistService.wishGame(gameId).subscribe({
      next: () => alert('Game added to wishlist!'),
      error: () => alert('Failed to add game to wishlist')
    });
  }
}
