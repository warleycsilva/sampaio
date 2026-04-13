import {Injectable} from '@angular/core';
import {environment} from '../../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {AppService} from './app.service';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  private url = environment.urls;

  constructor(private http: HttpClient, private appService: AppService) {
  }

  changePasswordWithToken = (data: any): Observable<any> =>
    this.http.post<any>(`${this.url.api2}/v1/profile/reset-password-with-token`, data).pipe(map(resp => resp));

  activateAccount = (data: any): Observable<any> =>
    this.http.post<any>(`${this.url.api2}/v1/profile/activate-account`, data).pipe(map(resp => resp));

  changePassword = (data: any): Observable<any> =>
    this.http.post<any>(`${this.url.api2}/v1/profile/change-password`, data).pipe(map(resp => resp));

  acceptTerms = (): Observable<any> =>
    this.http.get<any>(`${this.url.api2}/v1/profile/accept-terms-conditions`).pipe(map(resp => resp));
}
