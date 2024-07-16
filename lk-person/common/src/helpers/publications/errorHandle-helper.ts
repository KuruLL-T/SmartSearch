import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';

export function PublicationsHandleError(error: HttpErrorResponse) {
  let errorMessage: string;
  if (typeof error.error == 'string') {
    errorMessage = error.error;
  } else {
    console.log(`Error Code: ${error.status}\nMessage: ${error.message}`);
  }

  return throwError(() => {
    return errorMessage ?? error;
  });
}
