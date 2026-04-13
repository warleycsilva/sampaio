export interface IItemOrderPut {
  id: string;
  priority: number;
}
export interface IItensOrderPut {
  items: IItemOrderPut[];
}
