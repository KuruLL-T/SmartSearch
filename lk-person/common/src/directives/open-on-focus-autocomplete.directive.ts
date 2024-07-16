import {Directive, HostListener, Input} from '@angular/core';
import {AutoCompleteComponent} from '@progress/kendo-angular-dropdowns';

@Directive({
  selector: 'kendo-autocomplete[openOnFocus]',
})
export class OpenOnFocusAutocompleteDirective {
  @Input() openOnFocus!: AutoCompleteComponent;

  @HostListener('focus') onFocus() {
    this.openOnFocus.toggle(true);
  }

  @HostListener('blur') onBlur() {
    this.openOnFocus.toggle(false);
  }
}
