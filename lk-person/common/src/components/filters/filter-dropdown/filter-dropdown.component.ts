import {Component, Input} from '@angular/core';
import {FilterService, BaseFilterCellComponent} from '@progress/kendo-angular-grid';
import {CompositeFilterDescriptor} from '@progress/kendo-data-query';
import {VirtualizationSettings} from '@progress/kendo-angular-dropdowns';

@Component({
  selector: 'filter-dropdown',
  templateUrl: './filter-dropdown.component.html',
  styleUrls: ['./filter-dropdown.component.scss'],
})
export class FilterDropdownComponent extends BaseFilterCellComponent {
  public get selectedValue(): unknown {
    const filter = this.filterByField(this.valueField);
    return filter ? filter.value : null;
  }

  public defaultValue = '';

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

  public get defaultItem(): {[Key: string]: unknown} {
    return {
      [this.textField]: '',
      [this.valueField]: null,
    };
  }

  constructor(filterService: FilterService) {
    super(filterService);
  }

  public onChange(value: unknown): void {
    this.applyFilter(
      value === null // value of the default item
        ? this.removeFilter(this.gridField ?? this.valueField) // remove the filter
        : this.updateFilter({
            // add a filter for the field with the value
            field: this.gridField ?? this.valueField,
            operator: 'eq',
            value: value,
          })
    );
  }
}
