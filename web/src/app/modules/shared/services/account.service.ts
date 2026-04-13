import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

import { AppService } from "./app.service";
import { environment } from "../../../../environments/environment";
import {
  Bank,
  Account,
  ItemPix,
  BodyParamCreateAccount,
  ResponseAccountRequest,
} from "../models/account";
import { IGenericResponse } from "../models/generic";

@Injectable({
  providedIn: "root",
})
export class AccountService {
  private url = environment.urls;

  constructor(private http: HttpClient, private appService: AppService) { }

  getListBanks = (): Observable<Bank[]> =>
    this.http
      .get<any>(`${this.url.api2}/v1/banks`)
      .pipe(map((resp) => resp.data));

  createAccountPix = (
    idLessee: string,
    data: ItemPix
  ): Observable<IGenericResponse> =>
    this.http
      .post<any>(
        `${this.url.api2}/backoffice/v1/pix-keys/add/${idLessee}`,
        data
      )
      .pipe(map((resp) => resp));

  deleteAccountPix = (idLessee: string): Observable<IGenericResponse> =>
    this.http
      .delete<any>(`${this.url.api2}/backoffice/v1/pix-keys/delete/${idLessee}`)
      .pipe(map((resp) => resp));

  createAccount = (
    data: BodyParamCreateAccount
  ): Observable<ResponseAccountRequest> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/bank-data`, data)
      .pipe(map((resp) => resp));

  editAccount = (
    idAccount: string,
    data: BodyParamCreateAccount
  ): Observable<ResponseAccountRequest> =>
    this.http
      .put<any>(`${this.url.api.v1}/backoffice/bank-data/${idAccount}`, data)
      .pipe(map((resp) => resp));

  deleteAccount = (
    idAccount: string
  ): Observable<{ data: ResponseAccountRequest }> =>
    this.http
      .delete<any>(`${this.url.api.v1}/backoffice/bank-data/${idAccount}`)
      .pipe(map((resp) => resp));

  getDetailsAccount = (idAccount: string): Observable<BodyParamCreateAccount> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/bank-data/${idAccount}`)
      .pipe(map((resp) => resp));

  getDetailsAccountByLesseeId = (lesseeId: string): Observable<BodyParamCreateAccount> =>
    this.http
      .get<any>(`${this.url.api2}/locator/v1/bank-data/lessee-account/${lesseeId}`)
      .pipe(map((resp) => resp));
}
