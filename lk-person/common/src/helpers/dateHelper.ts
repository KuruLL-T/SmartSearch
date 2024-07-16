import * as moment from 'moment';
import {Moment, unitOfTime} from 'moment';
import {DatePipe} from '@angular/common';

export class DateHelper {
  // нужна для получения данных с сервера, чтобы не съехало время из-за часового пояса
  static DateFromUTCAsLocal(date: any): Date {
    if (date == null) return date;
    return new Date(typeof date == 'string' ? `${date}`.replace('Z', '') : date);
  }

  // для отправки данных на сервер, чтобы время ушло как UTC
  static TimeZoneFix(date: any, isStringDate?: boolean): Moment {
    return isStringDate ? moment(date, 'YYYY-MM-DD').utc(true) : moment(date).utc(true);
  }

  // Конвертирование даты в строку
  static DateToString(date: Date | string, format?: string) {
    const datepipe: DatePipe = new DatePipe('ru-Ru');
    return datepipe.transform(date, format != null ? format : 'dd.MM.yyyy');
  }

  static CurrentDayEnd(): Date {
    return moment(new Date()).endOf('day').toDate();
  }

  static getDateDiff(firstDate: Date, secondDate: Date, format?: unitOfTime.Diff) {
    return Math.abs(moment(secondDate).diff(firstDate, format));
  }

  static getDayStart(date: Date): Date {
    return moment(date).startOf('day').toDate();
  }

  static getDayEnd(date: Date): Date {
    return moment(date).endOf('day').toDate();
  }

  static DisableWheelEvent() {
    const elements = document.querySelectorAll('.k-datepicker input');
    for (let i = 0; i < elements.length; i++) {
      elements[i].addEventListener('wheel', (e) => {
        e.stopImmediatePropagation();
      });
    }
  }

  static isDate(date: unknown) {
    return Object.prototype.toString.call(date) === '[object Date]';
  }

  static monthWord(value: number) {
    let month = "";
    switch(value) {
      case 0:
        month = "января";
        break;
      case 1:
        month = "февраля";
        break;
      case 2:
        month = "марта";
        break;
      case 3:
        month = "апреля";
        break;
      case 4:
        month = "мая";
        break;
      case 5:
        month = "июня";
        break;
      case 6:
        month = "июля";
        break;
      case 7:
        month = "августа";
        break;
      case 8:
        month = "сентября";
        break;
      case 9:
        month = "октября";
        break;
      case 10:
        month = "ноября";
        break;
      case 11:
        month = "декабря";
        break;
      default:
        break;
    }
    return month;
  }
}
