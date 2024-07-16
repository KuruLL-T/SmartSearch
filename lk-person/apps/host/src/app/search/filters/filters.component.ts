import {Component} from "@angular/core";
import {ServiceModel} from "../../../models/search/service.model";

@Component({
  selector: "app-search-filters",
  templateUrl: "./filters.component.html",
  styleUrls: ["./filters.component.scss"]
})
export class FiltersComponent {
  selectedServices: ServiceModel[] = [];

  updateSelectedServices(services: ServiceModel[]): void {
    this.selectedServices = [...services];
  }
}
