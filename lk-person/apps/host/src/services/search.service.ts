import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {RequestModel} from "../models/search/request.model";
import {ResponseModel} from "../models/search/response.model";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  private apiUrl = 'https://localhost:7058/api/SearchItem/search';

  constructor(private http: HttpClient) {}

  Search(dto: RequestModel): Observable<ResponseModel> {
    return this.http.post<ResponseModel>(this.apiUrl, dto)
  }
}
