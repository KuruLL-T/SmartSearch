import {NgModule} from '@angular/core';
import {ExcelModule, GridModule, PDFModule} from '@progress/kendo-angular-grid';
import {DropDownListModule, DropDownsModule} from '@progress/kendo-angular-dropdowns';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {InputsModule} from '@progress/kendo-angular-inputs';
import {NavigationModule} from '@progress/kendo-angular-navigation';
import {ButtonsModule} from '@progress/kendo-angular-buttons';
import {MenusModule} from '@progress/kendo-angular-menu';
import {DateInputsModule} from '@progress/kendo-angular-dateinputs';
import {IndicatorsModule, LoaderModule} from '@progress/kendo-angular-indicators';
import {IntlModule} from '@progress/kendo-angular-intl';
import {LayoutModule} from '@progress/kendo-angular-layout';
import {IconsModule} from '@progress/kendo-angular-icons';
import {NotificationModule} from '@progress/kendo-angular-notification';
import {DialogsModule} from '@progress/kendo-angular-dialog';
import {LocalizationMessageService} from '../messages/localization-message-service.service';
import {MessageService} from '@progress/kendo-angular-l10n';
import {ListViewModule} from '@progress/kendo-angular-listview';
import {FileSelectModule, UploadsModule} from '@progress/kendo-angular-upload';
import {LabelModule} from '@progress/kendo-angular-label';
import {TooltipsModule} from '@progress/kendo-angular-tooltip';
import {PopupModule} from '@progress/kendo-angular-popup';
import {PDFExportModule} from '@progress/kendo-angular-pdf-export';
import {TreeViewModule} from '@progress/kendo-angular-treeview';
import {ListBoxModule} from '@progress/kendo-angular-listbox';
import {FilterModule} from '@progress/kendo-angular-filter';
import {TypographyModule} from '@progress/kendo-angular-typography';
import {PagerModule} from '@progress/kendo-angular-pager';
import {FilterDropdownComponent} from '../components/filters/filter-dropdown/filter-dropdown.component';
import {BlockwheelDirective} from '../directives/blockwheel.directive';
import {DoubleclicktransferDirective} from '../directives/doubleclicktransfer.directive';
import {OpenOnFocusAutocompleteDirective} from '../directives/open-on-focus-autocomplete.directive';
import {OpenOnFocusMultiselectDirective} from '../directives/open-on-focus-multiselect.directive';
import {OpenonfocusDirective} from '../directives/openonfocus.directive';
import {OpenonfocusdateDirective} from '../directives/openonfocusdate.directive';
import {EditorModule} from '@progress/kendo-angular-editor';
import {PhoneFormatPipe} from '../pipes/phoneFormat.pipe';
import {FilterMultiselectComponent} from '../components/filters/filter-multiselect/filter-multiselect.component';
import {FilterComboboxComponent} from '../components/filters/filter-combobox/filter-combobox.component';
import {DateRangeFilterComponent} from '../components/filters/date-range-filter/date-range-filter.component';
import {ToolBarModule} from '@progress/kendo-angular-toolbar';
import {TreeListModule} from "@progress/kendo-angular-treelist";
import {DictComponent} from '../components/dict/dict.component';
import {CommonModule} from '@angular/common';

// Общая библиотека. Сделана для импортирования библиотек Kendo UI и другие
@NgModule({
  imports: [
    DropDownsModule,
    DropDownListModule,
    LabelModule,
    DateInputsModule,
    FormsModule,
    ReactiveFormsModule,
    GridModule,
    InputsModule,
    ButtonsModule,
    DialogsModule,
    LoaderModule,
    CommonModule,
  ],
  declarations: [
    FilterDropdownComponent,
    FilterMultiselectComponent,
    FilterComboboxComponent,
    DateRangeFilterComponent,
    DictComponent,
    BlockwheelDirective,
    DoubleclicktransferDirective,
    OpenOnFocusAutocompleteDirective,
    OpenOnFocusMultiselectDirective,
    OpenonfocusDirective,
    OpenonfocusdateDirective,
    PhoneFormatPipe,
  ],
  exports: [
    DropDownsModule,
    GridModule,
    IndicatorsModule,
    PDFModule,
    FormsModule,
    ButtonsModule,
    LabelModule,
    InputsModule,
    MenusModule,
    DialogsModule,
    ReactiveFormsModule,
    DropDownListModule,
    NotificationModule,
    ExcelModule,
    LayoutModule,
    TypographyModule,
    ListViewModule,
    DateInputsModule,
    IntlModule,
    NavigationModule,
    IconsModule,
    FilterModule,
    ListBoxModule,
    TreeViewModule,
    UploadsModule,
    PDFExportModule,
    PopupModule,
    PagerModule,
    TooltipsModule,
    FileSelectModule,
    EditorModule,
    ToolBarModule,
    TreeListModule,

    FilterDropdownComponent,
    FilterMultiselectComponent,
    FilterComboboxComponent,
    DateRangeFilterComponent,
    DictComponent,

    BlockwheelDirective,
    DoubleclicktransferDirective,
    OpenOnFocusAutocompleteDirective,
    OpenOnFocusMultiselectDirective,
    OpenonfocusDirective,
    OpenonfocusdateDirective,

    PhoneFormatPipe,
  ],
  providers: [{provide: MessageService, useClass: LocalizationMessageService}],
})
export class SharedModule {}
