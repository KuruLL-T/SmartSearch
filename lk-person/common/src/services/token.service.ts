import {Injectable} from '@angular/core';
import {tokenStore} from '../options/settings';
import {Role} from "../models/role";

@Injectable({
  providedIn: 'root',
})
export class TokenStorageService {
  setToken(data: unknown): void {
    localStorage.removeItem(tokenStore);
    localStorage.setItem(tokenStore, JSON.stringify(data));
  }

  getAuthToken(): string | null {
    const tokens = this.getToken();
    if (tokens == null) return null;
    return tokens.accessToken;
  }

  getAuthTokenType(): string | null {
    const tokens = this.getToken();
    if (tokens == null) return null;
    return tokens.tokenType;
  }

  getToken() {
    const tokensJson = localStorage.getItem(tokenStore);
    if (tokensJson == null) return null;

    return JSON.parse(tokensJson);
  }

  deleteToken(): void {
    localStorage.removeItem(tokenStore);
  }

  getSwitchUserToken() {
    return localStorage.getItem('switchPerson') === 'true';
  }

  deleteSwitchUserToken() {
    localStorage.removeItem('switchPerson');
  }
}
