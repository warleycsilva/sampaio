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

interface IBlockUserBodyDTO {
  blockReason: string;
}

interface IDataQrCodePayment {
  deliveryRequestId: string;
  paymentType: "Credit" | "Debit" | "Money" | "Transfer" | "Pix";
}

interface IPixQrCodePaymentResponse {
  pixQrCode: string;
  error: string;
  message: string;
  success: boolean;
}

interface IListAll<T> {
  itens: T[];
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

interface IGetPartnersResponse {
  data: IListAll<{
    name: string;
    email: string;
    avatar: string;
    active: boolean;
    phones: {
      phone: {
        number: string;
        type: string;
        formatted: string;
      };
      partnerId: string;
      id: string;
      createdAt: Date;
      deleted: boolean;
    }[];
    addressInformation: {
      address: string;
      number: string;
      complement: string;
      neighborhood: string;
      zipCode: string;
      city: {
        id: string;
        name: string;
        state: {
          initials: string;
          name: string;
        };
      };
      cityId: string;
      formattedZipCode: string;
    };
    id: string;
    createdAt: Date;
    deleted: boolean;
  }>;
}

interface IGetPartnersParams {
  name?: string;
  email?: string;
  active?: boolean;
  pageIndex?: number;
  pageSize?: number;
  sortField?: string;
  sortType?: string;
}

@Injectable({
  providedIn: "root",
})
export class BackofficeService {
  private url = environment.urls;

  constructor(private http: HttpClient, private appService: AppService) {}

  getRenterById = (id: any): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/renters/${id}`)
      .pipe(map((resp) => resp));

  deleteRenterById = (id: any): Observable<any> =>
    this.http
      .delete<any>(`${this.url.api.v1}/backoffice/renters/${id}`)
      .pipe(map((resp) => resp));

  getRenterDocumentationById = (id: any): Observable<any> =>
    this.http
      .get<any>(
        `${this.url.api.v1}/backoffice/renters/${id}/documentation-download`
      )
      .pipe(map((resp) => resp));

  updateFeeConfig = (data: any, id: any): Observable<any> =>
    this.http
      .put<any>(`${this.url.api.v1}/backoffice/sampaio-fees/${id}`, data)
      .pipe(map((resp) => resp));

  createFeeConfig = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/sampaio-fees`, data)
      .pipe(map((resp) => resp));

  createBackofficeUser = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/backoffice-users`, data)
      .pipe(map((resp) => resp));

  updateHomologatedStatus = (id: any): Observable<any> =>
    this.http
      .put<any>(
        `${this.url.api.v1}/backoffice/renters/${id}/update-homologated-status/`,
        null
      )
      .pipe(map((resp) => resp));

  getPartnerById = (id: any): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/partners/${id}`)
      .pipe(map((resp) => resp));

  getPartners = (data?: IGetPartnersParams): Observable<IGetPartnersResponse> =>
    this.http
      .get<IGetPartnersResponse>(`${this.url.api.v1}/backoffice/partners`, {
        params: { ...data },
      })
      .pipe(map((resp) => resp));

  createPartner = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/partners`, data)
      .pipe(map((resp) => resp));

  updatePartner = (data: any, id: any): Observable<any> =>
    this.http
      .put<any>(`${this.url.api.v1}/backoffice/partners/${id}`, data)
      .pipe(map((resp) => resp));

  createBenefit = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/benefits`, data)
      .pipe(map((resp) => resp));

  updateBenefit = (data: any, id: any): Observable<any> =>
    this.http
      .put<any>(`${this.url.api.v1}/backoffice/benefits/${id}`, data)
      .pipe(map((resp) => resp));

  toggleBenefitStatus = (id: string): Observable<any> =>
    this.http
      .patch<any>(
        `${this.url.api.v1}/backoffice/benefits/change-status/${id}`,
        null
      )
      .pipe(map((resp) => resp));

  deleteBenefit = (id: any): Observable<any> =>
    this.http
      .delete<any>(`${this.url.api.v1}/backoffice/benefits/${id}`)
      .pipe(map((resp) => resp));

  acceptTermByRenterId = (data: any): Observable<any> =>
    this.http
      .put<any>(
        `${this.url.api.v1}/backoffice/renters/accept-payment-term/`,
        data
      )
      .pipe(map((resp) => resp));

  getVolumeTypes = (): Observable<any> =>
    this.http
      .get(`${this.url.api.v1}/backoffice/delivery-volume-types`)
      .pipe(map((resp) => resp));

  createVolumeType = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/delivery-volume-types`, data)
      .pipe(map((resp) => resp));

  createNewActiveNeighborhood = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/active-neighborhoods`, data)
      .pipe(map((resp) => resp));

  deleteActiveNeighborhood = (id: string): Observable<any> =>
    this.http
      .delete<any>(`${this.url.api.v1}/backoffice/active-neighborhoods/${id}`)
      .pipe(map((resp) => resp));

  updateVolumeType = (data: any, id: any): Observable<any> =>
    this.http
      .put<any>(
        `${this.url.api.v1}/backoffice/delivery-volume-types/${id}`,
        data
      )
      .pipe(map((resp) => resp));

  deleteVolumeType = (id: any): Observable<any> =>
    this.http
      .delete<any>(`${this.url.api.v1}/backoffice/delivery-volume-types/${id}`)
      .pipe(map((resp) => resp));

  getSegments = (): Observable<any> =>
    this.http
      .get(`${this.url.api.v1}/backoffice/establishment-segments`)
      .pipe(map((resp) => resp));

  createEstablishmentSegment = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/establishment-segments`, data)
      .pipe(map((resp) => resp));

  updateEstablishmentSegment = (data: any, id: any): Observable<any> =>
    this.http
      .put<any>(
        `${this.url.api.v1}/backoffice/establishment-segments/${id}`,
        data
      )
      .pipe(map((resp) => resp));

  deleteEstablishmentSegment = (id: any): Observable<any> =>
    this.http
      .delete<any>(`${this.url.api.v1}/backoffice/establishment-segments/${id}`)
      .pipe(map((resp) => resp));

  getTypes = (): Observable<any> =>
    this.http
      .get(`${this.url.api.v1}/backoffice/establishment-types`)
      .pipe(map((resp) => resp));

  createEstablishmentType = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/establishment-types`, data)
      .pipe(map((resp) => resp));

  updateEstablishmentType = (data: any, id: any): Observable<any> =>
    this.http
      .put<any>(`${this.url.api.v1}/backoffice/establishment-types/${id}`, data)
      .pipe(map((resp) => resp));

  deleteEstablishmentType = (id: any): Observable<any> =>
    this.http
      .delete<any>(`${this.url.api.v1}/backoffice/establishment-types/${id}`)
      .pipe(map((resp) => resp));

  getEstablishmentById = (id: any): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/establishments/${id}`)
      .pipe(map((resp) => resp));

  toggleEstablishmentHomologatedStatus = (id: string): Observable<any> =>
    this.http
      .patch<any>(
        `${this.url.api.v1}/backoffice/establishments/toggle-homologated-status/${id}`,
        null
      )
      .pipe(map((resp) => resp));

  deleteEstablishment = (id: string): Observable<any> =>
    this.http
      .delete<any>(`${this.url.api.v1}/backoffice/establishments/delete/${id}`)
      .pipe(map((resp) => resp));

  createEstablishmentByBackoffice = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/establishments/`, data)
      .pipe(map((resp) => resp));

  createBenefitCategory = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/benefit-categories`, data)
      .pipe(map((resp) => resp));

  updateBenefitCategory = (data: any, id: any): Observable<any> =>
    this.http
      .put<any>(`${this.url.api.v1}/backoffice/benefit-categories/${id}`, data)
      .pipe(map((resp) => resp));

  deleteBenefitCategory = (id: any): Observable<any> =>
    this.http
      .delete<any>(`${this.url.api.v1}/backoffice/benefit-categories/${id}`)
      .pipe(map((resp) => resp));

  getBenefitCategoryById = (id: any): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/benefit-categories/${id}`)
      .pipe(map((resp) => resp));

  getBenefitCategories = (): Observable<any> => {
    return this.http
      .get<any>(
        `${this.url.api.v1}/backoffice/benefit-categories?PageIndex=1&PageSize=9999`
      )
      .pipe(map((resp) => resp));
  };

  importBenefits = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/benefits/import`, data)
      .pipe(map((resp) => resp));

  activateUserById = (data: any): Observable<any> =>
    this.http
      .post<any>(
        `${this.url.api.v1}/backoffice/backoffice-users/activate-account`,
        data
      )
      .pipe(map((resp) => resp));

  updateLessee = (data: any, id: string): Observable<any> =>
    this.http
      .put<any>(
        `${this.url.api.v1}/backoffice/backoffice-users/update-lessee/${id}`,
        data
      )
      .pipe(map((resp) => resp));

  getDeliveryRequestById = (id: any): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/delivery-requests/${id}`)
      .pipe(map((resp) => resp));

  setDeliveryRequestPaidByEstablishment = (id: string): Observable<any> =>
    this.http
      .patch<any>(
        `${this.url.api.v1}/backoffice/delivery-requests/${id}/set-paid-by-establishment`,
        null
      )
      .pipe(map((resp) => resp));

  setDeliveryRequestPaidToLessee = (id: string): Observable<any> =>
    this.http
      .patch<any>(
        `${this.url.api.v1}/backoffice/delivery-requests/${id}/set-paid-to-lessee`,
        null
      )
      .pipe(map((resp) => resp));

  getDeliveryRequestIncomesExtract = (filter?: any): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/delivery-requests/incomes`, {
        params: filter,
      })
      .pipe(map((resp) => resp));

  getDiscountsCoupons = (): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/discountCoupons`)
      .pipe(map((resp) => resp));

  getDiscountsCouponsById = (id: string): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/discountCoupons/${id}`)
      .pipe(map((resp) => resp));

  createDiscountCoupom = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/discountCoupons`, data)
      .pipe(map((resp) => resp));

  editDiscountCoupom = (id: string, data: any): Observable<any> =>
    this.http
      .put<any>(`${this.url.api.v1}/backoffice/discountCoupons/${id}`, data)
      .pipe(map((resp) => resp));

  deleteDiscountsCouponsById = (id: string): Observable<any> =>
    this.http
      .delete<any>(`${this.url.api.v1}/backoffice/discountCoupons/${id}`)
      .pipe(map((resp) => resp));

  changeStatusDiscountCounponById = (id: string, data: null): Observable<any> =>
    this.http
      .patch<any>(`${this.url.api.v1}/backoffice/discountCoupons/${id}`, data)
      .pipe(map((resp) => resp));

  getAllEstablishments = (): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/establishments/list-all`)
      .pipe(map((resp) => resp));

  getClientIntegrationById = (id: any): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/client-integrations/${id}`)
      .pipe(map((resp) => resp));

  createClientIntegration = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/client-integrations`, data)
      .pipe(map((resp) => resp));

  updateClientIntegration = (data: any, id: any): Observable<any> =>
    this.http
      .put<any>(`${this.url.api.v1}/backoffice/client-integrations/${id}`, data)
      .pipe(map((resp) => resp));

  getDeliveriesByIdLessee = (idLessee: any): Observable<any> =>
    this.http
      .get<any>(
        `${this.url.api.v1}/backoffice/delivery-requests/lessee/${idLessee}`
      )
      .pipe(map((resp) => resp));

  sendListDeliveriesRequestsPaidByEstablishment = (
    listDeliveries: string[]
  ): Observable<any> =>
    this.http
      .patch<any>(
        `${this.url.api.v1}/backoffice/delivery-requests/set-paid-by-establishment`,
        { deliveriesRequestId: listDeliveries }
      )
      .pipe(map((resp) => resp));

  sendListDeliveriesRequestsPaidToLessee = (
    listDeliveries: string[]
  ): Observable<any> =>
    this.http
      .patch<any>(
        `${this.url.api.v1}/backoffice/delivery-requests/set-paid-to-lessee`,
        { deliveriesRequestId: listDeliveries }
      )
      .pipe(map((resp) => resp));

  getDataEstablishmentById = (id: string): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/establishments/${id}`)
      .pipe(map((resp) => resp));

  updateDataEstablishment = (id: string, data): Observable<any> =>
    this.http
      .put<any>(`${this.url.api.v1}/backoffice/establishments/${id}`, data)
      .pipe(map((resp) => resp));

  blockLessee = (id: string, data: IBlockUserBodyDTO): Observable<any> =>
    this.http
      .patch<any>(`${this.url.api.v1}/backoffice/lessees/block/${id}`, {
        blockReason: data.blockReason,
      })
      .pipe(map((resp) => resp));

  unblockLessee = (id: string): Observable<any> =>
    this.http
      .patch<any>(`${this.url.api.v1}/backoffice/lessees/unblock/${id}`, {})
      .pipe(map((resp) => resp));

  establhismentDeliveriesHistoricbyId = (id: string): Observable<any> =>
    this.http
      .get<any>(
        `${this.url.api.v1}/backoffice/establishments/deliveries-request/${id}`
      )
      .pipe(map((resp) => resp));

  blockBackofficeUser = (
    id: string,
    data: IBlockUserBodyDTO
  ): Observable<any> =>
    this.http
      .patch<any>(
        `${this.url.api.v1}/backoffice/backoffice-users/block/${id}`,
        {
          blockReason: data.blockReason,
        }
      )
      .pipe(map((resp) => resp));

  unblockBackofficeUser = (id: string): Observable<any> =>
    this.http
      .patch<any>(
        `${this.url.api.v1}/backoffice/backoffice-users/unblock/${id}`,
        {}
      )
      .pipe(map((resp) => resp));

  blockEstablishmentsUser = (
    id: string,
    data: IBlockUserBodyDTO
  ): Observable<any> =>
    this.http
      .patch<any>(`${this.url.api.v1}/backoffice/establishments/block/${id}`, {
        blockReason: data.blockReason,
      })
      .pipe(map((resp) => resp));

  unblockEstablishmentsUser = (id: string): Observable<any> =>
    this.http
      .patch<any>(
        `${this.url.api.v1}/backoffice/establishments/unblock/${id}`,
        {}
      )
      .pipe(map((resp) => resp));

  editTransferPolicy = (data: { content: string }): Observable<any> =>
    this.http
      .put<any>(`${this.url.api.v1}/backoffice/transfer-policy`, data)
      .pipe(map((resp) => resp));

  getTransferPolicy = (): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/transfer-policy`)
      .pipe(map((resp) => resp));

  assignLesseeToDelivery = (data: {
    deliveryRequestId: string;
    lesseeId: string;
  }): Observable<any> =>
    this.http
      .post<any>(
        `${this.url.api.v1}/backoffice/delivery-requests/assign-lessee`,
        data
      )
      .pipe(map((resp) => resp));

  getAllLessees = (): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/lessees`)
      .pipe(map((resp) => resp));

  getAllLesseeVehicles = (
    id: string,
    lesseeVehicleFilter?: any
  ): Observable<any> =>
    this.http
      .get<any>(
        `${this.url.api.v1}/backoffice/lessee-vehicles/lessee-id/${id}`,
        {
          params: lesseeVehicleFilter,
        }
      )
      .pipe(map((resp) => resp));

  activateLesseeVehicle = (id: string): Observable<any> =>
    this.http
      .patch<any>(
        `${this.url.api.v1}/backoffice/lessee-vehicles/change-status/${id}`,
        {}
      )
      .pipe(map((resp) => resp));

  changeStatusDocumentLesseeVehicle = (
    id: string,
    data?: any
  ): Observable<any> =>
    this.http
      .patch<any>(
        `${this.url.api.v1}/backoffice/lessee-vehicles/change-status-document/${id}`,
        data
      )
      .pipe(map((resp) => resp));

  createNewLesseeVehicle = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/lessee-vehicles`, data)
      .pipe(map((resp) => resp));

  getLesseeVehicleById = (id: string): Observable<any> =>
    this.http.get<any>(`${this.url.api.v1}/backoffice/lessee-vehicles/${id}`);

  editLesseeVehicle = (id: string, data): Observable<any> =>
    this.http
      .put<any>(`${this.url.api.v1}/backoffice/lessee-vehicles/${id}`, data)
      .pipe(map((resp) => resp));

  deleteLesseeVehicleById = (id: string): Observable<any> =>
    this.http
      .delete<any>(`${this.url.api2}/v1/backoffice/lessee-vehicles/${id}`)
      .pipe(map((resp) => resp));

  getFileExcelLessee = (): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/lessees/export-to-excel`)
      .pipe(map((resp) => resp));

  getFileExcelPartnerBenefits = (filter?: any): Observable<any> => {
    return this.http
      .get<any>(`${this.url.api.v1}/backoffice/benefits/export-to-excel`, {
        params: this.appService.formatParams(filter),
      })
      .pipe(map((resp) => resp));
  };

  getFileExcelPartnerBenefitsPurchases = (filter?: any): Observable<any> => {
    return this.http
      .get<any>(
        `${this.url.api.v1}/backoffice/benefits/purchases/export-to-excel`,
        {
          params: this.appService.formatParams(filter),
        }
      )
      .pipe(map((resp) => resp));
  };

  getFileExcelEstablishments = (): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/establishments/export-to-excel`)
      .pipe(map((resp) => resp));

  getFileExcelDeliveries = (): Observable<any> =>
    this.http
      .get<any>(
        `${this.url.api.v1}/backoffice/delivery-requests/export-to-excel`
      )
      .pipe(map((resp) => resp));

  getListCustomers = (
    idEstablishment: string
  ): Observable<CustomersResponse[]> =>
    this.http
      .get<any>(
        `${this.url.api.v1}/backoffice/establishments/customers/${idEstablishment}`
      )
      .pipe(map((resp) => resp.data));

  editOrderItens = (id: string, data: IItensOrderPut): Observable<any> =>
    this.http
      .put<any>(
        `${this.url.api.v1}/backoffice/delivery-requests/${id}/items/change-priority`,
        data
      )
      .pipe(map((resp) => resp));

  deleteDeliveryItem = (id: string): Observable<any> =>
    this.http
      .delete<any>(`${this.url.api.v1}/backoffice/delivery-requests/item/${id}`)
      .pipe(map((resp) => resp));

  createDelivery = (data): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/delivery-requests`, data)
      .pipe(map((resp) => resp));

  createPartnerUser = (data): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/partners-users`, data)
      .pipe(map((resp) => resp));

  getPartnersUsersByPartnerId = (id: string, params: any): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/partners-users/partner/${id}`, {
        params: {
          params,
        },
      })
      .pipe(map((resp) => resp.data));

  getPartnerUserById = (id: any): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/partners-users/${id}`)
      .pipe(map((resp) => resp));

  blockPartnerUser = (id: string, data: IBlockUserBodyDTO): Observable<any> =>
    this.http
      .patch<any>(
        `${this.url.api.v1}/backoffice/backoffice-users/block/${id}`,
        {
          blockReason: data.blockReason,
        }
      )
      .pipe(map((resp) => resp));

  unblockPartnerUser = (id: string): Observable<any> =>
    this.http
      .patch<any>(
        `${this.url.api.v1}/backoffice/backoffice-users/unblock/${id}`,
        {}
      )
      .pipe(map((resp) => resp));

  inactivePartnersUsersById = (id: string): Observable<any> =>
    this.http
      .patch<any>(
        `${this.url.api.v1}/backoffice/partners-users/inactive/${id}`,
        null
      )
      .pipe(map((resp) => resp.data));

  disassociateUserByPartnerId = (id: any): Observable<any> =>
    this.http
      .patch<any>(
        `${this.url.api.v1}/backoffice/partners-users/disassociate-user/${id}`,
        null
      )
      .pipe(map((resp) => resp));

  updatePartnerUser = (id: any, data: any): Observable<any> =>
    this.http
      .post<any>(
        `${this.url.api.v1}/backoffice/backoffice-users/update-user/${id}`,
        data
      )
      .pipe(map((resp) => resp.data));

  editDelivery = (id: string, data): Observable<any> =>
    this.http
      .put<any>(`${this.url.api.v1}/backoffice/delivery-requests/${id}`, data)
      .pipe(map((resp) => resp));

  addItemDelivery = (data): Observable<any> =>
    this.http
      .post<any>(
        `${this.url.api.v1}/backoffice/delivery-requests/add-item`,
        data
      )
      .pipe(map((resp) => resp));

  editItemDelivery = (id: string, data): Observable<any> =>
    this.http
      .put<any>(
        `${this.url.api.v1}/backoffice/delivery-requests/item/${id}`,
        data
      )
      .pipe(map((resp) => resp));

  getLesseesGeolocation = (
    onlyRecents?: boolean
  ): Observable<ILesseeGeolocation[]> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/lessees/geolocalizations`, {
        params: {
          onlyRecents,
        },
      })
      .pipe(map((resp) => resp.data));

  getDeliveriesGeolocation = (filter?: any): Observable<any> =>
    this.http
      .get<any>(
        `${this.url.api.v1}/backoffice/delivery-requests/geolocalizations`,
        {
          params: filter,
        }
      )
      .pipe(map((resp) => resp.data));

  getLesseeGeolocationById = (id: string): Observable<ILesseeGeolocation> =>
    this.http.get<any>(
      `${this.url.api.v1}/backoffice/lessees/get-geolocalization/${id}`
    );

  sendLesseeToZoop = (id: string): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/lessees/${id}/send-to-zoop`)
      .pipe(map((resp) => resp.data));

  updateZoopStatusHomologated = (id: string, data): Observable<any> =>
    this.http
      .put<any>(
        `${this.url.api.v1}/backoffice/lessees/${id}/update-homologated-status`,
        data
      )
      .pipe(map((resp) => resp));

  createPixPayment = (
    data: IDataQrCodePayment
  ): Observable<IPixQrCodePaymentResponse> =>
    this.http
      .post<any>(
        `${this.url.api.v1}/backoffice/delivery-requests/payment/pix`,
        data
      )
      .pipe(map((resp) => resp.data));

  verifyPaymentStatus = (id: string): Observable<any> =>
    this.http
      .get(`${this.url.api.v1}/backoffice/delivery-requests/payment/${id}`)
      .pipe(map((resp) => resp));

  getRoutesById = (id: string): Observable<IResponseRoutes> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/delivery-requests/${id}/route`)
      .pipe(map((resp) => resp.data));

  createLessee = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/lessees/create`, data)
      .pipe(map((resp) => resp));

  // BANNERS
  createBanner = (data: any): Observable<any> =>
    this.http
      .post<any>(`${this.url.api.v1}/backoffice/banners`, data)
      .pipe(map((resp) => resp));

  getBannerById = (id: string): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/banners/${id}`)
      .pipe(map((resp) => resp));

  getAllBanners = (): Observable<any> =>
    this.http
      .get<any>(`${this.url.api.v1}/backoffice/banners`)
      .pipe(map((resp) => resp));

  editBanner = (id: string, data: any): Observable<any> =>
    this.http
      .put<any>(`${this.url.api.v1}/backoffice/banners/${id}`, data)
      .pipe(map((resp) => resp));

  deleteBanner = (id: string): Observable<any> =>
    this.http
      .delete<any>(`${this.url.api.v1}/backoffice/banners/${id}`)
      .pipe(map((resp) => resp));

  getUserDevices = (id: string): Observable<any> =>
    this.http
      .get<{ data: UserDevice[] }>(
        `${this.url.api2}/v1/backoffice/user-devices/user/${id}`
      )
      .pipe(map((resp) => resp.data));

  cancelBenefitPurchase = (id: string): Observable<IGenericResponseData> =>
    this.http
      .patch<IGenericResponse>(
        `${this.url.api2}/v1/backoffice/benefits/purchase/${id}/cancel`,
        null
      )
      .pipe(map((resp) => resp.data));
}
