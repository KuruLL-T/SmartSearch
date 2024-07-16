import {PagerSettings} from '@progress/kendo-angular-grid';

export const dictPagerSettings: PagerSettings = {
  type: 'numeric',
  buttonCount: 5,
  pageSizes: [10, 20, 50],
  info: true,
  previousNext: true,
  position: 'bottom',
};
