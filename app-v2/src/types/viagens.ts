export interface Passageiro {
  id: string;
  viagemId: string;
  viagemPagamentoId: string;
  assento: number;
  nome: string;
  documento: string;
  telefone: string;
  comprador: boolean;
}

export interface ViagemPagamento {
  id: string;
  viagemId: string;
  quantidadePassagens: number;
  valorTotal: number;
  pagamentoStatus: 'Pending' | 'Approved' | 'Failed' | string;
  passageiros: Passageiro[];
}

export interface Viagem {
  id: string;
  origem: string;
  destino: string;
  dataPartida: string | Date;
  preco: number;
  qtdVagas: number;
  vagasVendidas: number;
  viagemPagamentos: ViagemPagamento[];
  passageiros: Passageiro[];
}

export interface GetViagensResponseData {
  itens: Viagem[];
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

export interface GetViagensResponse {
  data: GetViagensResponseData;
  success: boolean;
}

export interface PassageiroRequest {
  assento: number | undefined;
  nome: string;
  documento: string;
  telefone: string;
  comprador: boolean;
}

export interface CartaoCredito {
  numeroCartao?: string;
  nomeTitularCartao?: string;
  docTitularCartao?: string;
  mesExpiracao?: number;
  anoExpiracao?: number;
  codigoSegurancaCartao?: string;
  enderecoCepCartao?: string;
  enderecoCartao?: string;
  enderecoNumeroCartao?: string;
  enderecoRuaCartao?: string;
  enderecoBairroCartao?: string;
  enderecoCidadeCartao?: string;
  enderecoEstadoCartao?: string;
  valido?: boolean;
}

export interface ComprarPassagemRequest {
  passageiros: PassageiroRequest[];
  tipoPagamento: 'Pix' | 'Credit';
  email?: string;
  cpf?: string;
  cartaoCredito?: CartaoCredito;
}
