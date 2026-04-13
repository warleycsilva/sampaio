import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { Observable } from "rxjs";
import { AppService } from "./app.service";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  private url = environment.urls;

  constructor(private http: HttpClient, private appService: AppService) {}

  login = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/auth`, data)
      .pipe(map((resp) => resp));

  signUp = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api2}/signup/v1/signup`, data)
      .pipe(map((resp) => resp));

  logout = (logoutId: string): Observable<any> =>
    this.http
      .get<any>(`${this.url}/auth/logout/?logoutId=${logoutId}`)
      .pipe(map((resp) => resp));

  isLogged = (): boolean =>
    this.appService.accessToken != null ? true : false;

  remember = (data: { email: string }): Observable<any> =>
    this.http
      .post(`${this.url.api.v1}/profile/reset-password-request`, data)
      .pipe(map((resp) => resp));

  localLogout = (): Observable<any> =>
    this.http
      .get<any>(`${this.url}/auth/local-logout`)
      .pipe(map((resp) => resp));

  getError = (errorId: string): Observable<any> =>
    this.http
      .get<any>(`${this.url}/auth/error/?errorId=${errorId}`)
      .pipe(map((resp) => resp));

  getAuthorizeUrl = (
    solutionId: string,
    redirectUrl: string
  ): Observable<any> =>
    this.http
      .post<any>(`${this.url}/auth/authorize-url`, { solutionId, redirectUrl })
      .pipe(map((resp) => resp));

  // Backoffice:

  loginBackoffice = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/auth`, data, {
        withCredentials: true,
      })
      .pipe(map((resp) => resp));
  loginHangfire = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/auth/hangfire`, data, {
        withCredentials: true,
      })
      .pipe(map((resp) => resp));

  isLoggedBackoffice = (): boolean =>
    this.appService.accessToken != null ? true : false;
  localLogoutBackoffice = (): Observable<any> =>
    this.http
      .get<any>(`${this.url}/auth/backoffice/local-logout`)
      .pipe(map((resp) => resp));

  getErrorBackoffice = (errorId: string): Observable<any> =>
    this.http
      .get<any>(`${this.url}/auth/backoffice/error/?errorId=${errorId}`)
      .pipe(map((resp) => resp));

  getAuthorizeUrlBackoffice = (
    solutionId: string,
    redirectUrl: string
  ): Observable<any> =>
    this.http
      .post<any>(`${this.url}/auth/backoffice/authorize-url`, {
        solutionId,
        redirectUrl,
      })
      .pipe(map((resp) => resp));
}
