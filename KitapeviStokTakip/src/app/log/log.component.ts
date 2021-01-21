import { Component, OnInit } from '@angular/core';
import { BookLog } from '../models/bookLog';
import { CategoryLog } from '../models/categoryLog';
import { LogService } from '../services/log.service';
declare let alertify: any;

@Component({
  selector: 'app-log',
  templateUrl: './log.component.html',
  styleUrls: ['./log.component.css'],
})
export class LogComponent implements OnInit {
  constructor(private logService: LogService) {}

  p;
  categoryLogs: CategoryLog[];
  bookLogs: BookLog[];

  ngOnInit(): void {
    this.logService.getCategoryLogs().subscribe((l) => (this.categoryLogs = l));
    this.logService.getBookLogs().subscribe((l) => (this.bookLogs = l));
  }

  onDeleteCategorySubmit(log: BookLog) {
    alertify.confirm(
      'ONAY',
      log.logId + ' id\'li kaydı ' + 'Silmeyi onaylayın',
      () =>
      this.logService
      .deleteCategoryLogById(log.logId)
      .subscribe(() => this.ngOnInit(), alertify.error( log.logId +' Silindi')),
      () => alertify.success('İptal Edildi')
    );

  }

  onDeleteAllCategorySubmit() {
    alertify.confirm(
      'ONAY',
      'Tüm Kategori Loglarını Silmeyi onaylayın',
      () =>
      this.logService.deleteAllCategoryLog().subscribe(() => this.ngOnInit(), alertify.error('Tüm Kategori Logları Silindi')),
      () => alertify.success('İptal Edildi')
    );
  }

  onDeleteBookSubmit(log: BookLog) {
    alertify.confirm(
      'ONAY',
     log.logId + ' id\'li kaydı ' +  'Silmeyi Onaylayın',
      () =>
        this.logService
          .deleteBookLogById(log.logId)
          .subscribe(() => this.ngOnInit(), alertify.error(log.logId + ' Silindi')),
      () => alertify.success('İptal Edildi')
    );
  }

  onDeleteAllBookSubmit() {
    alertify.confirm(
      'ONAY',
      'Tüm Kitap Loglarını Silmeyi Onaylayın',
      () =>
      this.logService.deleteAllBookLog().subscribe(() => this.ngOnInit(), alertify.error('Tüm Kitap Logları Silindi')),
      () => alertify.success('İptal Edildi')
    );
  }
}
