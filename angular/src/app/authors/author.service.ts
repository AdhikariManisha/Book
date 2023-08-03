import { Injectable } from '@angular/core'
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthorFilter, CreateUpdateAuthorDto } from './model';
import { PagedAndSortedResultResponseDto } from '../models/PagedAndSortedResultResponseDto';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  apiURL = `${environment.apis.url}author`;
  get = (id: number): Observable<any> => this.httpClient.get(`${this.apiURL}/${id}`);

  create = (dto: CreateUpdateAuthorDto) => this.httpClient.post(this.apiURL, dto);

  getList = (): Observable<any> => this.httpClient.get(this.apiURL);

  update = (dto: CreateUpdateAuthorDto) => this.httpClient.put(`${this.apiURL}`, dto);

  delete = (id: number) => this.httpClient.delete(`${this.apiURL}/${id}`)

  deleteMany = (ids: number[]) => this.httpClient.request('delete', this.apiURL, { body: ids })

  getListByFilter(input: PagedAndSortedResultResponseDto, filter: AuthorFilter): Observable<any> {
    const params = new HttpParams({
      fromObject: { ...input, ...filter}
    });

    return this.httpClient.get(`${this.apiURL}/get-list-by-filter`, { params: params });
  }
  constructor(private httpClient: HttpClient) { }
}