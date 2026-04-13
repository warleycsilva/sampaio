import {Injectable} from '@angular/core';
import {environment} from '../../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {AppService} from './app.service';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AddressService {

  private url = environment.urls;

  constructor(private http: HttpClient, private appService: AppService) {
  }

  getStates = (): Observable<any> =>
    this.http.get<any>(`${this.url.api2}/v1/states`).pipe(map(resp => resp));

  getDataByCep = (cep: string): Observable<any> => {
    console.log('cep_route', `${this.url.api2}/v1/states/cep/${cep}`)
    return this.http.get<any>(`${this.url.api2}/v1/states/cep/${cep}`).pipe(map(resp => resp));
  };

  getCitiesByState = (id: string): Observable<any> =>
    this.http.get<any>(`${this.url.api2}/v1/states/${id}/cities`).pipe(map(resp => resp));

}
