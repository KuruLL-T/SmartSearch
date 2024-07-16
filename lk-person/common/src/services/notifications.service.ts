import {Injectable} from '@angular/core';
import {NotificationService} from '@progress/kendo-angular-notification';
import {getErrorMessage} from '../helpers/errorHandle-helper';

@Injectable({
  providedIn: 'root',
})
export class NotificationsService {
  constructor(private notificationService: NotificationService) {}

  public showSuccess(message: string, hideAfter: number = 1500): void {
    this.notificationService.show({
      content: message ?? '',
      hideAfter: hideAfter,
      width: 500,
      position: {horizontal: 'center', vertical: 'top'},
      animation: {type: 'fade', duration: 300},
      type: {style: 'success', icon: true},
    });
  }

  public showError(message: unknown, hideAfter: number = 1500): void {
    this.notificationService.show({
      content: typeof message == 'string' ? message : getErrorMessage(message),
      hideAfter: hideAfter,
      width: 500,
      position: {horizontal: 'center', vertical: 'top'},
      animation: {type: 'slide', duration: 300},
      type: {style: 'error', icon: true},
    });
  }

  public showWarning(message: string) {
    this.notificationService.show({
      content: message ?? '',
      hideAfter: 5000,
      width: 500,
      position: {horizontal: 'center', vertical: 'top'},
      animation: {type: 'fade', duration: 300},
      type: {style: 'warning', icon: true},
    });
  }
}
