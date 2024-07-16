import {NgModule} from "@angular/core";
import {CategoryComponent} from "./category.component";
import {NgClass, NgForOf} from "@angular/common";

@NgModule({
  declarations: [CategoryComponent],
    imports: [NgForOf, NgClass],
  exports: [CategoryComponent]
})
export class CategoryModule { }
