import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Category } from '../models/category';
import { BookService } from '../services/book.service';
declare let alertify: any;

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css'],
})
export class CategoryComponent implements OnInit {
  newCategory:Category;
  constructor(private bookService: BookService) {}
  addSubmitted;
  categories: Category[] = [];

  categoryForm = new FormGroup({
    categoryName: new FormControl('', Validators.required)
  });

get categoryFormCont() {
    return this.categoryForm.controls;
  }

  ngOnInit(): void {
    this.bookService.getCategories().subscribe((c) => (this.categories = c));
  }

  onAddSubmit(){
    this.addSubmitted = true;

    this.newCategory = {
     categoryId:0,
     categoryName:this.categoryForm.value.categoryName,
    };

    if (this.categoryForm.invalid) {
      return;
    }

    this.bookService.addCategory(this.newCategory).subscribe(
      (b) => (this.newCategory = b),
      () => this.ngOnInit()
    );
    alertify.success(this.newCategory.categoryName + ' Eklendi')
    
    setTimeout(() => {
      this.ngOnInit();
    }, 400);
  }

  onDeleteSubmit(category:Category){
    alertify.confirm(
      'ONAY',
     category.categoryName + '\'i' + ' Silmeyi Onaylayın </br>' + 
     'DİKKAT: ' + category.categoryName + '\'i' + ' sildiğinizde bu kategorideki kitapları da silmiş olacaksınız.'
     ,
      () =>
        this.bookService
          .deleteCategory(category.categoryId)
          .subscribe(() => this.ngOnInit(), alertify.error(category.categoryName + ' Silindi')),
      () => alertify.success('İptal Edildi')
    );
  }
  
}
