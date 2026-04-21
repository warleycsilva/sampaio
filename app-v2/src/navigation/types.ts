import { Viagem } from '../types/viagens';

export type RootStackParamList = {
  Home: undefined;
  Confirmacao: { item?: Viagem } | undefined;
  EscolherAssento: undefined;
  PagamentoTela: undefined;
  Registrar: undefined;
};
