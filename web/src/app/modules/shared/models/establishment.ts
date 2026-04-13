interface DeliveryVolumeType {
  name: string;
  description: string;
  id: string;
  createdAt: string;
}

interface Phone {
  userId: string;
  phone: {
    number: string;
    type: string;
    formatted: string;
  };
  id: string;
  createdAt: string;
  deleted: boolean;
}

export interface Establishments {
  user: {
    firstName: string;
    lastName: string;
    email: string;
    avatar: string;
    active: boolean;
    fullName: string;
    blocked: boolean;
    phones: Phone[];
    id: string;
    createdAt: string;
    deleted: boolean;
  };
  identification: {
    number: string;
    type: string;
    formatted: string;
  };
  addressInformation: {
    address: string;
    number: string;
    complement: string;
    neighborhood: string;
    zipCode: string;
    city: {
      id: string;
      name: string;
      state: {
        initials: string;
        name: string;
      };
    };
    state: {
      initials: string;
      name: string;
    };
    formattedZipCode: string;
  };
  name: string;
  segment: {
    name: string;
    description: string;
    establishments: any[];
    id: string;
    createdAt: string;
  };
  type: {
    name: string;
    description: string;
    establishments: any[];
    id: string;
    createdAt: string;
  };
  hasParking: boolean;
  homologated: boolean;
  establishmentNumber: 3;
  formattedEstablishmentNumber: string;
  deliveryVolumeTypes: DeliveryVolumeType[];
  id: string;
  createdAt: string;
}
