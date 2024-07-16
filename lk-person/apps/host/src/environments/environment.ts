export interface Environment {
  production: Window | boolean | string;
  demo: Window | boolean | string;
  searchBotDisallow: Window | boolean;
  apiEndpoint: Window | string;
  allowedDomains: any;
  trustedNetClientId: Window | string;
  authEndPoint: Window | string;
  isDayOffEndpoint: string;
  educationApiEndpoint: Window | string;
  lkPersonApiEndpoint: Window | string;
  lkStudentApiEndpoint: Window | string;
  fileServerEndpoint: Window | string;
  announcementApiEndpoint: Window | string;
  classroomApiEndpoint: Window | string;
  daysInWeek: number;
  creditUnitsValue: number;
  supportEmail: Window | string;
  supportPhoneNumber: Window | string;
  maidenName: Window | string;
  orderSed: Window | string;
  headerTitle: any;
  remoteEntryTemplateURL: any;
  apiPaths: any;
  services: any;
  menuItems: any;
  announcement: any;
  contingent: any;
  publications: any;
}

export const environment: Environment = {
  production: window[<any>'env'][<any>'prod'] || false,
  demo: window[<any>'env'][<any>'demo'] || false,
  searchBotDisallow: window[<any>'env'][<any>'searchBotDisallow'] || false,
  apiEndpoint: window[<any>'env'][<any>'apiUrl'],
  allowedDomains: [window[<any>'env'][<any>'allowedDomains']],
  trustedNetClientId: window[<any>'env'][<any>'clientId'],
  authEndPoint: window[<any>'env'][<any>'authUrl'],
  educationApiEndpoint: window[<any>'env'][<any>'apiUrl'],
  lkPersonApiEndpoint: window[<any>'env'][<any>'apiUrl'],
  lkStudentApiEndpoint: window[<any>'env'][<any>'apiUrlLk'],
  fileServerEndpoint: window[<any>'env'][<any>'apiUrlFile'],
  announcementApiEndpoint: window[<any>'env'][<any>'apiUrl'],
  classroomApiEndpoint: window[<any>'env'][<any>'apiUrl'],
  isDayOffEndpoint: `https://isdayoff.ru/api/getdata?year=`,
  daysInWeek: 6,
  creditUnitsValue: 1.5,
  supportEmail: window[<any>'env'][<any>'supportEmail'],
  supportPhoneNumber: window[<any>'env'][<any>'supportPhoneNumber'],
  maidenName: window[<any>'env'][<any>'maidenName'],
  orderSed: window[<any>'env'][<any>'orderSed'],
  headerTitle: {
    full: window[<any>'env'][<any>'fullHeader'],
    short: window[<any>'env'][<any>'shortHeader'],
  },
  remoteEntryTemplateURL: window[<any>'env'][<any>'remoteEntryTemplateURL'],
  services: window[<any>'env'][<any>'services'],
  menuItems: window[<any>'env'][<any>'menuItems'],
  announcement: {
    base: 'announcement',
    maxFileSize: window[<any>'env'][<any>'announcement_maxFileSize'] || 20971520, //20971520,
    commentMaxFileSize: window[<any>'env'][<any>'announcement_commentMaxFileSize'] || 5242880, //5242880,
  },
  contingent: {
    maxFileSize: window[<any>'env'][<any>'contingent_maxFileSize'] || 4194304, //4194304,
  },
  publications: {
    maxFileSize: window[<any>'env'][<any>'publications_maxFileSize'] || 10485760,
  },
  apiPaths: {
    admin: 'lkperson/admin',
    lkPerson: 'lkperson/profile',
    announcement: {
      base: 'announcement',
    },
    publications: {
      main: 'publications',
    },
  },
};
