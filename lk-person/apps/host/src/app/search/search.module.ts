import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {HttpClientModule} from "@angular/common/http";
import {SearchComponent} from "./search.component";
import {InputsModule} from "@progress/kendo-angular-inputs";
import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {FormsModule} from "@angular/forms";
import {ListViewModule} from "@progress/kendo-angular-listview";
import {FiltersModule} from "./filters/filters.module";
import {ResultsModule} from "./results/results.module";
import {AdditionalModule} from "./additional/additional.module";

@NgModule({
  declarations: [SearchComponent],
    imports: [
        CommonModule,
        HttpClientModule,
        BrowserModule,
        BrowserAnimationsModule,
        FormsModule,
        InputsModule,
        ListViewModule,
        FiltersModule,
        ResultsModule,
        AdditionalModule
    ],
  exports: [
    SearchComponent
  ]
})
export class SearchModule {}
