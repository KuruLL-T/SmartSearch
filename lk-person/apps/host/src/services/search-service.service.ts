import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ServiceModel} from "../models/search/service.model";

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  private apiUrl = "https://localhost:7058/api/service/get-allowed-services"

  constructor(private http: HttpClient) {}

  getAllowedServices(): Observable<ServiceModel[]> {
    return this.http.get<ServiceModel[]>(`${this.apiUrl}`);
  }
}
