export interface Product {
  id: number;
  name: string;
  description: string;
  imageUrl?: string;
  price?: number;
  stockQuantity: number;
  categoryId: number;
  categoryName?: string;
  brandId: number;
  brandName?: string;
  specifications?: string;
  sku?: string;
  barcode?: string;
  length?: number;
  width?: number;
  height?: number;
  weight?: number;
}

export interface Order {
  id: number;
  orderNumber: string;
  orderDate: Date;
  status: string;
  totalAmount: number;
  notes?: string;
  dealerId: string;
  dealerName?: string;
  salesAgentId?: string;
  salesAgentName?: string;
  managerId?: string;
  managerName?: string;
  items: OrderItem[];
}

export interface OrderItem {
  id: number;
  productId: number;
  productName?: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
}

export interface CreateOrderItem {
  productId: number;
  quantity: number;
}

export interface User {
  id: string;
  userName: string;
  email: string;
  firstName: string;
  lastName: string;
  roles: string[];
}

export interface AuthResponse {
  token: string;
  userId: string;
  userName: string;
  email: string;
  roles: string[];
}

export interface LoginRequest {
  userName: string;
  password: string;
}

export interface RegisterRequest {
  userName: string;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phoneNumber?: string;
  address?: string;
  city?: string;
  state?: string;
  postalCode?: string;
  role: string;
  dealershipName?: string;
  licenseNumber?: string;
}

export interface CreateProductDto {
  name: string;
  description: string;
  imageUrl?: string;
  price: number;
  stockQuantity: number;
  categoryId: number;
  brandId: number;
  specifications?: string;
  sku?: string;
  barcode?: string;
  length?: number;
  width?: number;
  height?: number;
  weight?: number;
}

export interface UpdateProductDto extends CreateProductDto {
  id: number;
}
