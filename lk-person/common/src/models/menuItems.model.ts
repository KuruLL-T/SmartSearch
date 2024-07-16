export interface MenuItems {
  text: string;
  icon?: string;
  items: (MenuItem | MenuItems)[];
}

export interface MenuItem {
  text: string;
  path?: string;
  admin?: boolean;
}
