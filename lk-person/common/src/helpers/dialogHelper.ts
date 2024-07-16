import {DialogCloseResult, DialogResult, DialogService} from '@progress/kendo-angular-dialog';
import {Observable} from 'rxjs';
import {NotificationsService} from '../services/notifications.service';
import {getData$} from '../services/getData.service';

export class DialogHelper {
  /**
   * Открывает диалоговое окно с подтверждением удаления записи
   *
   * @param dialogService - сервис вызова диалогового окна
   * @param deleteTarget - объект, который мы хотим удалить
   * @param width - ширина диалогового окна
   * @param height - высота диалогового окна
   * @param minWidth - минимальная ширина диалогового окна
   */
  static openRemoveDialog(
    dialogService: DialogService,
    deleteTarget: string,
    width?: number,
    height?: number,
    minWidth?: number
  ) {
    return dialogService.open({
      title: 'Пожалуйста, подтвердите',
      content: `Вы действительно хотите удалить: ${deleteTarget}?`,
      actions: [{text: 'Нет'}, {text: 'Да', themeColor: 'primary'}],
      width: width ? width : 450,
      height: height ? height : 200,
      minWidth: minWidth ? minWidth : 250,
    });
  }

  static openDialog(
    dialogService: DialogService,
    deleteTarget: string,
    width?: number,
    height?: number,
    minWidth?: number
  ) {
    return dialogService.open({
      title: 'Пожалуйста, подтвердите',
      content: `${deleteTarget}`,
      actions: [{text: 'Нет'}, {text: 'Да', themeColor: 'primary'}],
      width: width ? width : 450,
      height: height ? height : 200,
      minWidth: minWidth ? minWidth : 250,
    });
  }

  /**
   * Выполнение сервиса после подтверждения диалогового окна
   * @param dialogResult - результат выбора в диалоговом окне
   * @param service - сервис на удаление данных
   * @param notificationService - сервис уведомления
   */
  static dialogResultHelper(
    dialogResult: Observable<DialogResult>,
    service: Observable<unknown>,
    notificationService: NotificationsService
  ) {
    dialogResult.subscribe((result) => {
      if (!(result instanceof DialogCloseResult) && result.text == 'Да') {
        service.subscribe({
          next: () => {
            notificationService.showSuccess('Успешно');
            getData$.next(true);
          },
          error: () => notificationService.showError('Произошла ошибка'),
        });
      }
    });
    return false;
  }
}
