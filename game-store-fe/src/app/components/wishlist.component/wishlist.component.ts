import {Component, OnInit} from '@angular/core';
import {WishlistService} from '../../services/wishlist.service';
import {Wish} from '../../interfaces/wishlist';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.css']
})
export class WishlistComponent implements OnInit {
  wishlist: Wish[] = [];
  loading = true;
  error: string | null = null;

  constructor(private wishlistService: WishlistService) {
  }

  ngOnInit(): void {
    this.loadWishlist();
  }

  loadWishlist(): void {
    this.loading = true;
    this.wishlistService.getWishlist().subscribe({
      next: data => {
        this.wishlist = data;
        this.loading = false;
      },
      error: err => {
        this.error = 'Failed to load wishlist';
        this.loading = false;
      }
    });
  }

  deleteWish(gameId: number): void {
    this.wishlistService.deleteWish(gameId).subscribe({
      next: () => this.loadWishlist(),
      error: () => alert('Failed to delete wish')
    });
  }
}
