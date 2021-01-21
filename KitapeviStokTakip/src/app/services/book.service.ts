import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ProcessHTTPMsgService } from './process-httpmsg.service.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../models/book';
import { url } from '../shared/url';
import { Category } from '../models/category';
import { Publisher } from '../models/publisher';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  constructor(
    private http: HttpClient,
    private processHTTPMsgService: ProcessHTTPMsgService
  ) {}

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(url + 'categories');
  }

  getCategoryById(id): Observable<Category>{
   return this.http.get<Category>(url + `categories/${id}`);
  }

  addCategory(category: Category) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.http.post<Category>(url + 'categories',category, httpOptions);
  }

  deleteCategory(id): Observable<Category> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.http.delete<Category>(url+`categories/${id}`);
  }

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(url + 'books');
  }

  getBookById(id){
    this.http.get<Book>(url + `books/${id}`);
  }

  addBook(book: Book) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.http.post<Book>(url + 'books',book, httpOptions);
  }

  updateBook(book: Book): Observable<Book> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.http.put<Book>(url + `books`,book,httpOptions);
  }

  deleteBook(id): Observable<Book> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.http.delete<Book>(url+`books/${id}`);
  }

  getPublishers(): Observable<Publisher[]> {
    return this.http.get<Publisher[]>(url + 'publishers');
  }

  getPublisherById(id): Observable<Publisher>{
    return this.http.get<Publisher>(url + `publishers/${id}`);
   }

   addPublisher(publisher: Publisher) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.http.post<Publisher>(url + 'publishers',publisher, httpOptions);
  }

  deletePublisher(id): Observable<Publisher> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.http.delete<Publisher>(url+`publishers/${id}`);
  }
}
