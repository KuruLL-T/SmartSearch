import {Component} from "@angular/core";

@Component({
  selector: "app-search",
  templateUrl: "./search.html",
  styleUrls: ["./search.component.scss"]
})
export class SearchComponent {
  searchQuery: string = "";

  constructor() {}
}
