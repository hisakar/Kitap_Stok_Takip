import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Book } from '../models/book';
import { Category } from '../models/category';
import { Publisher } from '../models/publisher';
import { BookService } from '../services/book.service';

declare let alertify: any;

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
})
export class ProductComponent implements OnInit {
  constructor(private bookService: BookService, private router: Router) {}

  addSubmitted = false;
  updateSubmitted = false;
  bookNameSearch: any;
  term;
  p: number = 1;
  showModal;
  critical: number = JSON.parse(localStorage.getItem('critical'));
  books: Book[] = [];
  categories: Category[] = [];
  publishers: Publisher[] = [];
  selectedCategory: Category = null;
  selectedPublisher: Publisher = null;
  selectedCate: Category = null;
  selectedPubl: Publisher = null;
  selectedBookId;

  test;
  book: Book;
  errMess: string;
  newBook: Book;
  newPublisher: Publisher;

  bookForm = new FormGroup({
    bookName: new FormControl('', Validators.required),
    writer: new FormControl('', Validators.required),
    price: new FormControl('', Validators.required),
    stockAmount: new FormControl('', Validators.required),
    barcode: new FormControl('', [
      Validators.required,
      Validators.minLength(13),
    ]),
    categoryId: new FormControl('', Validators.required),
    publisherId: new FormControl('', Validators.required),
  });

  updateForm = new FormGroup({
    bookName: new FormControl('', Validators.required),
    writer: new FormControl('', Validators.required),
    price: new FormControl('', Validators.required),
    stockAmount: new FormControl('', Validators.required),
    barcode: new FormControl('', [
      Validators.required,
      Validators.minLength(13),
    ]),
    categoryId: new FormControl('', Validators.required),
    publisherId: new FormControl('', Validators.required),
  });

  ngOnInit(): void {
    this.bookService.getCategories().subscribe((c) => (this.categories = c));
    this.bookService.getPublishers().subscribe((p) => (this.publishers = p));
    localStorage.setItem('critical', '50');

    this.bookService.getBooks().subscribe(
      (b) => (this.books = b),
      (errMess) => (this.errMess = <any>errMess)
    );
  }

  get bookFormCont() {
    return this.bookForm.controls;
  }

  get updateFormCont() {
    return this.updateForm.controls;
  }

  setCritical(cr: number) {
    localStorage.setItem('critical', cr.toString());
    this.critical = JSON.parse(localStorage.getItem('critical'));
  }

  onEdit(book: Book) {
    if (book) {
      this.bookService
        .getCategoryById(book.category.categoryId)
        .subscribe((c) => (this.selectedCategory = c));
      this.bookService
        .getPublisherById(book.publisher.publisherId)
        .subscribe((p) => (this.selectedPublisher = p));
      this.selectedBookId = book.bookId;
    }

    this.updateForm = new FormGroup({
      bookName: new FormControl(book.bookName, Validators.required),
      writer: new FormControl(book.writer, Validators.required),
      price: new FormControl(book.price, Validators.required),
      stockAmount: new FormControl(book.stockAmount, Validators.required),
      barcode: new FormControl(book.barcode, [
        Validators.required,
        Validators.minLength(13),
      ]),
      categoryId: new FormControl(
        book.category.categoryId,
        Validators.required
      ),
      publisherId: new FormControl(
        book.publisher.publisherId,
        Validators.required
      ),
    });

    this.showModal = true;
  }

  Search() {
    if (this.bookNameSearch == '') {
      this.ngOnInit();
    } else {
      this.books = this.books.filter((res) => {
        console.log(this.bookNameSearch);
        return res.bookName
          .toLocaleLowerCase()
          .match(this.bookNameSearch.toLocaleLowerCase());
      });
    }
  }

  onAddSubmit() {
    this.addSubmitted = true;

    var category: Category = {
      categoryId: parseInt(this.bookForm.value.categoryId),
      categoryName: null,
    };

    var publisher: Publisher = {
      publisherId: parseInt(this.bookForm.value.publisherId),
      publisherName: null,
    };

    this.newBook = {
      bookId: 0,
      publisher: publisher,
      bookName: this.bookForm.value.bookName,
      writer: this.bookForm.value.writer,
      barcode: this.bookForm.value.barcode,
      price: parseInt(this.bookForm.value.price),
      stockAmount: parseInt(this.bookForm.value.stockAmount),
      category: category,
      addedDate: null,
    };

    if (this.bookForm.invalid) {
      return;
    }

    this.newBook.addedDate = new Date().toISOString();
    this.bookService.addBook(this.newBook).subscribe(
      (b) => (this.newBook = b),
      () => this.ngOnInit()
    );
    alertify.success(this.newBook.bookName + ' Eklendi');
    setTimeout(() => {
      this.ngOnInit();
    }, 1000);
  }

  onUpdateSubmit() {
    this.updateSubmitted = true;

    var category: Category = {
      categoryId: parseInt(this.updateForm.value.categoryId),
      categoryName: null,
    };

    var publisher: Publisher = {
      publisherId: parseInt(this.updateForm.value.publisherId),
      publisherName: null,
    };

    this.newBook = {
      bookId: this.selectedBookId,
      bookName: this.updateForm.value.bookName,
      writer: this.updateForm.value.writer,
      barcode: this.updateForm.value.barcode,
      price: parseInt(this.updateForm.value.price),
      stockAmount: parseInt(this.updateForm.value.stockAmount),
      category: category,
      publisher: publisher,
      addedDate: null,
    };

    if (this.updateForm.invalid) {
      console.log('errrrr');
      return;
    }
    console.log(this.newBook);
    this.bookService.updateBook(this.newBook).subscribe(
      (b) => (this.newBook = b),
      () => this.ngOnInit()
    );
    alertify.success(this.newBook.bookName + ' Güncellendi');
    setTimeout(() => {
      this.ngOnInit();
    }, 100);

    this.showModal = false;
  }

  onDeleteSubmit(book: Book) {
    alertify.confirm(
      'ONAY',
      book.bookName + "'i" + ' Silmeyi onaylayın',
      () =>
        this.bookService
          .deleteBook(book.bookId)
          .subscribe(
            () => this.ngOnInit(),
            alertify.error(book.bookName + ' Silindi')
          ),
      () => alertify.success('İptal Edildi')
    );
  }
}
