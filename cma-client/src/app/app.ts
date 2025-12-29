import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  title = 'CMA - Marketing Agency';

  constructor(public authService: AuthService, private router: Router) {}

  logout() {
    this.authService.logout();
  }

  navigateTo(path: string) {
    this.router.navigate([path]);
  }
}
