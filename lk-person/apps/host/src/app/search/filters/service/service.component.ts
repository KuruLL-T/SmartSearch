import {Component, EventEmitter, OnInit, Output} from "@angular/core";
import {ServiceModel} from "../../../../models/search/service.model";
import {SearchService} from "../../../../services/search-service.service";
import {SearchFiltersService} from "../../../../services/search-filters.service";

@Component({
  selector: "app-search-filters-service",
  templateUrl: "./service.component.html",
  styleUrls: ["./service.component.scss"]
})
export class ServiceComponent implements OnInit {
  services: ServiceModel[] = [];
  selectedServices: ServiceModel[] = [];
  @Output() selectedServicesChange = new EventEmitter<ServiceModel[]>();

  constructor(
    private service: SearchService,
    private searchFiltersService: SearchFiltersService) {  }

  SwapSelectedServices(service: ServiceModel): void {
    if (this.selectedServices.includes(service)) {
      this.selectedServices.splice(this.selectedServices.indexOf(service), 1);
    } else {
      this.selectedServices.push(service);
    }

    this.searchFiltersService.UpdateSelectedServices(this.selectedServices);

    this.selectedServicesChange.emit(this.selectedServices);
  }

  ngOnInit() {
    this.service.getAllowedServices().subscribe(
      (data) => {
        this.services = data
      },
    (error) => {
        console.log(error);
    }
    )
  }
}
