import {Component, Input, OnChanges, OnInit, SimpleChanges} from "@angular/core";
import {TypeModel} from "../../../../models/search/type.model";
import {SearchTypeService} from "../../../../services/search-type.service";
import {ServiceModel} from "../../../../models/search/service.model";
import {SearchFiltersService} from "../../../../services/search-filters.service";

@Component({
  selector: "app-search-filters-category",
  templateUrl: "./category.component.html",
  styleUrls: ["./category.component.scss"]
})
export class CategoryComponent implements OnInit, OnChanges {
  @Input() selectedServices: ServiceModel[] = [];
  types: TypeModel[] = [];
  filteredTypes: TypeModel[] = [];
  selectedTypes: TypeModel[] = [];

  constructor(
    private typeService: SearchTypeService,
    private searchFiltersService: SearchFiltersService,) {
  }

  SwapSelectedTypes(type: TypeModel) {
    if (this.selectedTypes.includes(type)) {
      this.selectedTypes.splice(this.selectedTypes.indexOf(type), 1);
    } else {
      this.selectedTypes.push(type);
    }

    this.searchFiltersService.UpdateSelectedTypes(this.selectedTypes);
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['selectedServices']) {
      this.FilterTypes();
    }
  }

  FilterTypes() {
    if (this.selectedServices.length === 0) {
      this.filteredTypes = this.types
    } else {
      const selectedServicesIds = this.selectedServices.map(item => item.id);
      this.filteredTypes = this.types.filter(type => selectedServicesIds.includes(type.serviceId));
      this.selectedTypes = this.filteredTypes.filter(type => this.selectedTypes.includes(type));
      this.searchFiltersService.UpdateSelectedTypes(this.selectedTypes);
    }
  }

  ngOnInit() {
    this.typeService.getAllowedTypes().subscribe(
      (data) => {
        this.types = data;
        this.filteredTypes = data;
      },
      (error) => {
        console.log(error);
      }
    )
  }
}
