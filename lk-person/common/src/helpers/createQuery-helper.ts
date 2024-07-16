import {HttpParams} from '@angular/common/http';

// Works for GET requests
export function CreateQuery(queryModel: unknown) {
  let query = new HttpParams();
  if (queryModel instanceof Object) {
    for (const [key, value] of Object.entries(queryModel)) {
      // Value is array
      if (Array.isArray(value) && value != null && !value.includes('')) {
        query = query.appendAll({[key]: value});
      }
      // Value is object
      else if (value != null && !Array.isArray(value)) {
        query = query.append(key, `${value}`);
      }
    }
  }
  return query;
}
