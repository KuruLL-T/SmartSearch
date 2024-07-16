import {Injectable} from '@angular/core';
import {CanActivate, Router, UrlTree} from '@angular/router';
import {JwtHelperService} from '@auth0/angular-jwt';
import {Observable} from 'rxjs';
import {TokenStorageService} from '../services/token.service';
import {Location} from '@angular/common';
import {Role} from '../models/role';

@Injectable({
  providedIn: 'root',
})
export class AdminRoleGuard implements CanActivate {
  constructor(
    private tokenStore: TokenStorageService,
    private jwtHelper: JwtHelperService,
    private location: Location,
    private router: Router
  ) {}

  canActivate(): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const token = this.tokenStore.getAuthToken();
    if (token !== null) {
      const user = this.jwtHelper.decodeToken(token);
      const roles = user?.role;
      if (roles?.includes(Role.Admin)) return true;

      alert('Недостаточно прав для доступа на эту страницу');
    }
    localStorage.setItem('last_url', this.location.path());
    this.router.navigate([`/profile`]);
    return false;
  }
}
