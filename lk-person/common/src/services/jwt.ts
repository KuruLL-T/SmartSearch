import {TokenStorageService} from './token.service';
import {environment} from '../environments/environment';

export function jwtOptionsFactory(tokenStorageService: TokenStorageService) {
  return {
    tokenGetter: () => {
      return tokenStorageService.getAuthToken();
    },
    allowedDomains: environment.allowedDomains,
  };
}
