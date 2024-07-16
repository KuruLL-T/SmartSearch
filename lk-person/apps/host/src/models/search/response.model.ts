import {ItemModel} from "./item.model";

export interface ResponseModel {
  items: ItemModel[],
  totalItemNumber: number
}
