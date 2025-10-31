import { Routes } from '@angular/router';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { CategoryPageComponent } from './pages/category-page/category-page.component';
import { ProductPageComponent } from './pages/product-page/product-page.component';
import { UserPageComponent } from './pages/user-page/user-page.component';
import { RoleGuard } from './cores/guards/RoleGuard';
import { Roles } from './pages/product-page/product-page.component';

export const routes: Routes = [
  { path: '', component: LoginComponent },
  { 
    path: 'category', 
    component: CategoryPageComponent,
    canActivate: [RoleGuard],
    data: { roles: [Roles.User, Roles.AdvancedUser] } 
  },
  { 
    path: 'product', 
    component: ProductPageComponent,
    canActivate: [RoleGuard],
    data: { roles: [Roles.User, Roles.AdvancedUser] }
  },
  { 
    path: 'user', 
    component: UserPageComponent,
    canActivate: [RoleGuard],
    data: { roles: [Roles.Admin] }
  },
];
