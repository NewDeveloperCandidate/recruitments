export class Book {
  title!: string;
  author!: string;
  isbn!: string;
  tags!: string[];
  availableCount!: number;
}
export function createBook(params: Partial<Book>) {
  return {
    ...params,
  } as Book;
}