import {Directive, HostListener, Input} from '@angular/core';
import {ListBoxComponent} from '@progress/kendo-angular-listbox';

@Directive({
  selector: 'kendo-listbox[doubleClickTransfer]',
})
export class DoubleclicktransferDirective {
  @Input() dblclick!: ListBoxComponent;

  @HostListener('dblclick') onFocus() {
    this.dblclick.clearSelection();
  }

  @HostListener('focus') onBlur() {
    this.dblclick.selectItem(1);
  }
}
