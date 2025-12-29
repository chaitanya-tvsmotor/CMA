import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { OrderService } from '../../../services/order.service';
import { AuthService } from '../../../services/auth.service';
import { Order } from '../../../models';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard implements OnInit {
  orders: Order[] = [];
  loading = false;
  stats = {
    totalOrders: 0,
    pendingOrders: 0,
    completedOrders: 0,
    totalAmount: 0
  };

  constructor(
    private orderService: OrderService,
    public authService: AuthService
  ) {}

  ngOnInit() {
    this.loadOrders();
  }

  loadOrders() {
    this.loading = true;
    this.orderService.getAll().subscribe({
      next: (orders) => {
        this.orders = orders;
        this.calculateStats();
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading orders:', err);
        this.loading = false;
      }
    });
  }

  calculateStats() {
    this.stats.totalOrders = this.orders.length;
    this.stats.pendingOrders = this.orders.filter(o => 
      o.status === 'Draft' || o.status === 'PendingApproval'
    ).length;
    this.stats.completedOrders = this.orders.filter(o => o.status === 'Completed').length;
    this.stats.totalAmount = this.orders.reduce((sum, o) => sum + o.totalAmount, 0);
  }

  getStatusClass(status: string): string {
    const statusMap: {[key: string]: string} = {
      'Draft': 'status-draft',
      'PendingApproval': 'status-pending',
      'Approved': 'status-approved',
      'Completed': 'status-completed',
      'Rejected': 'status-rejected',
      'Cancelled': 'status-cancelled'
    };
    return statusMap[status] || '';
  }
}
