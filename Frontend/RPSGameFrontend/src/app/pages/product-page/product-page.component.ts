import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../../services/product-service';
import { CategoryService } from '../../services/category-service';
import { CurrencyService } from '../../services/CurrencyService';
import { AuthService } from '../../services/auth-service.service';
import { Product } from '../../cores/models/Product';
import { Category } from '../../cores/models/Category';
import { debounceTime } from 'rxjs/operators';
import { Router } from '@angular/router';

export enum Roles {
  Admin = 0,
  User = 1,
  AdvancedUser = 2,
  Blocked = 3
}

@Component({
  selector: 'app-product-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.scss']
})
export class ProductPageComponent implements OnInit {
  products: Product[] = [];
  categories: Category[] = [];
  loading = false;

  filterForm: FormGroup;

  modalVisible = false;
  form: FormGroup;
  editingProduct: Product | null = null;
  usdModalPrice: number | null = null;
  showUsdTooltip: string | null = null;
  usdPriceMap: Record<string, number> = {};

  userRole: Roles | null = null;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private currencyService: CurrencyService,
    private authService: AuthService,
    private fb: FormBuilder,
     private router: Router
  ) {
    this.filterForm = this.fb.group({
      nameStartsWith: [''],
      minPrice: [null],
      maxPrice: [null],
      categoryId: ['']
    });

    this.form = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      priceRub: [0, [Validators.required, Validators.min(0)]],
      commonNote: [''],
      specialNote: [''],
      categoryId: ['', Validators.required]
    });
  }
  goToCategories() {
    this.router.navigate(['/category']);
  }
  ngOnInit() {
    this.loadCategories();
    this.loadProducts();
    this.userRole = this.getUserRole();

    this.filterForm.valueChanges.pipe(debounceTime(300)).subscribe(() => this.loadProducts());
  }

  private getUserRole(): Roles | null {
    const roleStr = this.authService.getUserRoleFromToken();
    return roleStr === 'AdvancedUser' ? Roles.AdvancedUser : null;
  }

  private loadCategories() {
    this.categoryService.getAll().subscribe({
      next: cats => this.categories = cats
    });
  }

  private loadProducts() {
    this.loading = true;
    const filters = this.filterForm.value;

    this.productService.filter({
      nameStartsWith: filters.nameStartsWith,
      minPrice: filters.minPrice,
      maxPrice: filters.maxPrice,
      categoryId: filters.categoryId
    }).subscribe({
      next: data => {
        this.products = data;
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }

  openModal(product?: Product) {
    if (product) {
      this.editingProduct = product;
      this.form.patchValue({
        name: product.name,
        description: product.description,
        priceRub: product.priceRub,
        commonNote: product.commonNote,
        specialNote: product.specialNote,
        categoryId: product.category.id
      });
      this.onPriceChange(product.priceRub);
    } else {
      this.editingProduct = null;
      this.form.reset();
      this.usdModalPrice = null;
    }
    this.modalVisible = true;
  }

  closeModal() {
    this.modalVisible = false;
  }

  saveProduct() {
    if (this.form.invalid) return;

    const formValue = this.form.value;
    const productData = {
      name: formValue.name,
      description: formValue.description,
      priceRub: formValue.priceRub,
      commonNote: formValue.commonNote,
      specialNote: formValue.specialNote,
      categoryId: formValue.categoryId
    };

    const obs = this.editingProduct
      ? this.productService.update(this.editingProduct.id, productData)
      : this.productService.create(productData);

    obs.subscribe(() => {
      this.loadProducts();
      this.closeModal();
    });
  }

  deleteProduct(product: Product) {
    if (!confirm(`Delete product "${product.name}"?`)) return;

    this.productService.delete(product.id).subscribe(() => this.loadProducts());
  }

  hoveredPrice(product: Product) {
    this.showUsdTooltip = product.id;
    if (this.usdPriceMap[product.id] === undefined) {
      this.currencyService.getUsd(product.priceRub).subscribe({
        next: usd => this.usdPriceMap[product.id] = usd
      });
    }
  }

  hideUsdPrice(product: Product) {
    if (this.showUsdTooltip === product.id) {
      this.showUsdTooltip = null;
    }
  }

  onPriceChange(priceRub: number) {
    if (!priceRub || priceRub <= 0) {
      this.usdModalPrice = null;
      return;
    }
    this.currencyService.getUsd(priceRub).subscribe({
      next: usd => this.usdModalPrice = usd,
      error: () => this.usdModalPrice = null
    });
  }
}
