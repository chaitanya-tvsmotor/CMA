import { Component } from '@angular/core';
import { CommonModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { RegisterRequest } from '../../../models';

@Component({
  selector: 'app-register',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register {
  registerData: RegisterRequest = {
    userName: '',
    email: '',
    password: '',
    firstName: '',
    lastName: '',
    phoneNumber: '',
    address: '',
    city: '',
    state: '',
    postalCode: '',
    role: 'Dealer',
    dealershipName: '',
    licenseNumber: ''
  };
  
  confirmPassword = '';
  loading = false;
  error: string | null = null;
  
  roles = ['Dealer', 'SalesAgent'];

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onSubmit() {
    if (this.registerData.password !== this.confirmPassword) {
      this.error = 'Passwords do not match';
      return;
    }

    if (this.registerData.password.length < 6) {
      this.error = 'Password must be at least 6 characters long';
      return;
    }

    this.loading = true;
    this.error = null;

    this.authService.register(this.registerData).subscribe({
      next: (response) => {
        this.loading = false;
        // Navigate based on role
        if (response.roles.includes('Dealer')) {
          this.router.navigate(['/dealer/dashboard']);
        } else if (response.roles.includes('SalesAgent')) {
          this.router.navigate(['/sales-agent/orders']);
        } else {
          this.router.navigate(['/home']);
        }
      },
      error: (err) => {
        this.loading = false;
        this.error = err.error?.message || 'Registration failed. Please try again.';
        console.error('Registration error:', err);
      }
    });
  }
}
