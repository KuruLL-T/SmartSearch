import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';

export function handleError(error: HttpErrorResponse) {
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

export function getErrorMessage(error: any): string {
  console.log(error);

  let message;

  switch (error?.status) {
    case 500:
      //message = 'Произошла непредвиденная ошибка (Internal Server Error).';
      message = 'Произошла непредвиденная ошибка.';
      break;
    case 501:
      //message = 'Метод запроса не поддерживается сервером (Not Implemented).';
      message = 'Метод запроса не поддерживается сервером.';
      break;
    case 502:
      //message = 'Сервер получил недействительный ответ (Bad Gateway).';
      message = 'Сервер получил недействительный ответ.';
      break;
    case 503:
      //message = 'Сервис недоступен (Service Unavailable).';
      message = 'Сервис недоступен.';
      break;
    case 504:
      //message = 'Сервер не получил ответ вовремя (Gateway Timeout).';
      message = 'Сервер не получил ответ вовремя.';
      break;
    case 505:
      //message = 'HTTP-версия, используемая в запросе, не поддерживается сервером (HTTP Version Not Supported).';
      message = 'HTTP-версия, используемая в запросе, не поддерживается сервером.';
      break;
    default:
      if (typeof error?.error === 'string') message = error.error;
      else if (typeof (<any>error)?.detail === 'string') message = (<any>error).detail;
      else if (typeof error?.error?.detail === 'string') message = error.error.detail;
      break;
  }

  return message ?? 'Произошла непредвиденная ошибка';
}
