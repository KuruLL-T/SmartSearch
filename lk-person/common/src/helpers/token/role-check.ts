import {JwtHelperService} from '@auth0/angular-jwt';
import {TokenStorageService} from '../../services/token.service';

export function checkRole(tokenStore: TokenStorageService, jwtHelper: JwtHelperService, role: string) {
  const token = tokenStore.getAuthToken();
  if (token !== null) {
    const user = jwtHelper.decodeToken(token);
    const roles = user?.role;
    if (!roles) return false;
    return roles.includes(role);
  }
  return false;
}
