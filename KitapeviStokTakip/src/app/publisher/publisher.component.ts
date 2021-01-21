import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Publisher } from '../models/publisher';
import { BookService } from '../services/book.service';

declare let alertify: any;

@Component({
  selector: 'app-publisher',
  templateUrl: './publisher.component.html',
  styleUrls: ['./publisher.component.css']
})
export class PublisherComponent implements OnInit {
  newPublisher:Publisher;
  constructor(private bookService: BookService) {}
  addSubmitted;
  publishers: Publisher[] = [];

  publisherForm = new FormGroup({
    publisherName: new FormControl('', Validators.required)
  });

get publisherFormCont() {
    return this.publisherForm.controls;
  }

  ngOnInit(): void {
    this.bookService.getPublishers().subscribe((c) => (this.publishers = c));
  }

  onAddSubmit(){
    this.addSubmitted = true;

    this.newPublisher = {
     publisherId:0,
     publisherName:this.publisherForm.value.publisherName,
    };

    if (this.publisherForm.invalid) {
      return;
    }

    this.bookService.addPublisher(this.newPublisher).subscribe(
      (b) => (this.newPublisher = b),
      () => this.ngOnInit()
    );
    alertify.success(this.newPublisher.publisherName + ' Eklendi')
    setTimeout(() => {
     this.ngOnInit()
    }, 400);
  }

  onDeleteSubmit(publisher:Publisher){
    alertify.confirm(
      'ONAY',
      publisher.publisherName + '\'i' + ' Silmeyi Onaylayın </br>' + 
     'DİKKAT: ' + publisher.publisherName + '\'i' + ' sildiğinizde bu yayın evine ait kitapları da silmiş olacaksınız.',
      () =>
        this.bookService
          .deletePublisher(publisher.publisherId)
          .subscribe(() => this.ngOnInit(), alertify.error(publisher.publisherName + ' Silindi')),
      () => alertify.success('İptal Edildi')
    );
  }
  
}
