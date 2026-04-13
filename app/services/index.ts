import axios, {AxiosResponse, InternalAxiosRequestConfig} from 'axios'
import {ApiBaseUrl} from "../utils/consts";

const instance = axios.create({
    baseURL: ApiBaseUrl,
    headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
    },
})

instance.interceptors.request.use(
    async function (config: InternalAxiosRequestConfig) {
        return config
    },
    function (error: any) {
        return Promise.reject(error)
    },
)

instance.interceptors.response.use(
    async function (response: AxiosResponse) {
        return response.data
    },

    async function (error) {
        console.log(
            `[ERRO: ${error?.response?.status}] {[${error.config.method}]`,
            error.config.url,
        )
        if (error?.response?.status === 400) {
            toast?.show(
                error?.response?.data.errors[0] || '[400] Verifique os dados informados',
                {},
            )
        } else if (error?.response?.status === 401) {
        } else if (error?.response?.status === 403) {
            toast?.show('[403] Você não possui acesso à esse recurso', {
                type: 'warning',
            })
            console.warn(error?.response?.data.errors[0])
        } else if (error?.response?.status === 415) {
            toast?.show('Recurso usado de maneira incorreta!', {
                type: 'warning',
            })
        } else if (error?.response?.status === 404) {
            toast?.show('Não encontrado!', {
                type: 'warning',
            })
        } else if (error?.response?.status === 422) {
            toast?.show(
                'Algum campo foi preenchido incorretamente ou está vazio, tente novamente!',
                {
                    type: 'warning',
                },
            )
        } else if (error?.response?.status === 500) {
            toast?.show('Ops, algo deu errado! Tente novamente mais tarde.', {})
        }
        else if (error?.response?.status) {
            toast?.show('Verifique sua conexão com a internet', {})
            return
        }
        return Promise.reject(error)
    },
)

export default instance
