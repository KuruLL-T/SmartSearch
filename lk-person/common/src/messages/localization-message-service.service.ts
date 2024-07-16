import {Injectable} from '@angular/core';
import {MessageService} from '@progress/kendo-angular-l10n';
import {Messages} from '../models/messages.model';

const data: {[key: string]: Messages} = {
  ru: {
    rtl: false,
    messages: {
      kendo: {
        grid: {
          groupPanelEmpty: 'Перетаскивайте сюда заголовки колонок, чтобы сгруппировать по ним',
          noRecords: 'Нет записей.',
          pagerFirstPage: 'Перейти на первую страницу',
          pagerPreviousPage: 'Перейти на предыдущую страницу',
          pagerNextPage: 'Перейти на следующую страницу',
          pagerLastPage: 'Перейти на последнюю страницу',
          pagerPage: 'Страница',
          pagerOf: 'из',
          pagerItems: 'элементов',
          pagerItemsPerPage: 'элементов на странице',
          pagerPageNumberInputTitle: 'Page Number',
          filter: 'Фильтр',
          filterEqOperator: 'Равно',
          filterNotEqOperator: 'Не равно',
          filterIsNullOperator: 'Нет значения',
          filterIsNotNullOperator: 'Есть значение',
          filterIsEmptyOperator: 'Пусто',
          filterIsNotEmptyOperator: 'Не пусто',
          filterStartsWithOperator: 'Начинается с',
          filterContainsOperator: 'Содержит',
          filterNotContainsOperator: 'Не содержит',
          filterEndsWithOperator: 'Заканчивается на',
          filterGteOperator: 'Больше или равно',
          filterGtOperator: 'Больше чем',
          filterLteOperator: 'Меньше или равно',
          filterLtOperator: 'Меньше чем',
          filterIsTrue: 'Да',
          filterIsFalse: 'Нет',
          filterBooleanAll: '(Все)',
          filterAfterOrEqualOperator: 'Позже или равно',
          filterAfterOperator: 'Позже чем',
          filterBeforeOperator: 'Раньше чем',
          filterBeforeOrEqualOperator: 'Раньше или равно',
          filterFilterButton: 'Применить',
          filterClearButton: 'Сбросить',
          filterAndLogic: 'И',
          filterOrLogic: 'ИЛИ',
          filterDateToggle: 'Переключить календарь.',
          filterDateToday: 'Сегодня',
          filterNumericDecrement: 'Уменьшить значение',
          filterNumericIncrement: 'Увеличить значение',
          loading: 'Загрузка',
          columns: 'Колонки',
          lock: 'Заблокировать',
          unlock: 'Разблокировать',
          stick: 'Прикрепить',
          unstick: 'Открепить',
          setColumnPosition: 'Установить положение столбца',
          sortAscending: 'Отсортировать по возрастанию',
          sortDescending: 'Отсортировать по убыванию',
          columnsApply: 'Применить',
          columnsReset: 'Сбросить',
          sortable: 'Можно сортировать',
          sortedAscending: 'Отсортировано по возрастанию',
          sortedDescending: 'Отсортировано по убыванию',
          sortedDefault: 'Сортировка сброшена',
          filterInputLabel: '{columnName} Filter',
          filterMenuTitle: '{columnName} Filter Menu',
          filterMenuOperatorsDropDownLabel: '{columnName} Filter Operators',
          filterMenuLogicDropDownLabel: '{columnName} Filter Logic',
          columnMenu: '{columnName} Column Menu',
          selectionCheckboxLabel: 'Выберите строку',
          selectAllCheckboxLabel: 'Выбрать все строки',
          pagerLabel: 'Навигация по страницам, страница {currentPage} из {totalPages}',
          gridLabel: 'Таблица данных',
          groupCollapse: 'Свернуть группу',
          groupExpand: 'Развернуть группу',
          detailCollapse: 'Свернуть детали',
          detailExpand: 'Развернуть детали',
        },
        upload: {
          cancel: 'Отменить загрузку',
          clearSelectedFiles: 'Очистить',
          dropFilesHere: 'перетащите сюда файлы для загрузки',
          headerStatusUploaded: 'Готово',
          headerStatusUploading: 'Загружается...',
          invalidFileExtension: 'Не разрешенный тип файла.',
          invalidMaxFileSize: 'Размер файла слишком большой.',
          invalidMinFileSize: 'Размер файла слишком маленький.',
          remove: 'Удалить',
          retry: 'Повторить',
          select: 'Выбрать...',
          uploadSelectedFiles: 'Загрузить выбранные файлы',
          externalDropFilesHere: 'Перетащите файлы сюда для загрузки',
          filesBatchStatus: 'файлы',
          filesBatchStatusFailed: 'Не удалось загрузить файлы.',
          filesBatchStatusUploaded: 'Файлы загружены.',
          fileStatusFailed: 'Не удалось загрузить файл.',
          fileStatusUploaded: 'Файл загружен.',
          headerStatusPaused: 'Приостановлено',
        },
        fileselect: {
          dropFilesHere: 'Перетащите сюда файлы для загрузки',
          invalidFileExtension: 'Не разрешенный тип файла.',
          invalidMaxFileSize: 'Размер файла слишком большой.',
          invalidMinFileSize: 'Размер файла слишком маленький.',
          remove: 'Удалить',
          select: 'Выбрать...',
        },
        chat: {
          messagePlaceholder: 'Введите сообщение...',
          send: 'Отправить',
        },
        dropdownlist: {
          noDataText: 'Нет данных',
        },
        dropdowntree: {
          noDataText: 'Нет данных',
        },
        datepicker: {
          toggle: 'Переключить всплывающее окно',
          today: 'Сегодня',
          prevButtonTitle: 'Перейти к предыдущему виду',
          nextButtonTitle: 'Перейти к следующему виду',
        },
        combobox: {
          noDataText: 'Нет данных',
        },
        multiselect: {
          noDataText: 'Нет данных',
        },
        datetimepicker: {
          today: 'Сегодня',
          dateTab: 'Дата',
          timeTab: 'Время',
          accept: 'Назначить',
          cancel: 'Отмена',
          now: 'Сейчас',
        },
        autocomplete: {
          noDataText: 'Нет данных',
        },
        treelist: {
          noRecords: "Нет записей.",
          pagerOf: "из",
          pagerItems: "элементов",
          pagerItemsPerPage: "элементов на странице",
          pagerFirstPage: "Перейти на первую страницу",
          pagerPreviousPage: "Перейти на предыдущую страницу",
          pagerNextPage: "Перейти на следующую страницу",
          pagerLastPage: "Перейти на последнюю страницу",
          pagerLabel: 'Навигация по страницам, страница {currentPage} из {totalPages}',
          pagerPage: "Страница",
          pagerItemsTotal: "всего элементов",
        },
        multiviewcalendar: {
          today: 'Сегодня',
        },
        editor: {
          createLink: 'Вставить гиперссылку',
          linkWebAddress: 'Web-адрес',
          linkText: 'Текст',
          linkTitle: 'Текст при наведении',
          linkOpenInNewWindow: 'Открывать ссылку в новом окне',
          dialogCancel: 'Отмена',
          dialogInsert: 'Вставить',
        },
      },
    },
  },
  en: {
    rtl: false,
    messages: {
      'kendo.grid.noRecords': 'No records available.',
    },
  },
};

@Injectable()
export class LocalizationMessageService extends MessageService {
  /**
   * Язык по умолчанию
   * @private
   */
  private localeId = 'ru';

  public set language(value: string) {
    const lang = data[value as keyof typeof data];
    if (lang) {
      this.localeId = value;
      this.notify(lang.rtl);
    }
  }

  public get language(): string {
    return this.localeId;
  }

  private get messages(): {[key: string]: object | string} | undefined {
    const lang = data[this.localeId as keyof typeof data];
    return lang ? lang.messages : undefined;
  }

  public override get(key: string): string {
    if (this.messages) {
      try {
        const keys = key.split('.');
        return (
          (this.messages[keys[0]] as {[key: string]: object})[keys[1]] as {
            [key: string]: object;
          }
        )[keys[2]] as unknown as string;
      } catch (e) {
        return this.messages[key] as string;
      }
    } else {
      return '';
    }
  }
}
