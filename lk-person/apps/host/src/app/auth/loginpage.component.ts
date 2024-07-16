import {Component, OnInit} from '@angular/core';
import {environment} from '../../environments/environment';
import {Location} from '@angular/common';

@Component({
  template: '',
  selector: 'login-page',
})
export class LoginpageComponent implements OnInit {
  constructor(private location: Location) {}

  ngOnInit() {
    window.location.href = this.authUrlGenerate();
  }

  public login() {
    window.location.href = this.authUrlGenerate();
  }

  private authUrlGenerate() {
    let url = `${environment.authEndPoint}/idp/sso/oauth`;
    url +=
      '?client_id=' +
      environment.trustedNetClientId +
      '&redirect_uri=' +
      encodeURIComponent(`${window.location.protocol}//${window.location.host}/callback`) +
      '&scope=userprofile';
    localStorage.setItem('last_url', this.location.path());
    return url;
  }
}
