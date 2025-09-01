import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Game} from '../interfaces/game';

@Injectable({
  providedIn: 'root'
})
export class GameServices {
  private apiUrl = 'http://localhost:5057/api/Games';

  constructor(private http: HttpClient) {}

  getGames(): Observable<Game[]> {
    return this.http.get<Game[]>(this.apiUrl);
  }
}
