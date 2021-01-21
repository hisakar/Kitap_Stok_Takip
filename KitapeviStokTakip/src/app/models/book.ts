import { Category } from "./category";
import { Publisher } from "./publisher";

export class Book{
    bookId: number;
    category: Category;
    publisher: Publisher;
    bookName: string;
    writer: string;
    price: number;
    stockAmount: number;
    barcode: string;
    addedDate: string;
}