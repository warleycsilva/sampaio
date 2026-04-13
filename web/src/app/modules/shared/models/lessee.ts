export interface AddressInformation {
  address: string;
  number: string;
  complement: string;
  neighborhood: string;
  zipCode: string;
  city: City;
  state: State;
  formattedZipCode: string;
}

export interface City {
  id: string;
  name: string;
  state: State;
}

export interface State {
  initials: string;
  name: string;
}

export interface Identification {
  number: string;
  type: string;
  formatted: string;
}

export interface User {
  firstName: string;
  lastName: string;
  email: string;
  avatar: string;
  active: boolean;
  fullName: string;
  blocked: boolean;
  phones: Phone[];
  id: string;
  createdAt: Date;
  deleted: boolean;
}

export interface Phone {
  userId: string;
  phone: Identification;
  id: string;
  createdAt: Date;
  deleted: boolean;
}

export default interface Lessee {
  user: User;
  addressInformation: AddressInformation;
  identification: Identification;
  birthdate: Date;
  cnhNumber: string;
  isTaxiDriver: boolean;
  ratrExpirationDate: Date;
  ratr: string;
  inss: string;
  termsAreAccepted: boolean;
  driverLicensePhoto: string;
  homeProofPhoto: string;
  activityPhoto: string;
  lesseeNumber: number;
  formattedLesseeNumber: string;
  lesseeGeolocalizations: any[];
  id: string;
  createdAt: Date;
  deleted: boolean;
}
