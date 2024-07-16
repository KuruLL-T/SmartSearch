'use strinct';

import {environment} from '../environments/environment';

export const tokenStore = 'lk_service';
export const currentToken = 'currentToken';

export const WebpackRemotes = {
  profile: environment.production
    ? 'https://profile-dev-lk.univuz.ru/remoteEntry.js'
    : 'http://localhost:8450/remoteEntry.js',
  currentcontrol: environment.production
    ? 'https://currentcontrol-dev-lk.univuz.ru/remoteEntry.js'
    : 'http://localhost:8453/remoteEntry.js',
  contingent: environment.production
    ? 'https://contingent-dev-lk.univuz.ru/remoteEntry.js'
    : 'http://localhost:8455/remoteEntry.js',
  dicts: environment.production
    ? 'https://dicts-dev-lk.univuz.ru/remoteEntry.js'
    : 'http://localhost:8452/remoteEntry.js',
  announcements: environment.production
    ? 'https://announcements-dev-lk.univuz.ru/remoteEntry.js'
    : 'http://localhost:8451/remoteEntry.js',
  middlecontrol: environment.production
    ? 'https://middlecontrol-dev-lk.univuz.ru/remoteEntry.js'
    : 'http://localhost:8454/remoteEntry.js',
  education: environment.production
    ? 'https://education-dev-lk.univuz.ru/remoteEntry.js'
    : 'http://localhost:8456/remoteEntry.js',
  persondepartment: environment.production
    ? 'https://persondepartment-dev-lk.univuz.ru/remoteEntry.js'
    : 'http://localhost:8460/remoteEntry.js',
  disciplineworkload: environment.production
    ? 'https://disciplineworkload-dev-lk.univuz.ru/remoteEntry.js'
    : 'http://localhost:8457/remoteEntry.js',
  classroom: environment.production
    ? 'https://classroom-dev-lk.univuz.ru/remoteEntry.js'
    : 'http://localhost:8458/remoteEntry.js',
  publications: environment.production
    ? 'https://publications-dev-lk.univuz.ru/remoteEntry.js'
    : 'http://localhost:8459/remoteEntry.js',
};
