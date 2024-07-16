export enum FieldType {
  Textbox,
  Order,
  Number,
  Float,
  Checkbox,
  Combobox,
  Multiselect,
  Autocomplete,
  Color,
  Link,
}

export const numericFields = [FieldType.Order, FieldType.Number, FieldType.Float];

export const listOptionsFields = [FieldType.Combobox, FieldType.Multiselect, FieldType.Autocomplete];

export const nonFilterableFields = [
  FieldType.Checkbox,
  FieldType.Autocomplete,
  FieldType.Color,
  FieldType.Link,
];

export const leftAlignmentFields = [
  FieldType.Textbox,
  FieldType.Combobox,
  FieldType.Multiselect,
  FieldType.Autocomplete,
];
