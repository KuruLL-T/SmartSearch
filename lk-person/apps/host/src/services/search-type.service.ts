import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {TypeModel} from "../models/search/type.model";

@Injectable({
  providedIn: 'root'
})
export class SearchTypeService {
  private apiUrl = "https://localhost:7058/api/type/get-allowed-types"

  constructor(private http: HttpClient) {}

  getAllowedTypes(): Observable<TypeModel[]> {
    return this.http.get<TypeModel[]>(`${this.apiUrl}`);
  }
}
