import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [CommonModule, RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {
  features = [
    {
      title: 'Wide Product Range',
      description: 'Browse our extensive collection of motorcycles and accessories',
      icon: '🏍️'
    },
    {
      title: 'Trusted Brands',
      description: 'Premium brands like Honda, Yamaha, and TVS',
      icon: '⭐'
    },
    {
      title: 'Expert Support',
      description: '24/7 customer support for all your needs',
      icon: '🤝'
    },
    {
      title: 'Fast Delivery',
      description: 'Quick and reliable delivery across all regions',
      icon: '🚚'
    }
  ];
}
