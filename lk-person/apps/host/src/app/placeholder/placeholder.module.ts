import {NgModule} from '@angular/core';
import {SharedModule} from 'common';
import {Route, RouterModule} from '@angular/router';
import {PlaceholderComponent} from './placeholder';

const remoteRoutes: Route[] = [{path: '', component: PlaceholderComponent, pathMatch: 'full'}];

@NgModule({
  declarations: [],
  imports: [SharedModule, RouterModule.forChild(remoteRoutes)],
  providers: [],
})
export class PlaceholderModule {}
