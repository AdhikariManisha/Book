import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateUpdateBookDto } from './model';

@Injectable({
  providedIn: 'root'
})
export class BooksService {

  apiURL = `https://localhost:7024/api/book`;
  get = (id: number): Observable<any> => this.httpClient.get(`${this.apiURL}/${id}`);

  create = (dto: CreateUpdateBookDto) => this.httpClient.post(this.apiURL, dto);

  getList = (): Observable<any> => this.httpClient.get(this.apiURL);

  update = (dto: CreateUpdateBookDto) =>this.httpClient.put(`${this.apiURL}`, dto);

  delete = (id: number) => this.httpClient.delete(`${this.apiURL}/${id}`)

  constructor(private httpClient: HttpClient) { }
}
