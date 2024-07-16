import {Component, OnInit, ViewChild, Renderer2, Input, Output, EventEmitter} from '@angular/core';
import {
  AddEvent,
  GridComponent,
  RemoveEvent,
  CellClickEvent,
  PagerSettings,
} from '@progress/kendo-angular-grid';
import {FormGroup} from '@angular/forms';
import {DialogHelper} from '../../helpers/dialogHelper';
import {ElementRefHelper} from '../../helpers/elementRef-helper';
import {NotificationsService} from '../../services/notifications.service';
import {DialogService, DialogCloseResult} from '@progress/kendo-angular-dialog';
import {ActivatedRoute} from '@angular/router';
import {DropDownFilterSettings} from '@progress/kendo-angular-dropdowns';
import {GroupDescriptor} from '@progress/kendo-data-query';
import {EditEvent, EventData, FieldInfo, ListOptions} from '../../models/common-dict.model';
import {
  FieldType,
  leftAlignmentFields,
  listOptionsFields,
  nonFilterableFields,
  numericFields,
} from '../../enums/common-dict.enum';

@Component({
  selector: 'app-dict',
  templateUrl: './dict.component.html',
  styleUrls: ['./dict.component.scss'],
})
/**
 * Общий компонент для справочников.
 * @property {Array<object>} data - массив данных
 * @property {Array<FieldInfo>} fieldsInfo - информация о столбцах
 * @property {Map<string, string>} breadcrumbs - хлебные крошки
 * @property {boolean} editable - возможность редактирования
 * @property {EventEmitter<void>} getMethod - запрос на получение записей
 * @property {EventEmitter<EventData<T>>} removeMethod - запрос на удаление записи
 *
 *
 * ***Опционально***
 * @property {boolean} [editViaForm] - редактирование через форму
 * @property {EventEmitter<void>} [accessMethod] - запрос на получение прав доступа
 * @property {boolean} [loading] - отображение загрузки
 * @property {boolean} [isTreeList] - вложенность записей (in progress)
 * @property {PagerSettings|boolean} [pageable] - настройки пейджера
 * @property {number} [pageSize] - размер страницы
 * @property {boolean} [editOnly] - только редактирование существующих записей
 * @property {boolean} [sortable] - сортировка
 * @property {boolean} [filterable] - фильтрация
 * @property {Array<GroupDescriptor>} [group] - группировка
 * @property {string} [groupTitle] - заголовок группы
 *
 *
 * ***Если `editViaForm = true`***
 * @property {boolean} isEditMode - флаг для режима редактирования
 * @property {EventEmitter<EditEvent<T>>} editFormHandler - переход в режим редактирования
 *
 *
 * ***Если `editViaForm = false`***
 * @property {(dataItem?: T) => FormGroup} getFormGroup - функция для создания `FormGroup`
 * @property {EventEmitter<EventData<T>>} addMethod - запрос на добавление записи
 * @property {EventEmitter<EventData<T>>} updateMethod - запрос на редактирование записи
 * @property {boolean} [saveOnClick] - сохранение по клику в свободной области
 * @example
 * _@Component({
 *    selector: 'app-test-dict',
 *    template: `
 *        <app-dict [data]="dictData"
 *                  [fieldsInfo]="info"
 *                  [breadcrumbs] = "breadcrumbItemsMap"
 *                  [editable]="editable"
 *                  [getFormGroup]="formGroup"
 *                  (getMethod)="getData()"
 *                  (addMethod)="addData($event)"
 *                  (updateMethod)="updateData($event)"
 *                  (removeMethod)="removeData($event)"
 *        </app-dict>
 *    `
 * })
 * export class TestDictComponent implements OnInit {
 *   public dictData: TestModel[] = [];
 *   protected form?: FormGroup;
 *   protected editable = false;
 *
 *   protected info: FieldInfo<TestModel>[] = [
 *     {
 *       field: 'name',
 *       type: FieldType.Textbox,
 *       title: 'Название',
 *       width: 350,
 *       useForDelete: true,
 *     },
 *     {
 *       field: 'order',
 *       type: FieldType.Order,
 *       width: 200,
 *     }
 *   ];
 *
 *   ...
 *
 *   protected formGroup(dataItem?: TestModel): FormGroup {
 *     return new FormGroup({
 *       ...
 *     });
 *   }
 * }
 */
export class DictComponent<T> implements OnInit {
  /**
   * Справочник автоматически подставит в заголовок
   * название справочника из хлебных крошек.
   */
  @Input() breadcrumbs!: Map<string, string>;
  /**
   * Настраивает вид пейджера таблицы.
   *
   * Доступные опции:
   * - `buttonCount: Number` &mdash; Устанавливает максимальное количество кнопок. Значение по умолчанию: `5`.
   * - `info: Boolean` &mdash; Переключает информацию о текущей странице и общем количестве записей. Значение по умолчанию: `true`.
   * - `type: PagerType` &mdash; Принимает `numeric` (кнопки с цифрами) и `input` (ввод номера страницы вручную) значения. Значение по умолчанию: `'numeric'`.
   * - `pageSizes: Boolean` or `Array<number>` &mdash; Набор для выбора размера страницы в меню. Значение по умолчанию: `[10, 20, 50]`.
   * - `previousNext: Boolean` &mdash; Включает\отключает кнопки **Назад** и **Вперед**. Значение по умолчанию: `true`.
   * - `position: PagerPosition` &mdash; Определяет местоположение пейджера относительно таблицы. Значение по умолчанию: `bottom`.
   */
  @Input() pageable: PagerSettings | boolean = {
    type: 'numeric',
    buttonCount: 5,
    pageSizes: [10, 20, 50],
    info: true,
    previousNext: true,
    position: 'bottom',
  };
  /**
   * Определяет размер страницы в таблице по умолчанию.
   * @default 10
   */
  @Input() pageSize = 10;
  /**
   * Регулирует возможность редактирования справочника.
   */
  @Input() editable = false;
  /**
   * Регулирует отображение загрузки данных
   * @default false
   */
  @Input() loading = false;
  /**
   * Отключает добавление/удаление записей справочника.
   * Рекомендуется использовать для справочников с enum,
   * от которого зависит внутренняя логика.
   * @default false
   */
  @Input() editOnly = false;
  /**
   * Включает вложенность записей.
   * @default false
   * @todo
   */
  protected isTreeList = false;
  /**
   * Редактирование через внешнюю форму.
   * @default false
   */
  @Input() editViaForm = false;
  /**
   * Флаг для режима редактирования.
   * Используется для отображения\скрытия внешней формы.
   *
   * (`editViaForm = true`)
   */
  @Input() isEditMode = false;
  /**
   * Включает сохранение записей по клику мыши в свободной области.
   * Используется при редактировании непосредственно в таблице.
   *
   * (`editViaForm = false`)
   * @default true
   */
  @Input() saveOnClick = true;
  /**
   * Включает сортировку значений полей.
   * @default true
   */
  @Input() sortable = true;
  /**
   * Включает фильтрацию значений полей.
   * @default true
   */
  @Input() filterable = true;
  /**
   * Массив данных, который будет использоваться для заполнения справочника.
   */
  @Input() data: T[] = [];
  /**
   * Дескрипторы, по которым будут группироваться данные.
   * @example
   * ```html
   * <app-dict [data]="dictData"
   *           [group]="[{ field: 'name'}]"
   * </app-dict>
   * ```
   */
  @Input() group: GroupDescriptor[] = [];
  /**
   * Заголовок группы. По умолчанию: название поля, указанного в `group`.
   */
  @Input() groupTitle?: string;
  /**
   * Массив, содержащий информацию о столбцах справочника.
   *
   * Доступные свойства:
   * - `field?: keyof T & string` &mdash; Название поля (соответствует полю модели данных и FormControl)
   * - `type: FieldType` &mdash; Тип поля
   * - `title?: string` &mdash; Название столбца (Для типа `Order` значение по умолчанию: `'Порядковый номер для сортировки при отображении'`)
   * - `width: number` &mdash; Ширина столбца
   * - `listOptions?: ListOptions` &mdash; Данные для отображения списка (Для типов `Combobox`, `Multiselect`, `Autocomplete`)
   * - `boolOptions?: Map<boolean, string>` &mdash; Данные для отображения значений типа `Checkbox`
   * - `iconClass?: string` &mdash; Иконка для перехода по ссылке (Для типа `Link`)
   * - `clickHandler?: (dataItem?: T) => void` &mdash; Переход по ссылке (Для типа `Link`)
   * - `sortable?: boolean` &mdash; Сортировка значений столбца. Значение по умолчанию: `true` при включенной сортировке справочника.
   * - `filterable?: boolean` &mdash; Фильтрация значений столбца.
   * Значение по умолчанию: `true` при включенной фильтрации справочника (Фильтрация для типов `Checkbox`, `Autocomplete`, `Color`, `Link` отключена)
   * - `useForDelete?: boolean` &mdash; Использовать значения столбца в сообщении при удалении записи (По умолчанию берутся значения первого столбца)
   */
  @Input() fieldsInfo: FieldInfo<T>[] = [];
  /**
   * Функция для создания `FormGroup` по модели данных справочника.
   * Используется при редактировании непосредственно в таблице.
   *
   * (`editViaForm = false`)
   * @example
   * protected formGroup(dataItem?: DataModel): FormGroup {
   *   return new FormGroup({
   *     externalId: new FormControl(dataItem?.externalId ?? null),
   *     name: new FormControl(dataItem?.name ?? null, Validators.required),
   *     order: new FormControl(dataItem?.order ?? null),
   *   });
   * }
   */
  @Input() getFormGroup!: (dataItem?: T) => FormGroup;

  @ViewChild(GridComponent) private grid!: GridComponent;
  private editedRowIndex: number | undefined;
  private isNew = false;
  private isLine = false;

  protected dictName?: string;
  protected form?: FormGroup;
  /**
   * Вызывается при переходе в режим редактирования.
   * Используется при редактировании через внешнюю форму.
   *
   * (`editViaForm = true`)
   * @example
   * protected edit({isNew, dataItem}: EditEvent<DataModel>) {
   *   this.isEditMode = true;
   *   this.isNew = isNew;
   *   this.form = this.formGroup(dataItem);
   * }
   */
  @Output() editFormHandler = new EventEmitter<EditEvent<T>>();
  /**
   * Вызывается при инициализации компонента для получения прав доступа и получения данных справочника
   * Использовать при необходимости (например если доступ к справочнику не блокируется ранее)
   * @example
   * public getAccessLevel() {
   *   this.userAccessService.getAccessLevel().subscribe((response) => {
   *     if (response.accessLevel === 0) {
   *         this.notificationService.showError('У вас нет прав для просмотра данной страницы');
   *         this.router.navigate(['/profile']);
   *     } else {
   *       this.editable = response.access !== 1;
   *       this.getData();
   *     }
   *   });
   * }
   */
  @Output() accessMethod = new EventEmitter<void>();
  /**
   * Вызывается при инициализации компонента и после изменения данных для получения данных справочника
   * @example
   * protected getData() {
   *   this.dataService.get().subscribe((response) => this.data = response);
   * }
   */
  @Output() getMethod = new EventEmitter<void>();
  /**
   * Вызывается для сохранения новой записи.
   * Используется при редактировании непосредственно в таблице.
   *
   * (`editViaForm = false`)
   * @example
   * protected addData({value, observer}: EventData<DataModel>) {
   *   this.dataService.addData(value).subscribe(observer);
   * }
   */
  @Output() addMethod = new EventEmitter<EventData<T>>();
  /**
   * Вызывается для сохранения изменений существующей записи.
   * Используется при редактировании непосредственно в таблице.
   *
   * (`editViaForm = false`)
   * @example
   * protected updateData({value, observer}: EventData<DataModel>) {
   *   this.dataService.updateData(value).subscribe(observer);
   * }
   */
  @Output() updateMethod = new EventEmitter<EventData<T>>();
  /**
   * Вызывается при нажатии на кнопку удаления записи
   * @example
   * protected removeData({value, observer}: EventData<DataModel>) {
   *   this.dataService.deleteData(value.externalId).subscribe(observer);
   * }
   */
  @Output() removeMethod = new EventEmitter<EventData<T>>();

  protected filterSettings: DropDownFilterSettings = {
    caseSensitive: false,
    operator: 'contains',
  };

  protected readonly FieldType = FieldType;
  protected readonly numericFields = numericFields;
  protected readonly listOptionsFields = listOptionsFields;
  protected readonly nonFilterableFields = nonFilterableFields;
  protected readonly leftAlignmentFields = leftAlignmentFields;

  protected get isInEditingMode(): boolean {
    return this.editViaForm ? this.isEditMode : this.editedRowIndex !== undefined || this.isNew;
  }

  constructor(
    private activateRoute: ActivatedRoute,
    private renderer: Renderer2,
    private notificationService: NotificationsService,
    private dialogService: DialogService
  ) {}

  public ngOnInit(): void {
    this.getDictName();
    this.accessMethod.observed ? this.getAccess() : this.getData();
    this.subscribe();
  }

  private subscribe() {
    if (this.saveOnClick)
      this.renderer.listen('document', 'click', ({target}) => {
        if (!ElementRefHelper.isChildOf(target, 'k-grid') && !ElementRefHelper.hasClass(target, 'k-i-x')) {
          this.saveCurrent();
        }
      });
  }

  private getAccess() {
    this.accessMethod.emit();
  }

  private getData() {
    this.getMethod.emit();
  }

  private getDictName() {
    const path = this.activateRoute.snapshot.url.at(-1)?.path;
    path && (this.dictName = this.breadcrumbs.get(path));
  }

  protected getDataItemText(value: any, listOptions?: ListOptions) {
    const item = listOptions?.data.find((item) => item[listOptions?.valueField] === value);
    return item ? item[listOptions?.textField ?? ''] : '';
  }

  protected closeEditor(): void {
    this.isNew = false;
    this.form = undefined;
    this.grid.closeRow(this.editedRowIndex);
    this.editedRowIndex = undefined;
  }

  public saveHandler(form?: FormGroup) {
    this.form = form;
    this.saveRow();
  }

  protected addHandler({sender}: AddEvent): void {
    if (this.editViaForm) {
      this.isNew = true;
      this.editFormHandler.emit({isNew: this.isNew});
      return;
    }

    this.form = this.getFormGroup();
    this.isLine = true;
    this.isNew = true;
    sender.addRow(this.form);
  }

  private saveRow(): void {
    if (this.isInEditingMode && this.form) {
      const data: EventData<T> = {
        value: this.form.value,
        observer: {
          next: () => {
            this.getData();
            this.notificationService.showSuccess(this.isNew ? 'Добавлено' : 'Сохранено');
          },
          error: () => {
            this.notificationService.showError(`Не удалось ${this.isNew ? 'добавить' : 'сохранить'} запись`);
          },
        },
      };

      this.isNew ? this.addMethod.emit(data) : this.updateMethod.emit(data);
    }
    this.closeEditor();
  }

  protected editHandler({sender, columnIndex, rowIndex, dataItem}: CellClickEvent): void {
    if (!this.editable) return;
    if (this.editViaForm) {
      this.isNew = false;
      this.editFormHandler.emit({isNew: this.isNew, dataItem});
      return;
    }

    if (this.isLine || (this.form && this.form.invalid)) {
      this.saveOnClick && this.saveCurrent();
      return;
    }

    this.isLine = true;
    this.isNew = false;
    this.saveRow();
    this.form = this.getFormGroup(dataItem);
    this.editedRowIndex = rowIndex;

    sender.editRow(rowIndex, this.form, {columnIndex});
  }

  protected saveCurrent(): void {
    if (this.form && this.form.invalid) {
      return;
    }

    this.isLine = false;
    this.saveRow();
  }

  protected removeHandler({dataItem}: RemoveEvent): void {
    const fieldName = this.fieldsInfo.find((item) => item.useForDelete)?.field ?? this.fieldsInfo[0].field;
    const dialog = DialogHelper.openRemoveDialog(this.dialogService, dataItem[fieldName]);
    dialog.result.subscribe((result) => {
      if (!(result instanceof DialogCloseResult) && result.text == 'Да') {
        this.removeMethod.emit({
          value: dataItem,
          observer: {
            next: () => {
              this.getData();
              this.notificationService.showSuccess('Удалено');
            },
            error: (error) => {
              this.notificationService.showError(error.error ?? error);
            },
          },
        });
      }
    });
  }

  protected clearField(field: string) {
    this.form?.get(field)?.patchValue(null);
  }
}
