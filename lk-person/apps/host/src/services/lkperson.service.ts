import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {catchError, Observable, throwError} from 'rxjs';
import {LKPerson} from 'common';
import {environment} from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PersonService {
  baseUrl = `${environment.lkPersonApiEndpoint}${environment.apiPaths.lkPerson}`;

  constructor(private http: HttpClient) {}

  //Get Person
  public getPersonByLogin(): Observable<LKPerson> {
    return this.http.get<LKPerson>(this.baseUrl + '/GetPersonByLogin').pipe(catchError(this.handleError));
  }

  //Get Current Person
  public getCurrentPerson(): Observable<LKPerson> {
    return this.http.get<LKPerson>(this.baseUrl + '/GetCurrentPerson').pipe(catchError(this.handleError));
  }

  //Get all persons
  public getPersons(): Observable<LKPerson[]> {
    return this.http.get<LKPerson[]>(this.baseUrl + '/GetPersons/').pipe(catchError(this.handleError));
  }

  // Error
  handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Handle client error
      errorMessage = error.error.message;
    } else {
      // Handle server error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(() => {
      errorMessage;
    });
  }
}
