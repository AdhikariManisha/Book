import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { UserLoginDto } from './model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'

})
export class UserService {
  apiURL= `${environment.apis.url}user/login`;
  login =(dto: UserLoginDto): Observable<any> => this.httpClient.post(this.apiURL, dto);
  

  constructor(private httpClient: HttpClient) { 

  }
}
