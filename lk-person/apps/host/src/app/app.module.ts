import {LOCALE_ID, NgModule} from '@angular/core';
import {AppComponent} from './app.component';
import {RouterModule} from '@angular/router';
import {appRoutes} from './app.routes';
import localeRu from '@angular/common/locales/ru';
import {IntlModule} from '@progress/kendo-angular-intl';
import '@progress/kendo-angular-intl/locales/ru/all';
import {registerLocaleData} from '@angular/common';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from '@angular/common/http';
import {TokenInterceptor} from 'common';
import {JWT_OPTIONS, JwtModule} from '@auth0/angular-jwt';
import {jwtOptionsFactory} from 'common';
import {TokenStorageService} from 'common';
import {SharedModule} from 'common';
import {CallBackComponent} from './auth/callback.component';
import {LoginpageComponent} from './auth/loginpage.component';
import {BrowserModule} from '@angular/platform-browser';
import {PlaceholderComponent} from './placeholder/placeholder';
import {SearchModule} from "./search/search.module";

registerLocaleData(localeRu);

@NgModule({
  declarations: [AppComponent, CallBackComponent, LoginpageComponent, PlaceholderComponent],
  imports: [
    SearchModule,
    HttpClientModule,
    BrowserModule,
    BrowserAnimationsModule,
    IntlModule,
    SharedModule,
    RouterModule.forRoot(appRoutes, {initialNavigation: 'enabledBlocking'}),
    JwtModule.forRoot({
      jwtOptionsProvider: {
        provide: JWT_OPTIONS,
        useFactory: jwtOptionsFactory,
        deps: [TokenStorageService],
      },
    }),
  ],
  providers: [
    {
      deps: [HttpClient],
      provide: LOCALE_ID,
      useValue: 'ru',
    },
    {provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true},
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
