import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  credentials = {
    userName: '',
    password: ''
  };
  loading = false;
  error: string | null = null;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onSubmit() {
    if (!this.credentials.userName || !this.credentials.password) {
      this.error = 'Please enter username and password';
      return;
    }

    this.loading = true;
    this.error = null;

    this.authService.login(this.credentials).subscribe({
      next: (response) => {
        this.loading = false;
        // Navigate based on user role
        if (response.roles.includes('Supervisor')) {
          this.router.navigate(['/supervisor/products']);
        } else if (response.roles.includes('Dealer')) {
          this.router.navigate(['/dealer/dashboard']);
        } else if (response.roles.includes('Manager')) {
          this.router.navigate(['/manager/dashboard']);
        } else if (response.roles.includes('SalesAgent')) {
          this.router.navigate(['/sales-agent/orders']);
        } else {
          this.router.navigate(['/home']);
        }
      },
      error: (err) => {
        this.loading = false;
        this.error = 'Invalid username or password';
        console.error('Login error:', err);
      }
    });
  }
}
