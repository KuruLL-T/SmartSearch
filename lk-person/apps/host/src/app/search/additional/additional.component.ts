import {Component} from "@angular/core";
import {SearchFiltersService} from "../../../services/search-filters.service";

@Component({
  selector: "app-search-additional",
  templateUrl: "./additional.component.html",
  styleUrls: ["./additional.component.scss"],
})
export class AdditionalComponent {
  constructor(private searchFiltersService: SearchFiltersService) {}

  searchTerms = new Map<string, number>(Object.entries(
    {
      "none": 0,
      "begins": 1,
      "contains": 2,
      "excludes": 3
    }
  ));

  searchTerm = 0;
  searchId = '';

  HandleSearchTerm(event: Event): void {
    const element = event.target as HTMLElement;

    this.searchId = element.id;

    if (this.searchTerm === this.searchTerms.get(element.id)) {
      this.searchTerm = 0;
      this.searchId = 'none';
    } else {
      this.searchTerm = this.searchTerms.get(element.id) ?? 0;
    }

    this.searchFiltersService.UpdateSelectedSearchTerm(this.searchTerm);
  }
}
