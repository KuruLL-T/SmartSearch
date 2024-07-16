import {Directive, HostListener, Input} from '@angular/core';
import {ComboBoxComponent} from '@progress/kendo-angular-dropdowns';

@Directive({
  selector: 'kendo-combobox[openOnFocus]',
})
export class OpenonfocusDirective {
  @Input() openOnFocus!: ComboBoxComponent;

  @HostListener('focus') onFocus() {
    this.openOnFocus.toggle(true);
  }

  @HostListener('blur') onBlur() {
    this.openOnFocus.toggle(false);
  }
}
