export interface Account {
  holderName: string;
  bank: {
    code: string;
    name: string;
    id: string;
  };
  identification: {
    number: string;
    type: "Cpf" | "Cnpj";
    formatted: string;
  };
  bankAccountType: "Checking" | "Savings";
  branchNumber: string;
  accountNumber: string;
  id: string;
}

export interface Bank {
  code: string;
  name: string;
  id: string;
}

export interface ItemPix {
  id?: string;
  key: string;
  description: string;
}

export interface BodyParamCreateAccount {
  holderName: string;
  bankId: string;
  identification: {
    number: string;
    type: "Cpf" | "Cnpj";
  };
  bankAccountType: "Checking" | "Savings";
  branchNumber: string;
  accountNumber: string;
  lesseeId?: string;
}

export interface ResponseAccountRequest {
  message: string;
  bankDataId: string;
  success: boolean;
  error: string;
}
