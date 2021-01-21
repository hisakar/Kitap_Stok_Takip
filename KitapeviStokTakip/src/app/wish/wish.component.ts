import { Component, OnInit } from '@angular/core';
import { Demand } from '../models/demand';
import { UserBookService } from '../services/user-book.service';

@Component({
  selector: 'app-wish',
  templateUrl: './wish.component.html',
  styleUrls: ['./wish.component.css']
})
export class WishComponent implements OnInit {
  
  demands: Demand[];

  constructor(private userBookService:UserBookService) { }

  ngOnInit(): void {
    this.userBookService.getAllDemands().subscribe(d=>this.demands=d)
  }

}
