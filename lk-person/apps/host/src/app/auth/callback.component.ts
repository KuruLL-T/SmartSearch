import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ActivatedRoute} from '@angular/router';
import {TokenStorageService} from 'common';
import {environment} from '../../environments/environment';

@Component({
  template: '',
  selector: 'callback',
})
export class CallBackComponent implements OnInit {
  code = '';
  invalidLogin?: boolean;

  constructor(
    private tokenStoreService: TokenStorageService,
    private httpClient: HttpClient,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      this.code = params['code'];
      // for (const paramsKey in params) {
      //   this.queryString += `${this.queryString.length == 0 ? "" : "&"}${paramsKey}=${params[paramsKey]}`
      // }

      if (this.code && this.code.length > 0)
        this.httpClient
          .get(
            `${environment.apiEndpoint}auth/gettoken?code=${this.code}&client_id=${environment.trustedNetClientId}`
          )
          .subscribe(
            (response) => {
              this.tokenStoreService.setToken(response);

              if (this.tokenStoreService.getSwitchUserToken()) {
                this.tokenStoreService.deleteSwitchUserToken();
              }

              this.invalidLogin = false;
              const lastUrl = localStorage.getItem('last_url')
              if (lastUrl && lastUrl!='/login') {
                window.location.href = lastUrl;
                localStorage.removeItem('last_url')
              }
              else {
                window.location.href = '/';
              }
            },
            () => {
              this.invalidLogin = true;
            }
          );
    });
  }
}
