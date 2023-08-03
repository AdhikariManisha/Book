import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorsComponent } from './authors/authors.component';
import { BooksComponent } from './books/books.component';
import { AuthGuardService } from './guards/auth-guard.service';

const routes: Routes = [
  {path:'books', component: BooksComponent, canActivate: [AuthGuardService]},
  {path:'authors', component: AuthorsComponent, canActivate: [AuthGuardService]},
  { path: 'login', loadChildren: () => import('./login/login.module').then(m => m.LoginModule) },
  { path: '', loadChildren: () => import('./home/home.module').then(m => m.HomeModule), canActivate: [AuthGuardService] },
  { path: 'register', loadChildren: () => import('./register/register.module').then(m => m.RegisterModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
