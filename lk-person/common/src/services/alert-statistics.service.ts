import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {catchError, Subject} from 'rxjs';
import {handleError} from '../helpers/errorHandle-helper';
import {environment} from '../environments/environment';
import {AlertStatistics} from '../models/alertStatistics.model';

@Injectable({
  providedIn: 'root',
})
export class AlertStatisticsService {
  baseUrl = `${environment.announcementApiEndpoint}${environment.apiPaths.announcement.base}`;

  private observer = new Subject();
  public subscriber$ = this.observer.asObservable();

  constructor(private http: HttpClient) {}

  public getAlertStatistics() {
    return this.http.get<AlertStatistics>(`${this.baseUrl}/statistics`).pipe(catchError(handleError));
  }
}
