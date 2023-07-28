import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorsComponent } from './authors/authors.component';
import { BooksComponent } from './books/books.component';

const routes: Routes = [
  {path:'books', component: BooksComponent},
  {path:'authors', component: AuthorsComponent},
  { path: 'login', loadChildren: () => import('./login/login.module').then(m => m.LoginModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
