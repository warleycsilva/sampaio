export interface IAction<P = undefined> {
  type: string;
  payload: P;
  error?: boolean;
  meta?: any;
}

export interface IGenericFieldUpdate<T> {
  key: keyof T;
  value: string | number | boolean | undefined;
}
export interface IJsonWebToken {
  exp: string;
  iat: string;
  hash: string;
  region: string;
  uid: string;
}
export interface IModalProps {
  open: boolean;

  setOpen(open: boolean): void;
}
export interface IOption {
  label: string;
  value: string | number;
}
export interface IOptionWithColor extends IOption {
  color: string;
}
export interface IColorPalette {
  primary: string;
  secondary: string;
  alternative: string;
  white: string;
  fullWhite: string;
  black: string;
  gray: string;
  error: string;
  warn: string;
  success: string;
}
