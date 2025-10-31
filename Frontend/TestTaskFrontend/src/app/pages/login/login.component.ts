import { Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth-service.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { EmailPasswordFormComponent } from '../../components/app-email-password-form/email-password-form.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule, EmailPasswordFormComponent], 
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  authService = inject(AuthService);
  router = inject(Router);

  onLogin(data: { email: string; password: string }) {
    console.log('[Login] Attempting login with:', data);

    this.authService.login(data).subscribe({
      next: (res) => {
        console.log('[Login] Login response:', res);

        const token = this.authService.getToken();
        console.log('[Login] Current token:', token);

        const role = this.authService.getUserRoleFromToken();
        console.log('[Login] Role from token:', role);

        if (role === 'Admin') {
          console.log('[Login] Navigating to /user');
          this.router.navigate(['/user']);
        } else if (role === 'User' || role === 'AdvancedUser') {
          console.log('[Login] Navigating to /product');
          this.router.navigate(['/product']);
        } else {
          console.log('[Login] Role unknown, navigating to /');
          this.router.navigate(['/']);
        }

        console.log('[Login] Login successful');
      },
      error: (err: any) => {
        console.error('[Login] Login failed:', err);
      }
    });
  }
}
