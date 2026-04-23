import axios, { AxiosResponse, InternalAxiosRequestConfig } from 'axios';

const API_BASE_URL = 'https://sampaioturismo.azurewebsites.net/api';

const client = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    Accept: 'application/json',
    'Content-Type': 'application/json',
  },
});

client.interceptors.request.use(
  (config: InternalAxiosRequestConfig) => config,
  (error) => Promise.reject(error),
);

client.interceptors.response.use(
  (response: AxiosResponse) => response.data,
  (error) => {
    const status: number | undefined = error?.response?.status;
    const showToast = (msg: string, type?: string) =>
      (global as any).toast?.show(msg, type ? { type } : undefined);

    if (status === 400)       showToast(error?.response?.data?.errors?.[0] ?? '[400] Verifique os dados');
    else if (status === 403)  showToast('[403] Sem permissão de acesso', 'warning');
    else if (status === 404)  showToast('Não encontrado', 'warning');
    else if (status === 415)  showToast('Recurso usado incorretamente', 'warning');
    else if (status === 422)  showToast('Campo preenchido incorretamente', 'warning');
    else if (status === 500)  showToast('Erro interno. Tente novamente mais tarde.');
    else if (status)          showToast('Verifique sua conexão com a internet');

    return Promise.reject(error);
  },
);

export default client;
