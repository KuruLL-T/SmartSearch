import {Injectable} from '@angular/core';
import {Observable, Subject} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {PublicationsNotification} from '../models/publicationsNotification.model';
import {environment} from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PublicationsBadgeService {
  private baseUrl = `${environment.lkPersonApiEndpoint}${environment.apiPaths.publications.main}/GetNotification`;

  private observer = new Subject();
  public subscriber$ = this.observer.asObservable();

  constructor(private http: HttpClient) {}

  public refreshBadge() {
    this.observer.next(0);
  }

  public getNotification(): Observable<PublicationsNotification> {
    return this.http.get<PublicationsNotification>(this.baseUrl);
  }
}
