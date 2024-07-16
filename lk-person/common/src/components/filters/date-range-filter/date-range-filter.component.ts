import {Component, Input, ElementRef} from '@angular/core';
import {
  FilterService,
  SinglePopupService,
  PopupCloseEvent,
  BaseFilterCellComponent,
} from '@progress/kendo-angular-grid';
import {PopupSettings} from '@progress/kendo-angular-dateinputs';
import {FilterDescriptor} from '@progress/kendo-data-query';
import {addDays} from '@progress/kendo-date-math';
import {Subscription} from 'rxjs';

/**
 * NOTE: Interface declaration here is for demo compilation purposes only!
 * In the usual case include it as an import from the data query package:
 *
 * import { CompositeFilterDescriptor } from '@progress/kendo-data-query';
 */
interface CompositeFilterDescriptor {
  logic: 'or' | 'and';
  filters: Array<any>;
}

const closest = (node: HTMLElement, predicate: (node: HTMLElement) => boolean): HTMLElement => {
  while (node && !predicate(node)) {
    node = node.parentNode as HTMLElement;
  }

  return node;
};

@Component({
  selector: 'date-range-filter',
  templateUrl: './date-range-filter.component.html',
  styleUrls: ['./date-range-filter.component.scss'],
})
export class DateRangeFilterComponent extends BaseFilterCellComponent {
  @Input() public override filter!: CompositeFilterDescriptor;

  @Input() public field!: string;

  private popupSubscription: Subscription;
  public popupSettings: PopupSettings = {
    popupClass: 'date-range-filter',
  };

  constructor(
    filterService: FilterService,
    private element: ElementRef,
    private popupService: SinglePopupService
  ) {
    super(filterService);
    this.popupSubscription = popupService.onClose.subscribe((e: PopupCloseEvent) => {
      if (
        document.activeElement &&
        closest(
          document.activeElement as HTMLElement,
          (node) =>
            node === this.element.nativeElement || String(node.className).indexOf('date-range-filter') >= 0
        )
      ) {
        e.preventDefault();
      }
    });
  }

  public get hasFilter(): boolean {
    return this.filtersByField(this.field).length > 0;
  }

  public clearFilter(): void {
    this.filterService.filter(this.removeFilter(this.field));
  }

  public get start(): Date {
    const first = this.findByOperator('gte');

    return (first || <FilterDescriptor>{}).value;
  }

  public get end(): Date {
    const end = this.findByOperator('lte');
    return (end || <FilterDescriptor>{}).value;
  }

  public get min(): Date | null {
    return this.start ? addDays(this.start, 1) : null;
  }

  public get max(): Date | null {
    return this.end ? addDays(this.end, -1) : null;
  }

  public onStartChange(value: Date): void {
    this.filterRange(value, this.end);
  }

  public onEndChange(value: Date): void {
    this.filterRange(this.start, value);
  }

  public filterRange(start: Date, end: Date): void {
    this.filter = this.removeFilter(this.field);

    const filters = [];

    if (start) {
      filters.push({
        field: this.field,
        operator: 'gte',
        value: start,
      });
    }

    if (end) {
      filters.push({
        field: this.field,
        operator: 'lte',
        value: end,
      });
    }

    const root = this.filter || {
      logic: 'and',
      filters: [],
    };

    if (filters.length) {
      root.filters.push(...filters);
    }

    this.filterService.filter(root);
  }

  private findByOperator(op: string): FilterDescriptor {
    return this.filtersByField(this.field).filter(({operator}) => operator === op)[0];
  }
}
