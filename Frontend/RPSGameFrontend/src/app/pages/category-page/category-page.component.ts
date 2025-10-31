import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from '../../services/category-service';
import { Category } from '../../cores/models/Category';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service.service';
import { Roles } from '../product-page/product-page.component';

@Component({
  selector: 'app-category-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './category-page.component.html',
  styleUrls: ['./category-page.component.scss']
})
export class CategoryPageComponent {
  categories: Category[] = [];
  loading = false;
  modalVisible = false;
  form: FormGroup;
  editingCategory: Category | null = null;
  
  userRole: number;

  constructor(
    private categoryService: CategoryService,
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService
  ) {
    this.form = this.fb.group({
      name: ['', Validators.required]
    });

    // Получаем роль пользователя из JWT
    const roleStr = this.authService.getUserRoleFromToken();
    this.userRole = Number(roleStr);

    this.loadCategories();
  }

  private loadCategories() {
    this.loading = true;
    this.categoryService.getAll().subscribe({
      next: data => {
        this.categories = data;
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }

  openModal(category?: Category) {
    if (category) {
      this.editingCategory = category;
      this.form.patchValue({ name: category.name });
    } else {
      this.editingCategory = null;
      this.form.reset();
    }
    this.modalVisible = true;
  }

  closeModal() {
    this.modalVisible = false;
  }

  saveCategory() {
    if (this.form.invalid) return;

    const cat: Category = {
      id: this.editingCategory ? this.editingCategory.id : '',
      name: this.form.value.name
    };

    const obs = this.editingCategory
      ? this.categoryService.update(cat)
      : this.categoryService.create(cat.name);

    obs.subscribe(() => {
      this.loadCategories();
      this.closeModal();
    });
  }

  deleteCategory(category: Category) {
    if (!confirm(`Delete category "${category.name}"?`)) return;

    this.categoryService.delete(category.id).subscribe(() => this.loadCategories());
  }

  goToProducts() {
    this.router.navigate(['/product']);
  }

  isAdvancedUser(): boolean {
    return this.userRole === Roles.AdvancedUser;
  }
}
