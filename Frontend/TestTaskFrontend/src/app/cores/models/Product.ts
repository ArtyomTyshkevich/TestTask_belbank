import { Category } from './Category';

export interface Product {
  id: string;
  name: string;
  description: string;
  priceRub: number;
  commonNote?: string;
  specialNote?: string;
  category: Category;
}