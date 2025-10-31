import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service.service';
import { RegisterRequest } from '../../cores/models/auth/RegisterRequest';
import { SetPasswordRequest } from '../../cores/models/auth/SetPasswordRequest';
import { Roles } from '../product-page/product-page.component';

@Component({
  selector: 'app-user-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.scss']
})
export class UserPageComponent {
  users: any[] = [];
  loading = false;

  registerModalVisible = false;
  passwordModalVisible = false;

  registerForm: FormGroup;
  passwordForm: FormGroup;

  editingUser: any | null = null;
  currentUserEmail = '';

  rolesEnum = Roles;
  rolesList = Object.keys(Roles)
    .filter(key => isNaN(Number(key)))
    .map(key => ({
      label: key,
      value: Roles[key as keyof typeof Roles]
    }));

  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      nickname: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      passwordConfirm: ['', Validators.required],
      role: [Roles.User, Validators.required]
    });

    this.passwordForm = this.fb.group({
      NewPassword: ['', Validators.required]
    });

    // получаем email из токена
    this.currentUserEmail = this.authService.getUserEmailFromToken();
    this.loadUsers();
  }

  private loadUsers() {
    this.loading = true;
    this.authService.getAllUsers().subscribe({
      next: data => {
        this.users = data;
        this.loading = false;
      },
      error: () => (this.loading = false)
    });
  }

  openRegisterModal() {
    this.registerForm.reset({ role: Roles.User });
    this.registerModalVisible = true;
  }

  closeRegisterModal() {
    this.registerModalVisible = false;
  }

  registerUser() {
    if (this.registerForm.invalid) return;

    const req: RegisterRequest = this.registerForm.value;
    // не меняем токен
    this.authService.register(req).subscribe(() => {
      this.loadUsers();
      this.closeRegisterModal();
    });
  }

  openPasswordModal(user: any) {
    if (user.role === Roles.Blocked || user.email === this.currentUserEmail) return;
    this.editingUser = user;
    this.passwordForm.reset();
    this.passwordModalVisible = true;
  }

  closePasswordModal() {
    this.passwordModalVisible = false;
  }

  setPassword() {
    if (this.passwordForm.invalid || !this.editingUser) return;

    const req: SetPasswordRequest = {
      email: this.editingUser.email,
      NewPassword: this.passwordForm.value.NewPassword
    };

    this.authService.setPassword(req).subscribe(() => {
      this.closePasswordModal();
    });
  }

  blockUser(user: any) {
    if (user.role === Roles.Blocked || user.email === this.currentUserEmail) return;
    if (!confirm(`Block user "${user.nickname}"?`)) return;

    this.authService.blockUser(user.email).subscribe(() => this.loadUsers());
  }

  deleteUser(user: any) {
    if (user.email === this.currentUserEmail) return;
    if (!confirm(`Delete user "${user.nickname}"?`)) return;

    this.authService.deleteUser(user.email).subscribe(() => this.loadUsers());
  }

  goToProducts() {
    this.router.navigate(['/product']);
  }
}
