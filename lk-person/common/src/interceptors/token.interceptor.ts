import {Injectable} from '@angular/core';
import {HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {Router} from '@angular/router';
import {catchError} from 'rxjs/operators';
import {TokenStorageService} from '../services/token.service';
import {Location} from '@angular/common';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(
    private route: Router,
    private tokenStorageService: TokenStorageService,
    private location: Location
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // let accessToken = this.tokenStorageService.getAuthToken();
    // let accessTokenType = this.tokenStorageService.getAuthTokenType();
    // if (accessToken != null && accessTokenType != null)
    //   request = this.addAuthenticationToken(request, accessToken, accessTokenType);

    return next.handle(request).pipe(
      catchError((err) => {
        if (err instanceof HttpErrorResponse) {
          //console.log('status code ' + err.status);
          switch ((<HttpErrorResponse>err).status) {
            case 401:
              if (!localStorage.getItem('last_url')) {
                localStorage.setItem('last_url', this.location.path());
              }
              this.tokenStorageService.deleteToken();
              this.route.navigateByUrl('login');
              return throwError(() => err);
            /*case 400:
            case 500:
            case 209:
              console.log(err);
              return throwError(() => err);
              break;*/
            default:
              //this.notification.showNotification("error",
              // err?.error?.message ?? "Произошла ошибка при обработке запроса");
              return throwError(() => err);
          }
        } else return throwError(() => err);
      })
    );
  }

  // private addAuthenticationToken(request: HttpRequest<any>, token: string, tokenType: string = 'Bearer') {
  //   if (tokenType == null || token == null)
  //     return request;
  //
  //   return request.clone({
  //     setHeaders: {
  //       Authorization: `${tokenType} ${token}`,
  //     }
  //   });
  // }
}
