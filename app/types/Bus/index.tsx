export interface LugaresOnibus {
  assentos: Assento[];
}
export interface Assento {
  numero: number;
  selecionado: boolean;
  ocupado: boolean;
}
