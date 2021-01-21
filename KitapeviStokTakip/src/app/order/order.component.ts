import { Component, OnInit } from '@angular/core';
import { Chart } from '../models/chart';
import { UserBookService } from '../services/user-book.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css'],
})
export class OrderComponent implements OnInit {

  charts: Chart[] = [];

  constructor(private UserBookService: UserBookService) {}

  ngOnInit(): void {
    this.UserBookService.getAllCharts().subscribe((c) => (this.charts = c));
  }
}
