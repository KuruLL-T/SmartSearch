import {NgModule} from "@angular/core";
import {FiltersComponent} from "./filters.component";
import {CategoryModule} from "./category/category.module";
import {ServiceModule} from "./service/service.module";

@NgModule({
  declarations: [FiltersComponent],
  imports: [
    CategoryModule,
    ServiceModule
  ],
  exports: [FiltersComponent]
})
export class FiltersModule { }
