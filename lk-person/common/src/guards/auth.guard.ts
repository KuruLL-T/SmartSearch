import {Injectable} from '@angular/core';
import {CanActivate, Router} from '@angular/router';
import {JwtHelperService} from '@auth0/angular-jwt';
import {TokenStorageService} from '../services/token.service';
import {Location} from '@angular/common';
import {User} from '../models/user';
import {environment} from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private jwtHelper: JwtHelperService,
    private router: Router,
    private location: Location,
    private tokenStore: TokenStorageService
  ) {}

  canActivate() {
    const token = this.tokenStore.getAuthToken();

    if (!token) {
      return false;
    }

    const user: User = this.jwtHelper.decodeToken(token);

    if (this.jwtHelper.isTokenExpired(token) || (!user.person_id && localStorage.getItem('last_url'))) {
      localStorage.setItem('last_url', this.location.path());
    }

    if (!this.jwtHelper.isTokenExpired(token) && user.person_id) {
      return true;
    }

    if (!user.person_id) {
      alert(environment.invalidTokenMessage);
    }

    this.router.navigateByUrl('/login');
    return false;
  }
}
