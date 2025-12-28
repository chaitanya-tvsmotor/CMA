import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ProductService } from '../../../services/product.service';
import { OrderService } from '../../../services/order.service';
import { Product, CreateOrderItem } from '../../../models';

@Component({
  selector: 'app-create-order',
  imports: [CommonModule, FormsModule],
  templateUrl: './create-order.html',
  styleUrl: './create-order.scss',
})
export class CreateOrder implements OnInit {
  products: Product[] = [];
  selectedItems: Map<number, { product: Product, quantity: number }> = new Map();
  notes = '';
  loading = false;
  submitting = false;
  error: string | null = null;

  constructor(
    private productService: ProductService,
    private orderService: OrderService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.loading = true;
    this.productService.getAll().subscribe({
      next: (products) => {
        this.products = products.filter(p => p.stockQuantity > 0);
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load products';
        this.loading = false;
      }
    });
  }

  addToOrder(product: Product, quantity: number) {
    if (quantity > 0 && quantity <= product.stockQuantity) {
      this.selectedItems.set(product.id, { product, quantity });
    }
  }

  removeFromOrder(productId: number) {
    this.selectedItems.delete(productId);
  }

  get totalAmount(): number {
    let total = 0;
    this.selectedItems.forEach(item => {
      total += (item.product.price || 0) * item.quantity;
    });
    return total;
  }

  submitOrder() {
    if (this.selectedItems.size === 0) {
      this.error = 'Please add at least one product to the order';
      return;
    }

    const orderItems: CreateOrderItem[] = Array.from(this.selectedItems.values()).map(item => ({
      productId: item.product.id,
      quantity: item.quantity
    }));

    this.submitting = true;
    this.error = null;

    this.orderService.create(orderItems, this.notes).subscribe({
      next: (order) => {
        this.submitting = false;
        alert('Order created successfully!');
        this.router.navigate(['/dealer/dashboard']);
      },
      error: (err) => {
        this.submitting = false;
        this.error = 'Failed to create order. Please try again.';
        console.error('Error creating order:', err);
      }
    });
  }
}
