import { Viagem } from '../types/viagens';

export type RootTabParamList = {
  Home: undefined;
  ListaViagens: undefined;
  ChoosePlace: undefined;
  Payment: undefined;
  Confirmacao: { item?: Viagem } | undefined;
  Registrar: undefined;
};

export type RootStackParamList = {
  HomeBase: undefined;
};
