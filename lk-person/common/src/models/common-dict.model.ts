import {Observer} from 'rxjs';
import {FieldType} from '../enums/common-dict.enum';

export type EventData<T> = {
  value: T;
  observer: Partial<Observer<T>>;
};

export type EditEvent<T> = {
  isNew: boolean;
  dataItem?: T;
};

export type ListOptions = {
  data: any[];
  textField: string;
  valueField: string;
};

export type FieldInfo<T> = {
  field?: keyof T & string;
  type: FieldType;
  title?: string;
  width: number;
  listOptions?: ListOptions;
  boolOptions?: Map<boolean, string>;
  iconClass?: string;
  clickHandler?: (dataItem?: T) => void;
  sortable?: boolean;
  filterable?: boolean;
  useForDelete?: boolean;
};
