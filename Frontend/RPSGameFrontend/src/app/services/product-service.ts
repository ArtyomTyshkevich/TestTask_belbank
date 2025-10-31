import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../cores/models/Product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'https://localhost:7238/api/product';

  constructor(private http: HttpClient) {}

  // Получить все продукты (без фильтров)
  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);
  }

  // Фильтры продуктов
filter(filter: {
  minPrice?: number | null;
  maxPrice?: number | null;
  categoryId?: string;
  nameStartsWith?: string;
}): Observable<Product[]> {
  let params = new HttpParams();

  if (filter.minPrice != null) {
    params = params.set('minPrice', filter.minPrice.toString());
  }

  if (filter.maxPrice != null) {
    params = params.set('maxPrice', filter.maxPrice.toString());
  }

  if (filter.categoryId) {
    params = params.set('categoryId', filter.categoryId);
  }

  if (filter.nameStartsWith) {
    params = params.set('nameStartsWith', filter.nameStartsWith);
  }

  return this.http.get<Product[]>(`${this.apiUrl}/filter`, { params });
}


  // Создать продукт
  create(product: Omit<Product, 'id' | 'category'> & { categoryId: string }): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }

  // Обновить продукт
  update(
    id: string,
    product: Omit<Product, 'id' | 'category'> & { categoryId: string }
  ): Observable<Product> {
    return this.http.put<Product>(`${this.apiUrl}/${id}`, product);
  }

  // Удалить продукт
  delete(id: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${id}`);
  }
}
