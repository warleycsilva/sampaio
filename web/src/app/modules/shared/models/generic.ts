export interface IGenericResponseData {
  message: string;
  success: boolean;
  error?: string;
}

export interface IGenericResponse {
  data: IGenericResponseData;
  success?: boolean;
}
