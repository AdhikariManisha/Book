import { Component } from '@angular/core';
import { BooksService } from './books.service';
import { BookDto } from './model';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent {
  data: BookDto []=[];
  constructor(private bookServices: BooksService){
    this.getList();
  }
  getList(){
    this.bookServices.getList().subscribe((s: BookDto[])=>{
      this.data = s;
    });
  }

}


