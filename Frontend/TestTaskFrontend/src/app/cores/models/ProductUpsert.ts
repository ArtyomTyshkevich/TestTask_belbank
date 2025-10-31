export interface ProductUpsert {
  name: string;
  description: string;
  priceRub: number;
  commonNote?: string;
  specialNote?: string;
  categoryId: string;
}
