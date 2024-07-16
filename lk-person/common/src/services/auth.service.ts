import {Injectable} from '@angular/core';
import {TokenStorageService} from './token.service';
import {JwtHelperService} from '@auth0/angular-jwt';
import {Role} from "../models/role";

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private tokenStoreService: TokenStorageService, protected jwtHelper: JwtHelperService) {}

  public isUserAuthenticated() {
    const token = this.tokenStoreService.getAuthToken();
    return !!(token && !this.jwtHelper.isTokenExpired(token));
  }

  public decodeToken() {
    const token = this.tokenStoreService.getAuthToken();
    if (token) return this.jwtHelper.decodeToken(token);
    return {};
  }

  public get isAdmin() {
    const token = this.tokenStoreService.getAuthToken();

    if (!token) {
      return false;
    }

    const user = this.jwtHelper.decodeToken(token);
    const roles = user?.role;
    return roles?.includes(Role.Admin);
  }
}
