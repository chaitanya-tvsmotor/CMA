import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order, CreateOrderItem } from '../models';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = 'http://localhost:5000/api/orders';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Order[]> {
    return this.http.get<Order[]>(this.apiUrl);
  }

  getById(id: number): Observable<Order> {
    return this.http.get<Order>(`${this.apiUrl}/${id}`);
  }

  create(items: CreateOrderItem[], notes?: string): Observable<Order> {
    return this.http.post<Order>(this.apiUrl, { items, notes });
  }

  assignToSalesAgent(orderId: number, salesAgentId: string): Observable<Order> {
    return this.http.put<Order>(`${this.apiUrl}/${orderId}/assign-sales-agent`, { userId: salesAgentId });
  }

  assignToManager(orderId: number, managerId: string): Observable<Order> {
    return this.http.put<Order>(`${this.apiUrl}/${orderId}/assign-manager`, { userId: managerId });
  }

  updateStatus(orderId: number, status: string): Observable<Order> {
    return this.http.put<Order>(`${this.apiUrl}/${orderId}/status`, { status });
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
