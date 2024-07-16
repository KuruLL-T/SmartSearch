import {platformBrowserDynamic} from '@angular/platform-browser-dynamic';
import {enableProdMode} from '@angular/core';
import {AppModule} from './app/app.module';
import {environment} from './environments/environment';

if (environment.production) {
  enableProdMode();
}

if (environment.searchBotDisallow) {
  const meta = document.querySelector('meta') ?? document.createElement('meta');
  meta.setAttribute('name', 'robots');
  meta.setAttribute('content', 'none');
}

platformBrowserDynamic()
  .bootstrapModule(AppModule)
  .catch((err) => console.error(err));
