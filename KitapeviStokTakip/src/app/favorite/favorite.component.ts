import { Component, OnInit } from '@angular/core';
import { Book } from '../models/book';
import { Fav } from '../models/fav';
import { User } from '../models/user';
import { AuthenticationService } from '../services/authentication.service';
import { UserBookService } from '../services/user-book.service';
declare let alertify: any;

@Component({
  selector: 'app-favorite',
  templateUrl: './favorite.component.html',
  styleUrls: ['./favorite.component.css'],
})
export class FavoriteComponent implements OnInit {
  user: User;
  favoriteBooks: Book[] = [];
  fav: Fav = { book: null, user: null };
  constructor(
    private userBookService: UserBookService,
    private authService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.authService.user.subscribe((user) => (this.user = user));

    this.userBookService
      .getFavBooksByUserId(this.user.userId)
      .subscribe((f) => (this.favoriteBooks = f));

  }

  deleteFavorite(book) {
    this.fav = { book: book, user: this.user };
    this.userBookService
      .deleteSingleFavorite(this.fav)
      .subscribe(() => this.ngOnInit());
    alertify.error('Silindi')
  }
}
