import {Directive, HostListener, Input} from '@angular/core';
import {DatePickerComponent} from '@progress/kendo-angular-dateinputs';

@Directive({
  selector: 'kendo-datepicker[blockwheel]',
})
export class BlockwheelDirective {
  @Input() blockwheel!: DatePickerComponent;

  @HostListener('focus') onFocus() {
    document.querySelector('.k-datepicker input')?.addEventListener('wheel', (e) => {
      e.stopImmediatePropagation();
    });
  }

  @HostListener('blur') onBlur() {
    this.blockwheel.toggle(false);
  }
}
