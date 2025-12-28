import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-contact',
  imports: [CommonModule, FormsModule],
  templateUrl: './contact.html',
  styleUrl: './contact.scss',
})
export class Contact {
  contactForm = {
    name: '',
    email: '',
    subject: '',
    message: ''
  };
  submitted = false;

  onSubmit() {
    console.log('Contact form submitted:', this.contactForm);
    this.submitted = true;
    // In a real app, this would send to the backend
    setTimeout(() => {
      this.submitted = false;
      this.contactForm = { name: '', email: '', subject: '', message: '' };
    }, 3000);
  }
}
