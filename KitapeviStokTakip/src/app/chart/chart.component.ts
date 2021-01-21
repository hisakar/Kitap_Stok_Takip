import { Component, OnInit } from '@angular/core';
import { Book } from '../models/book';
import { Chart } from '../models/chart';
import { User } from '../models/user';
import { AuthenticationService } from '../services/authentication.service';
import { UserBookService } from '../services/user-book.service';
declare let alertify: any;

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartComponent implements OnInit {

  user: User;
  chartItems: Book[] = [];
  chart: Chart = { book: null, user: null,orderedDate:null };
  constructor(
    private userBookService: UserBookService,
    private authService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.authService.user.subscribe((user) => (this.user = user));

    this.userBookService
      .getChartItemsByUserId(this.user.userId)
      .subscribe((c) => (this.chartItems = c));

  }

  deleteChartItem(book) {
    this.chart = { book: book, user: this.user,orderedDate:null };
    this.userBookService
      .deleteSingleChartItem(this.chart)
      .subscribe(() => this.ngOnInit());
    alertify.error('Silindi')
  }

}
