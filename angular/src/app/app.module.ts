import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ErrorHandler, NgModule } from '@angular/core';
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
import { MatDialogRef, MatDialogModule} from '@angular/material/dialog'
import { MatInputModule} from '@angular/material/input'
import { MatButtonModule} from '@angular/material/button'
import { MatCardModule} from '@angular/material/card'
import { MatPaginatorModule } from '@angular/material/paginator'
import { MatSortModule } from '@angular/material/sort'
import { MatTableModule } from '@angular/material/table'
import { MatDatepickerModule } from '@angular/material/datepicker'
import { MatSelectModule } from '@angular/material/select'
import { MatNativeDateModule } from '@angular/material/core'
import { MatCheckboxModule } from '@angular/material/checkbox'
import { GlobalErrorHandlerService } from './global-error-handler.service';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { NgSelectModule } from '@ng-select/ng-select';

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
      preventDuplicates: true,
    }),
    MatInputModule,
    MatDialogModule,
    MatButtonModule,
    MatCardModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatCheckboxModule,
    NgxDatatableModule,
    NgSelectModule
  ],
  providers: [{provide: ErrorHandler, useClass: GlobalErrorHandlerService}, HttpClient, AuthorService ],
  bootstrap: [AppComponent],
  entryComponents:[ConfirmationDialog]
})
export class AppModule { }
