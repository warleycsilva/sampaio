export interface CustomersResponse {
  requests: number;
  destinationContactName: string;
  destinationContactPhone: string;
  destinationAddress: {
    address?: string;
    complement?: string;
    neighborhood?: string;
    number?: string;
    zipCode?: string;
  };
}
