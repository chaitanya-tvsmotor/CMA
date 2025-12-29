import { Routes } from '@angular/router';
import { authGuard, roleGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { 
    path: 'home',
    loadComponent: () => import('./components/public/home/home').then(m => m.Home)
  },
  { 
    path: 'products',
    loadComponent: () => import('./components/public/products/products').then(m => m.Products)
  },
  { 
    path: 'about',
    loadComponent: () => import('./components/public/about/about').then(m => m.About)
  },
  { 
    path: 'contact',
    loadComponent: () => import('./components/public/contact/contact').then(m => m.Contact)
  },
  { 
    path: 'login',
    loadComponent: () => import('./components/auth/login/login').then(m => m.Login)
  },
  { 
    path: 'register',
    loadComponent: () => import('./components/auth/register/register').then(m => m.Register)
  },
  { 
    path: 'dealer/dashboard',
    loadComponent: () => import('./components/dealer/dashboard/dashboard').then(m => m.Dashboard),
    canActivate: [authGuard, roleGuard('Dealer')]
  },
  { 
    path: 'dealer/create-order',
    loadComponent: () => import('./components/dealer/create-order/create-order').then(m => m.CreateOrder),
    canActivate: [authGuard, roleGuard('Dealer')]
  },
  { 
    path: 'supervisor/products',
    loadComponent: () => import('./components/supervisor/products-management/products-management').then(m => m.ProductsManagement),
    canActivate: [authGuard, roleGuard('Supervisor')]
  },
  { path: '**', redirectTo: '/home' }
];
