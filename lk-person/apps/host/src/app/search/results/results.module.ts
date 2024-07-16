import {NgModule} from "@angular/core";
import {ResultsComponent} from "./results.component";
import {NgForOf, NgIf} from "@angular/common";

@NgModule({
  declarations: [ResultsComponent],
  imports: [
    NgIf,
    NgForOf
  ],
  exports: [ResultsComponent]
})
export class ResultsModule {}
