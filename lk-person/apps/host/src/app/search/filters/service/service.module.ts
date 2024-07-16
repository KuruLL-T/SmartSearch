import {NgModule} from "@angular/core";
import {ServiceComponent} from "./service.component";
import {NgClass, NgForOf} from "@angular/common";

@NgModule({
  declarations: [ServiceComponent],
  imports: [
    NgForOf,
    NgClass
  ],
  exports: [ServiceComponent]
})
export class ServiceModule {}
