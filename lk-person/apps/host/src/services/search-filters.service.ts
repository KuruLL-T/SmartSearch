import {Injectable} from "@angular/core";
import {BehaviorSubject} from "rxjs";
import {ServiceModel} from "../models/search/service.model";
import {TypeModel} from "../models/search/type.model";

@Injectable({
  providedIn: 'root'
})
export class SearchFiltersService {
  private selectedServicesSubject = new BehaviorSubject<ServiceModel[]>([]);
  selectedServices$ = this.selectedServicesSubject.asObservable();

  private selectedTypesSubject = new BehaviorSubject<TypeModel[]>([]);
  selectedTypes$ = this.selectedTypesSubject.asObservable();

  private selectedSearchTermSubject = new BehaviorSubject<number>(0);
  selectedSearchTerm$ = this.selectedSearchTermSubject.asObservable();

  UpdateSelectedServices(services: ServiceModel[]): void {
    this.selectedServicesSubject.next(services);
    this.filterSelectedTypes();

  }

  UpdateSelectedTypes(types: TypeModel[]): void {
    this.selectedTypesSubject.next(types);
  }

  UpdateSelectedSearchTerm(searchTerm: number): void {
    this.selectedSearchTermSubject.next(searchTerm);
  }

  private filterSelectedTypes(): void {
    const selectedServicesIds = this.selectedServicesSubject.value.map(service => service.id);
    const filteredTypes = this.selectedTypesSubject.value.filter(type =>
      selectedServicesIds.includes(type.serviceId)
    );
    this.selectedTypesSubject.next(filteredTypes);
  }
}
