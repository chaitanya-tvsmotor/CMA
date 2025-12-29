import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-about',
  imports: [CommonModule],
  templateUrl: './about.html',
  styleUrl: './about.scss',
})
export class About {
  stats = [
    { value: '10+', label: 'Years Experience' },
    { value: '500+', label: 'Happy Dealers' },
    { value: '50+', label: 'Products' },
    { value: '24/7', label: 'Support' }
  ];
}
