export interface GenericResponseError {
  errors?: string[]
  success: boolean
}

export interface GenericResponse extends GenericResponseError {
  data: {
    message: string
    error: string
    success: boolean
  }
}

export interface GenericParamsBodyPagination {
  pageIndex?: number
  pageSize?: number
}
