import {BreadCrumbItem} from '@progress/kendo-angular-navigation';

export interface MenuItem {
  text?: string;
  icon?: string;
  path?: string;
  selected?: boolean;
  separator?: boolean;
  id?: string;
  parentId?: string;
  url?: string;
  return?: boolean;
  padding?: string;
  default?: boolean;
}

export interface DisableBreadCrumbRoutes extends BreadCrumbItem {
  address?: string;
}
