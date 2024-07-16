import {Component, Input} from '@angular/core';
import {CompositeFilterDescriptor} from '@progress/kendo-data-query';
import {BaseFilterCellComponent, FilterService} from '@progress/kendo-angular-grid';
import {VirtualizationSettings} from '@progress/kendo-angular-dropdowns';

@Component({
  selector: 'filter-combobox',
  templateUrl: './filter-combobox.component.html',
  styleUrls: ['./filter-combobox.component.scss'],
})
export class FilterComboboxComponent extends BaseFilterCellComponent {
  constructor(filterService: FilterService) {
    super(filterService);
  }

  @Input() public override filter!: CompositeFilterDescriptor;
  @Input() public data!: unknown[];
  @Input() public textField!: string;
  @Input() public valueField!: string;
  @Input() public componentName!: string;
  @Input() public gridField!: string;
  @Input() public useVirtual = true;
  @Input() public userAccessValue!: boolean;
  @Input() public userAccessSource!: string;

  public filterVirtualization: boolean | VirtualizationSettings = {
    itemHeight: 28,
  };

  public get selectedValue(): unknown {
    const filter = this.filterByField(this.valueField);
    return filter ? filter.value : null;
  }

  public onChange(value: unknown): void {
    this.applyFilter(
      !value
        ? this.removeFilter(this.gridField ?? this.valueField)
        : this.updateFilter({
            field: this.gridField ?? this.valueField,
            operator: 'eq',
            value: value,
          })
    );
  }
}
