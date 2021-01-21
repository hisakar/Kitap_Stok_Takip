import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryLog } from '../models/categoryLog';
import { BookLog } from '../models/bookLog';
import { url } from '../shared/url';

@Injectable({
  providedIn: 'root',
})
export class LogService {
  constructor(private http: HttpClient) {}

  getCategoryLogs(): Observable<CategoryLog[]> {
    return this.http.get<CategoryLog[]>(url + 'logs/category');
  }

  deleteCategoryLogById(id): Observable<CategoryLog> {
    return this.http.delete<CategoryLog>(url + `logs/category/${id}`);
  }

  deleteAllCategoryLog(): Observable<CategoryLog> {
    return this.http.delete<CategoryLog>(url + `logs/category`);
  }

  getBookLogs(): Observable<BookLog[]> {
    return this.http.get<BookLog[]>(url + 'logs/book');
  }

  deleteBookLogById(id): Observable<BookLog> {
    return this.http.delete<BookLog>(url + `logs/book/${id}`);
  }

  deleteAllBookLog(): Observable<BookLog> {
    return this.http.delete<BookLog>(url + `logs/book`);
  }
}
