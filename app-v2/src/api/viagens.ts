import client from './client';
import { ComprarPassagemRequest, GetViagensResponse, Viagem, ViagemPagamento } from '../types/viagens';

export const getViagens = (
  origem: string,
  destino: string,
  data?: string,
): Promise<GetViagensResponse> => {
  let dataPartida: string | undefined;
  if (data && data.length === 10) {
    const [dia, mes, ano] = data.split('/');
    dataPartida = new Date(`${mes}/${dia}/${ano}`).toISOString();
  }
  const params: Record<string, string> = { Origem: origem, Destino: destino };
  if (dataPartida) params['DataPartida'] = dataPartida;
  return client.get('/v1/viagens', { params });
};

export const getViagemById = (id: string): Promise<Viagem> =>
  client.get(`/v1/viagens/${id}`).then((r: any) => r.data ?? r);

export const comprarViagem = (id: string, request: ComprarPassagemRequest): Promise<any> =>
  client.post(`/v1/viagens/${id}/comprar`, request);

export const getViagemPorEmail = (email: string): Promise<Viagem> =>
  client.get(`/v1/viagens/by-email/${email}`);

export const getUltimaViagemPorEmail = (email: string): Promise<ViagemPagamento> =>
  client.get(`/v1/viagens/by-email/${email}/last`).then((r: any) => r.data ?? r);
