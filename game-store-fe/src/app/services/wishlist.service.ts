import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Wish} from '../interfaces/wishlist';
import {AuthService} from './auth.service';
import {BACKEND_URL} from '../const';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private baseUrl = `${BACKEND_URL}/api/WishList`;

  constructor(private http: HttpClient, private authService: AuthService) {
  }

  // Helper to create headers with token
  private getAuthHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      Authorization: token ? `Bearer ${token}` : ''
    });
  }

  getWishlist(): Observable<Wish[]> {
    return this.http.get<Wish[]>(this.baseUrl, {
      headers: this.getAuthHeaders()
    });
  }

  wishGame(gameId: number): Observable<Wish> {
    return this.http.post<Wish>(`${this.baseUrl}/wish`, {gameId}, {
      headers: this.getAuthHeaders()
    });
  }

  deleteWish(gameId: number): Observable<void> {
    return this.http.request<void>('DELETE', `${this.baseUrl}/delete`, {
      headers: this.getAuthHeaders(),
      body: {gameId}
    });
  }
}
