import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateUpdateAuthorDto } from './model';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  apiURL = `https://localhost:7024/api/Author`;
  get = (id: number): Observable<any> => this.httpClient.get(`${this.apiURL}/${id}`);

  create = (dto: CreateUpdateAuthorDto) => this.httpClient.post(this.apiURL, dto);

  getList = (): Observable<any> => this.httpClient.get(this.apiURL);

  update = (dto: CreateUpdateAuthorDto) =>this.httpClient.put(`${this.apiURL}`, dto);

  delete = (id: number) => this.httpClient.delete(`${this.apiURL}/${id}`)

  constructor(private httpClient: HttpClient) { }

}