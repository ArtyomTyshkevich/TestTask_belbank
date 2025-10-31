import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AuthService } from '../../services/auth-service.service';
import { Roles } from '../enums/Roles';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const allowedRoles: Roles[] = route.data['roles'];
    console.log('[RoleGuard] Allowed roles for this route:', allowedRoles);

    const userRoleString = this.authService.getUserRoleFromToken();
    console.log('[RoleGuard] User role from token (string):', userRoleString);

    const userRole = Roles[userRoleString as keyof typeof Roles];
    console.log('[RoleGuard] User role as enum value:', userRole);

    if (userRoleString === 'Blocked') {
      console.warn('[RoleGuard] User is blocked. Redirecting to /');
      alert('Вы заблокированы');
      this.router.navigate(['/']);
      return false;
    }

    const isAllowed = allowedRoles.includes(userRole);
    console.log('[RoleGuard] Access allowed:', isAllowed);

    if (!isAllowed) {
      console.warn('[RoleGuard] Access denied.');

      if (userRoleString === 'Admin') {
        console.log('[RoleGuard] Redirecting Admin to /user');
        this.router.navigate(['/user']);
      } 
      else if (userRoleString === 'User' || userRoleString === 'AdvancedUser') {
        console.log('[RoleGuard] Redirecting User/AdvancedUser to /product');
        this.router.navigate(['/product']);
      } 
      else {
        console.log('[RoleGuard] Redirecting unknown role to /');
        this.router.navigate(['/']);
      }

      return false;
    }

    return true;
  }
}
