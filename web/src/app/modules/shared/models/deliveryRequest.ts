export interface DeliveryRequest {
  establishment?: {
    user?: {
      firstName?: string;
      lastName?: string;
      email?: string;
      avatar?: string;
      active?: false;
      fullName?: string;
      blocked?: false;
      phones?: any[];
      id?: string;
      createdAt?: string;
      deleted?: false;
    };
    identification?: {
      number?: string;
      type?: string;
      formatted?: string;
    };
    addressInformation?: {
      address?: string;
      number?: string;
      complement?: string;
      neighborhood?: string;
      zipCode?: string;
      city?: {
        id?: string;
        name?: string;
        state?: {
          initials?: string;
          name?: string;
        };
      };
      state?: {
        initials?: string;
        name?: string;
      };
      formattedZipCode?: string;
    };
    name?: string;
    hasParking?: true;
    homologated?: true;
    establishmentNumber?: number;
    formattedEstablishmentNumber?: string;
    deliveryVolumeTypes?: any[];
    id?: string;
    createdAt?: string;
  };
  lessee?: {
    id?: string;
    user?: {
      fullName?: string;
      avatar?: string;
    };
    lesseeGeolocalizations?: {
      lesseeId?: string;
      lesseeName?: string;
      latitude?: string;
      longitude?: string;
    };
  };
  originAddressLatitude?: string;
  originAddressLongitude?: string;
  lesseeVehicle?: {
    color?: string;
    colorDescription?: string;
    active?: false;
  };
  status?: string;
  statusDescription?: string;
  originContactName?: string;
  originContactPhone?: string;
  originAddress?: {
    address?: string;
    number?: string;
    complement?: string;
    neighborhood?: string;
    zipCode?: string;
    city?: {
      id?: string;
      name?: string;
      state?: {
        initials?: string;
        name?: string;
      };
    };
    state?: {
      initials?: string;
      name?: string;
    };
    formattedZipCode?: string;
  };
  returnAddress?: {
    address?: string;
    number?: string;
    complement?: string;
    neighborhood?: string;
    zipCode?: string;
    city?: {
      id?: string;
      name?: string;
      state?: {
        initials?: string;
        name?: string;
      };
    };
    state?: {
      initials?: string;
      name?: string;
    };
    formattedZipCode?: string;
  };
  lesseeCost?: number;
  totalDistance?: number;
  totalDuration?: number;
  totalDurationValue?: string;
  lesseeDuration?: number;
  lesseeDurationValue?: string;
  totalPrice?: number;
  paidByEstablishment?: false;
  paidToLessee?: false;
  deliveryNumber?: number;
  formattedDeliveryNumber?: string;
  isScheduled?: false;
  deliveryRequestStatuses?: [
    {
      status?: string;
      statusDescription?: string;
      id?: string;
      createdAt?: string;
    }
  ];
  deliveryRequestItems?: [
    {
      destinationAddressLatitude?: string;
      destinationAddressLongitude?: string;
      photo?: string;
      description?: string;
      height?: number;
      width?: number;
      depth?: number;
      totalWeight?: number;
      productValue?: number;
      distance?: number;
      estimatedDuration?: number;
      EstimatedDurationFormatted?: number;
      destinationContactName?: string;
      destinationContactPhone?: string;
      destinationAddress?: {
        address?: string;
        number?: string;
        complement?: string;
        neighborhood?: string;
        zipCode?: string;
        city?: {
          id?: string;
          name?: string;
          state?: {
            initials?: string;
            name?: string;
          };
        };
        formattedZipCode?: string;
      };
      volumeType?: {
        name?: string;
        description?: string;
        id?: string;
        createdAt?: string;
      };
      isDelivered?: false;
      priority?: number;
      lesseeRatingByClient?: number;
      id?: string;
      isPerishable?: boolean;
      devolution?: "Devolution" | "Discard";
    }
  ];
  lesseeRatingByEstablishment?: number;
  establishmentRatingAdditionalInfo?: string;
  driverRating?: number;
  driverRatingComment?: string;
  discountCoupon?: {
    discountValue?: string | number;
    totalPriceWithDiscount?: string | number;
  };
  assignedLessee?: {
    id?: string;
    user?: {
      fullName?: string;
      avatar?: string;
    };
    lesseeGeolocalizations?: {
      lesseeId?: string;
      lesseeName?: string;
      latitude?: string;
      longitude?: string;
    };
  };
  schedulingTime?: string;
  id?: string;
  createdAt?: string;
  vehicleType?: string;
  vehicleTypeValue?: string;
}
