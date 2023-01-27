import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ConfirmationDialog } from './confirmation-dialog/confirmation-dialog'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthorService } from './authors/author.service';
import { BooksComponent } from './books/books.component';
import { AuthorsComponent } from './authors/authors.component';
import { ToastrModule } from 'ngx-toastr';
import {MatDialogRef, MatDialogModule} from '@angular/material/dialog'
import{ MatButtonModule} from '@angular/material/button'
import{ MatCardModule} from '@angular/material/card'

@NgModule({
  declarations: [
    AppComponent,
    BooksComponent,
    AuthorsComponent,
    ConfirmationDialog,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      closeButton: true,
      preventDuplicates: true
    }),
    MatDialogModule,
    MatButtonModule,
    MatCardModule
  ],
  providers: [HttpClient, AuthorService, ],
  bootstrap: [AppComponent],
  entryComponents:[ConfirmationDialog]
})
export class AppModule { }
