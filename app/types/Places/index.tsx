export interface Place {
  id: string;
  name: string;
}
export interface PlaceAndTime extends Place {
  time: string;
}
export interface Travel {
  origem: string
  destino: string
  partida: string
  preco: number
}
export interface Estado {
  sigla: string;
  nome: string;
  cidades: string[];
}

export interface SearchForm {
  origin: string;
  destination: string;
  initialDate: Date;
  finalDate?: Date | undefined;
}
