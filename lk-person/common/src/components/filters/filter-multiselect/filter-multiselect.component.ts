import {Component, EventEmitter, Input, Output} from '@angular/core';
import {BaseFilterCellComponent, FilterService} from '@progress/kendo-angular-grid';

@Component({
  selector: 'filter-multiselect',
  templateUrl: './filter-multiselect.component.html',
  styleUrls: ['./filter-multiselect.component.scss'],
})
export class FilterMultiselectComponent extends BaseFilterCellComponent {
  constructor(filterService: FilterService) {
    super(filterService);
  }

  @Input() public data: any[] = [];
  @Input() public textField!: string;
  @Input() public valueField!: string;
  @Input() public checkboxes = false;
  @Input() public listHeight: number = 200;
  @Input() public gridField!: string;

  @Output() filterChange = new EventEmitter<any[]>();

  public get selectedValue(): any {
    const filter = this.filterByField(this.valueField);
    return filter ? filter.value : null;
  }

  public onChange(values: any[]): void {
    this.filterChange.emit(values);
    this.filterService.filter({
      filters: values.map((value) => ({
        field: this.gridField ?? this.valueField,
        operator: 'eq',
        value,
      })),
      logic: 'or',
    });
  }
}
