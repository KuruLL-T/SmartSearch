import {Directive, HostListener, Input} from '@angular/core';
import {MultiSelectComponent} from '@progress/kendo-angular-dropdowns';

@Directive({
  selector: 'kendo-multiselect[openOnFocus]',
})
export class OpenOnFocusMultiselectDirective {
  @Input() openOnFocus!: MultiSelectComponent;

  @HostListener('focus') onFocus() {
    this.openOnFocus.toggle(true);
  }

  @HostListener('blur') onBlur() {
    this.openOnFocus.toggle(false);
  }
}
