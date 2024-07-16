import {BehaviorSubject} from 'rxjs';
import {DisableBreadCrumbRoutes} from '../models/breadcrumbs.model';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class GetDataService {
  public addBreadCrumbs$ = new BehaviorSubject<DisableBreadCrumbRoutes[]>([]);
}

// Для вызова события на получение данных в Grid
export const getData$ = new BehaviorSubject<boolean>(false);
