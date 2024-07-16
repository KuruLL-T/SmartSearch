import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {environment} from '../environments/environment';
import {catchError, Subject} from 'rxjs';

@Injectable({
    providedIn: 'root'
  })

export class SummCommentsStatusesService {

    baseUrl = `${environment.apiEndpoint}`;

    constructor(private http: HttpClient) { }
    public getSumComments() {
        return this.http.get<number>(this.baseUrl + 'projecting/approvercommentstatuses/getsumnotviewcommentcount');
    }
}
