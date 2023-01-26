import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthorService } from './authors/author.service';
import { BooksComponent } from './books/books.component';
import { AuthorsComponent } from './authors/authors.component';
import { ToastrModule } from 'ngx-toastr';
import { ToastContainerModule } from 'ngx-toastr/toastr/toast.directive';

@NgModule({
  declarations: [
    AppComponent,
    BooksComponent,
    AuthorsComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({
      timeOut: 2000,
      positionClass: 'toast-bottom-right',
      closeButton: true,
      preventDuplicates: true
    })
  ],
  providers: [HttpClient, AuthorService],
  bootstrap: [AppComponent]
})
export class AppModule { }
