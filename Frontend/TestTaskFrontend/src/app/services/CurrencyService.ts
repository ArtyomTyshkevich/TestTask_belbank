import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from '../enviroment/environment';

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.authServiceUrl}/api/currency`;

  getUsd(amountInByn: number): Observable<number> {
    if (!amountInByn || amountInByn <= 0) return new Observable<number>(subscriber => subscriber.next(0));

    const params = new HttpParams().set('amountInByn', amountInByn.toString());
    return this.http.get<{ amountInUsd: number }>(`${this.apiUrl}/usd`, { params }).pipe(
      map(res => res.amountInUsd)
    );
  }
}