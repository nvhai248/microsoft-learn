import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Game} from '../interfaces/game';
import {BACKEND_URL} from '../const';

@Injectable({
  providedIn: 'root'
})
export class GameServices {
  private apiUrl = `${BACKEND_URL}/api/Games`;

  constructor(private http: HttpClient) {}

  getGames(): Observable<Game[]> {
    return this.http.get<Game[]>(this.apiUrl);
  }
}
