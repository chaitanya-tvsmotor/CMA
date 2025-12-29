import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../../services/product.service';
import { Product, CreateProductDto, UpdateProductDto } from '../../../models';

@Component({
  selector: 'app-products-management',
  imports: [CommonModule, FormsModule],
  templateUrl: './products-management.html',
  styleUrl: './products-management.scss',
})
export class ProductsManagement implements OnInit {
  products: Product[] = [];
  loading = false;
  showModal = false;
  editMode = false;
  currentProduct: CreateProductDto | UpdateProductDto = this.getEmptyProduct();

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.loading = true;
    this.productService.getAll().subscribe({
      next: (products) => {
        this.products = products;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading products:', err);
        this.loading = false;
      }
    });
  }

  getEmptyProduct(): CreateProductDto {
    return {
      name: '',
      description: '',
      imageUrl: '',
      price: 0,
      stockQuantity: 0,
      categoryId: 1,
      brandId: 1,
      specifications: '',
      sku: '',
      barcode: '',
      length: 0,
      width: 0,
      height: 0,
      weight: 0
    };
  }

  openCreateModal() {
    this.editMode = false;
    this.currentProduct = this.getEmptyProduct();
    this.showModal = true;
  }

  openEditModal(product: Product) {
    this.editMode = true;
    this.currentProduct = {
      id: product.id,
      name: product.name,
      description: product.description,
      imageUrl: product.imageUrl || '',
      price: product.price || 0,
      stockQuantity: product.stockQuantity,
      categoryId: product.categoryId,
      brandId: product.brandId,
      specifications: product.specifications || '',
      sku: product.sku || '',
      barcode: product.barcode || '',
      length: product.length || 0,
      width: product.width || 0,
      height: product.height || 0,
      weight: product.weight || 0
    };
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
    this.currentProduct = this.getEmptyProduct();
  }

  saveProduct() {
    if (this.editMode && 'id' in this.currentProduct) {
      this.productService.update(this.currentProduct.id, this.currentProduct as UpdateProductDto).subscribe({
        next: () => {
          this.loadProducts();
          this.closeModal();
          alert('Product updated successfully!');
        },
        error: (err) => console.error('Error updating product:', err)
      });
    } else {
      this.productService.create(this.currentProduct as CreateProductDto).subscribe({
        next: () => {
          this.loadProducts();
          this.closeModal();
          alert('Product created successfully!');
        },
        error: (err) => console.error('Error creating product:', err)
      });
    }
  }

  deleteProduct(id: number) {
    if (confirm('Are you sure you want to delete this product?')) {
      this.productService.delete(id).subscribe({
        next: () => {
          this.loadProducts();
          alert('Product deleted successfully!');
        },
        error: (err) => console.error('Error deleting product:', err)
      });
    }
  }
}
