import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { AuthRequest } from '../cores/models/auth/AuthRequest';
import { AuthResponse } from '../cores/models/auth/AuthResponse';
import { Observable, tap } from 'rxjs'; 
import { TokenModel } from '../cores/models/auth/TokenModel';
import { RegisterRequest } from '../cores/models/auth/RegisterRequest';
import { jwtDecode } from 'jwt-decode';
import { environment } from '../enviroment/environment';
import { Router } from '@angular/router';
import { SetPasswordRequest } from '../cores/models/auth/SetPasswordRequest';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private cookieService = inject(CookieService);
  router = inject(Router);

  token: string | null = null;
  refreshToken: string | null = null;
  
  get isAuth() {
    this.token = this.token || this.cookieService.get('token');
    return !!this.token;
  }
  getToken(): string | null {
  return this.token || this.cookieService.get('token') || null;
}
  login(authRequest: AuthRequest): Observable<AuthResponse> {
    const url = `${environment.authServiceUrl}/auth/login`;
    return this.http.post<AuthResponse>(url, authRequest).pipe(
      tap(val => {
        this.setTokens(val.token, val.refreshToken);
      })
    );
  }

register(registerRequest: RegisterRequest): Observable<any> {
  const url = `${environment.authServiceUrl}/auth/register`;
  return this.http.post(url, registerRequest);
}

  refreshTokens(): Observable<TokenModel> {
    const url = `${environment.authServiceUrl}/auth/refresh-token`;

    if (!this.refreshToken) {
      this.refreshToken = this.cookieService.get('refreshToken');
    }
    if (!this.token) {
      this.token = this.cookieService.get('token');
    }

    const tokenModel: TokenModel = {
      accessToken: this.token,
      refreshToken: this.refreshToken
    };

    return this.http.post<TokenModel>(url, tokenModel).pipe(
      tap(val => {
        this.setTokens(val.accessToken, val.refreshToken);
      })
    );
  }
  getUserEmailFromToken(): string {
  const token = this.token || this.cookieService.get('token');
  if (!token) {
      console.error("No token available");
      return '';
  }

  try {
      const decodedToken: any = jwtDecode(token);
      return decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] || '';
  } catch (error) {
      console.error('Token decoding error', error);
      return '';
  }
}

  getUserIdFromToken(): string {
    const token = this.token || this.cookieService.get('token');
    if (!token) {
        console.error("No token available");
        return '';
    }

    try {
        const decodedToken: any = jwtDecode(token);
        return decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"] || '';
    } catch (error) {
        console.error('Token decoding error', error);
        return '';
    }
  }

  getUserRoleFromToken(): string {
    const token = this.token || this.cookieService.get('token');
    if (!token) {
        console.error("No token available");
        return '';
    }
  
    try {
        const decodedToken: any = jwtDecode(token);
        return decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || '';
    } catch (error) {
        console.error('Token decoding error', error);
        return '';
    }
  }
  
  logout(): void {
    this.token = null;
    this.refreshToken = null;
    this.cookieService.delete('token');
    this.cookieService.delete('refreshToken');
    this.router.navigate(["/start"]);
    
  }
  
  private setTokens(token: string, refreshToken: string) {
    this.token = token;
    this.refreshToken = refreshToken;

    this.cookieService.set('token', token);
    this.cookieService.set('refreshToken', refreshToken);
  }
    getAllUsers(): Observable<any[]> {
    const url = `${environment.authServiceUrl}/auth/users`;
    return this.http.get<any[]>(url);
  }

  blockUser(email: string): Observable<any> {
    const url = `${environment.authServiceUrl}/auth/${email}/block`;
    return this.http.post(url, {});
  }

  deleteUser(email: string): Observable<void> {
    const url = `${environment.authServiceUrl}/auth/${email}`;
    return this.http.delete<void>(url);
  }

  revoke(username: string): Observable<any> {
    const url = `${environment.authServiceUrl}/auth/revoke/${username}`;
    return this.http.post(url, {});
  }

  revokeAll(): Observable<any> {
    const url = `${environment.authServiceUrl}/auth/revoke-all`;
    return this.http.post(url, {});
  }

  setPassword(request: SetPasswordRequest): Observable<void> {
    const url = `${environment.authServiceUrl}/auth/set-password`;
    return this.http.post<void>(url, request);
  }
}