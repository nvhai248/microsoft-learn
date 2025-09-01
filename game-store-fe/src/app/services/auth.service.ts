import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable, tap} from 'rxjs';
import {BACKEND_URL} from '../const';
import {Login, Register} from '../interfaces/auth';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${BACKEND_URL}/api/Auth`;
  private tokenKey = 'auth_token';

  constructor(private http: HttpClient) {
  }

  // ---------------------
  // Register new user
  // ---------------------
  register(data: Register): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, data);
  }

  // ---------------------
  // Login and store token
  // ---------------------
  login(data: Login): Observable<any> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, data).pipe(
      tap(response => {
        if (response.token) {
          this.setToken(response.token);
        }
      })
    );
  }

  // ---------------------
  // Get profile (JWT required)
  // ---------------------
  getProfile(): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.getToken()}`
    });
    return this.http.get(`${this.apiUrl}/profile`, {headers});
  }

  // ---------------------
  // Token handling
  // ---------------------
  private setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    return !!token;
  }
}
