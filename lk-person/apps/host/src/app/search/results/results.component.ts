import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges} from "@angular/core";
import {SearchService} from "../../../services/search.service";
import {RequestModel} from "../../../models/search/request.model";
import {ResponseModel} from "../../../models/search/response.model";
import {SearchFiltersService} from "../../../services/search-filters.service";
import {Subscription} from "rxjs";
import {ServiceModel} from "../../../models/search/service.model";
import {TypeModel} from "../../../models/search/type.model";
import {Router} from "@angular/router";

@Component({
  selector: "app-search-results",
  templateUrl: "./results.component.html",
  styleUrls: ["./results.component.scss"]
})
export class ResultsComponent implements OnInit, OnChanges, OnDestroy {
  @Input() searchString: string = "";
  searchResults: ResponseModel = {items: [], totalItemNumber: 0};
  private serviceSubscription: Subscription = new Subscription();
  private typeSubscription: Subscription = new Subscription();
  private searchTermSubscription: Subscription = new Subscription();
  selectedServices: ServiceModel[] = [];
  selectedTypes: TypeModel[] = [];
  selectedSearchTerm = 0;

  constructor(
    private searchService: SearchService,
    private searchFiltersService: SearchFiltersService,
    private router: Router,
  ) { }

  ngOnInit() {
    this.serviceSubscription = this.searchFiltersService.selectedServices$.subscribe(
      (services) => {
        this.selectedServices = services
        this.OnSearch();
      }
    );

    this.typeSubscription = this.searchFiltersService.selectedTypes$.subscribe(
      (types) => {
        this.selectedTypes = types;
        this.OnSearch();
      }
    )

    this.searchTermSubscription = this.searchFiltersService.selectedSearchTerm$.subscribe(
      (searchTerm) => {
        this.selectedSearchTerm = searchTerm;
        this.OnSearch();
      }
    )
  }

  ngOnDestroy() {
    this.serviceSubscription.unsubscribe();
    this.typeSubscription.unsubscribe();
    this.searchTermSubscription.unsubscribe();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['searchString']) {
      this.OnSearch()
    }
  }

  OnSearch(): void {
    const requestDto: RequestModel = {
      searchString: this.searchString,
      servicesId: this.selectedServices.map(item => item.id),
      typesId: this.selectedTypes.map(item => item.id),
      searchTerm: this.selectedSearchTerm,
      skipped: 0
    }

    this.searchService.Search(requestDto).subscribe(
      (response) => {
        this.searchResults = response;
      },
    (error) => {
        console.log(error);
    }
    )
  }

  Navigate(link: string) {
    this.router.navigate([link]);
  }
}
