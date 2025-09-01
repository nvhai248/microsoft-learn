import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {Login, Register} from '../../interfaces/auth';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  // Switch between login/register
  isLoginMode = true;

  // Form models
  loginData: Login = {email: '', password: ''};
  registerData: Register = {email: '', password: '', username: ''};

  // State
  isLoggedIn = false;
  userProfile: any = null;
  errorMessage: string | null = null;

  constructor(private authService: AuthService) {
  }

  ngOnInit(): void {
    if (this.authService.isLoggedIn()) {
      this.loadProfile();
    }
  }

  // ---------------------
  // Toggle form
  // ---------------------
  toggleMode() {
    this.isLoginMode = !this.isLoginMode;
    this.errorMessage = null;
  }

  // ---------------------
  // Handle login/register
  // ---------------------
  onSubmit() {
    if (this.isLoginMode) {
      this.authService.login(this.loginData).subscribe({
        next: () => this.loadProfile(),
        error: (err) => this.errorMessage = err.error || 'Login failed'
      });
    } else {
      this.authService.register(this.registerData).subscribe({
        next: () => {
          this.isLoginMode = true; // switch back to login after register
        },
        error: (err) => this.errorMessage = err.error || 'Registration failed'
      });
    }
  }

  // ---------------------
  // Load profile from API
  // ---------------------
  loadProfile() {
    this.authService.getProfile().subscribe({
      next: (profile) => {
        this.userProfile = profile;
        this.isLoggedIn = true;
      },
      error: () => {
        this.authService.logout();
        this.isLoggedIn = false;
      }
    });
  }

  // ---------------------
  // Logout
  // ---------------------
  logout() {
    this.authService.logout();
    this.isLoggedIn = false;
    this.userProfile = null;
  }
}
