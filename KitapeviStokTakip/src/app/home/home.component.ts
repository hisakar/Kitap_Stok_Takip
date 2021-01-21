import { Component, OnInit } from '@angular/core';
import { Book } from '../models/book';
import { Category } from '../models/category';
import { Chart } from '../models/chart';
import { Fav } from '../models/fav';
import { User } from '../models/user';
import { AuthenticationService } from '../services/authentication.service';
import { BookService } from '../services/book.service';
import { UserBookService } from '../services/user-book.service';
declare let alertify: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  constructor(
    private bookService: BookService,
    private authService: AuthenticationService,
    private userBookService: UserBookService
  ) {}
  categories: Category[] = [];
  books: Book[];
  favoriteBooks: Book[] = [];
  chartItems: Book[] = [];
  errMess: string;

  user: User;
  fav: Fav = { book: null, user: null };
  chart: Chart = { book: null, user: null, orderedDate:null };

  ngOnInit(): void {
    this.bookService.getBooks().subscribe(
      (b) => (this.books = b),
      (errMess) => (this.errMess = <any>errMess)
    );

    this.authService.user.subscribe((user) => (this.user = user));

    this.userBookService
      .getFavBooksByUserId(this.user.userId)
      .subscribe((f) => (this.favoriteBooks = f));

      this.userBookService
      .getChartItemsByUserId(this.user.userId)
      .subscribe((c) => (this.chartItems = c));
    setTimeout(() => {}, 1000);
  }

  addRemoveFavorite(book: Book) {
    try {
      this.fav.book = book;
      this.authService.user.subscribe((user) => (this.fav.user = user));

      if (this.favoriteBooks.some((b) => b.bookId == book.bookId)) {
        this.userBookService
          .deleteSingleFavorite(this.fav)
          .subscribe(() => this.ngOnInit(), alertify.error('Silindi'));
      }else{
        this.userBookService
        .addFavorite(this.fav)
        .subscribe(() => this.ngOnInit(), alertify.success('Eklendi'));
      }

      this.ngOnInit();
    } catch (error) {
      console.log('errr');
    }
  }

  isFav(book: Book) {
    if (this.favoriteBooks.some((b) => b.bookId == book.bookId)) {
      return true;
    }
    return false;
  }

  addRemoveChart(book: Book) {
    try {
      this.chart.book = book;
      this.authService.user.subscribe((user) => (this.chart.user = user));

      if (this.chartItems.some((b) => b.bookId == book.bookId)) {
        this.userBookService
          .deleteSingleChartItem(this.chart)
          .subscribe(() => this.ngOnInit(), alertify.error('Silindi'));
      }else{
        this.userBookService
        .addChartItem(this.chart)
        .subscribe(() => this.ngOnInit(), alertify.success('Eklendi'));
      }

      this.ngOnInit();
    } catch (error) {
      console.log('errr');
    }
  }

  isChart(book: Book) {
    if (this.chartItems.some((b) => b.bookId == book.bookId)) {
      return true;
    }
    return false;
  }
}
