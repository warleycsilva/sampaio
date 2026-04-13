import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

import { AppService } from "./app.service";
import { environment } from "../../../../environments/environment";
import { CustomersResponse } from "../models/customer";
import { IItensOrderPut } from "../models/orderItemDelivery";
import { ILesseeGeolocation } from "../models/lesseeGeolocation";
import IResponseRoutes from "../models/geolocationRoutes";
import { UserDevice } from "../models/user-device";
import { IGenericResponse, IGenericResponseData } from "../models/generic";

@Injectable({
  providedIn: "root",
})
export class BackofficeViagensService {
  private url = environment.urls;

  constructor(private http: HttpClient) {}

  obterPorId = (id: string): Observable<ViagemPorIdResponseData> =>
    this.http
      .get<ViagemPorIdResponseData>(`${this.url.api.v1}/backoffice/viagens/${id}`)
      .pipe(map((resp) => resp));

  criar = (data: Viagem): Observable<ViagemResponseData> =>
    this.http
      .post<ViagemResponseData>(`${this.url.api.v1}/backoffice/viagens`, data)
      .pipe(map((resp) => resp));

  editar = (id: string, data: Viagem): Observable<ViagemResponseData> =>
    this.http
      .put<ViagemResponseData>(`${this.url.api.v1}/backoffice/viagens/${id}`, data)
      .pipe(map((resp) => resp));

  estornar = (id: string): Observable<ViagemResponseData> =>
    this.http
      .put<ViagemResponseData>(`${this.url.api.v1}/backoffice/viagens/${id}/estornar`, {})
      .pipe(map((resp) => resp));
  toggleActivation = (id: string) =>
    this.http
      .patch(`${this.url.api.v1}/backoffice/viagens/${id}/is-active`, {})
      .pipe(map((resp) => resp));

}
export interface Viagem {
  origem: string;
  destino: string;
  dataPartida: Date;
  preco: number;
  qtdVagas: number;
  assentosDisponiveis: number[];
  assentosOcupados: number[];
  valorTotalFaturado: number;
}
export interface Passageiro {
  dataDaCompra: string;
  valorPago: number;
  id: string;
  viagemId: string;
  viagemPagamentoId: string;
  assento: number;
  nome: string;
  documento: string;
  telefone: string;
  comprador: boolean;
  viagemPagamentos: any[];

  cpfComprador: string;

  emailComprador: boolean;

  statusPagamentoDescricao: string;
}

export interface ViagemPagamento {
  id: string;
  viagemId: string;
  quantidadePassagens: number;
  valorTotal: number;
  idTransacao: string;
  pagamentoStatus: string;
  passageiros: any[];
  comprador: boolean;
}
export interface ViagemResponseData {
  success: boolean;
  data?: {
    message: string;
    success: boolean;
    viagem: ViagemResponse;
  };
}
export interface ViagemResponse extends Viagem {
  id: string;
  viagemPagamentos: any[];
  passageiros: Passageiro[];
}

export interface ViagemPorIdResponseData {
  success: boolean;
  data?: ViagemResponse
}
