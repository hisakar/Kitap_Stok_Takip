import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Demand } from '../models/demand';
import { User } from '../models/user';
import { AuthenticationService } from '../services/authentication.service';
import { UserBookService } from '../services/user-book.service';
declare let alertify: any;

@Component({
  selector: 'app-demand',
  templateUrl: './demand.component.html',
  styleUrls: ['./demand.component.css'],
})
export class DemandComponent implements OnInit {
  constructor(
    private userBookService: UserBookService,
    private authService: AuthenticationService
  ) {}

  demands: Demand[] = [];
  user: User;
  submitted;
  newDemand: Demand;
  demand: Demand = {
    demandId: 0,
    user: null,
    bookName: null,
    writer: null,
    requestedDate: null,
  };
  ngOnInit(): void {
    this.authService.user.subscribe((user) => (this.user = user));
    this.userBookService
      .getDemandByUserId(this.user.userId)
      .subscribe((d) => (this.demands = d));
  }

  demandForm = new FormGroup({
    bookName: new FormControl('', Validators.required),
    writer: new FormControl('', Validators.required),
  });

  get demandFormCont() {
    return this.demandForm.controls;
  }

  onDemandSubmit() {
    this.submitted = true;
    var u: User;
    this.authService.user.subscribe((user) => (u = user));
    this.newDemand = {
      demandId: 0,
      user: u,
      bookName: this.demandForm.value.bookName,
      writer: this.demandForm.value.writer,
      requestedDate: null,
    };

    if (this.demandForm.invalid) {
      return;
    }

    this.userBookService
      .addDemand(this.newDemand)
      .subscribe((d) => (this.demand = d));

    setTimeout(() => {
      location.reload();
    }, 400);
  }

  cancelDemand(demand: Demand) {
    var u: User;
    this.authService.user.subscribe((user) => (u = user));

    this.demand = {
      demandId: demand.demandId,
      bookName: demand.bookName,
      writer: demand.writer,
      user: u,
      requestedDate: null,
    };
    alertify.confirm(
      'ONAY',
      demand.bookName + ' talebini' + ' Silmeyi onaylayın',
      () =>
      this.userBookService
      .deleteSingleDemand(this.demand)
      .subscribe(() => this.ngOnInit(), alertify.error( ' Silindi')),
      () => alertify.success('İptal Edildi')
    );
  }
}
