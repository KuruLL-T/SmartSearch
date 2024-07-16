import {NgModule} from "@angular/core";
import {AdditionalComponent} from "./additional.component";
import {NgClass} from "@angular/common";

@NgModule({
    declarations: [AdditionalComponent],
    exports: [AdditionalComponent],
    imports: [
        NgClass
    ]
})
export class AdditionalModule {}
