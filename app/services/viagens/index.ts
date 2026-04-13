import httpClient from "../index";
import {ComprarPassagemRequest, GetViagensResponse, Viagem, ViagemPagamento} from "../../types/Viagens";
import {format} from "date-fns";

export const getViagens = async (searchOrigem: string, searchDestino: string, searchData: string | undefined): Promise<GetViagensResponse> => {
    try {
        if (!!searchData) {
            const dia = searchData.substring(0,2)
            const mes = searchData.substring(3,5)
            const ano = searchData.substring(6,10)
            searchData = new Date(mes+'/'+dia+'/'+ano).toISOString()
        }
    } finally {
    }
    const params: Record<string, string> = {
        'Origem': searchOrigem,
        'Destino': searchDestino,
    };
    if(!!searchData) {
        params['DataPartida'] = searchData
    }
    return await httpClient.get('/v1/viagens', {
        params
    });
}
export const getViagensById = async (id: string): Promise<Viagem> => {
    var response =  await httpClient.get(`/v1/viagens/${id}`);
    return response.data
}
export const comprarViagem = async (id: string, request: ComprarPassagemRequest): Promise<any> => {
    return await httpClient.post(`/v1/viagens/${id}/comprar`, request);
}

export const obterViagemPorEmail = async (email: string): Promise<Viagem> => {
    return await httpClient.get(`/v1/viagens/by-email/${email}`);
}

export const obterUltimaViagemPorEmail = async (email: string): Promise<ViagemPagamento> => {
    var result = await httpClient.get(`/v1/viagens/by-email/${email}/last`);
    return result.data;
}
