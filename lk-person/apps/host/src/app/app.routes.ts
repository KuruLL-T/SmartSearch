import {Route} from '@angular/router';
import {CallBackComponent} from './auth/callback.component';
import {LoginpageComponent} from './auth/loginpage.component';
import {loadRemoteModule} from '@angular-architects/module-federation';
import {environment} from '../environments/environment';
import {PlaceholderModule} from './placeholder/placeholder.module';

export const appRoutes: Route[] = [
  ...parseAvailableServices(),
  {path: 'callback', component: CallBackComponent},
  {path: 'login', component: LoginpageComponent},
];

function parseAvailableServices() {
  const items: Route[] = [];
  const services: {name: string, path: string, remoteURL: string}[] = environment.services;
  services.forEach((item) => {
    items.push({
      path: item.path,
      loadChildren: () =>
        loadRemoteModule({
          type: 'manifest',
          remoteName: item.name,
          exposedModule: './Module',
        }).then((m) => m.RemoteEntryModule)
          .catch((e) => {
            console.error(e);
            return PlaceholderModule;
          })})
  })
  return items;
}
