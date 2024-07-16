import {Directive, HostListener, Input} from '@angular/core';
import {DatePickerComponent} from '@progress/kendo-angular-dateinputs';

@Directive({
  selector: 'kendo-datepicker[openOnFocus]',
})
export class OpenonfocusdateDirective {
  @Input() openOnFocus!: DatePickerComponent;

  @HostListener('focus') onFocus() {
    this.openOnFocus.toggle(true);
  }

  @HostListener('blur') onBlur() {
    this.openOnFocus.toggle(false);
  }
}
