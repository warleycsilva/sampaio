enum EDeliveryRequestStatusFilterToMap {
  Available = 'Disponível',
  InProgress = 'Em andamento',
  Complete = 'Completa',
  Cancelled = 'Cancelada',

}

const deliveryRequestStatusMap = [{
  value: 'Available',
  description: 'Disponível'
}, {
  value: 'InProgress',
  description: 'Em andamento'
}, {
  value: 'Complete',
  description: 'Completa'
},
  {
    value: 'Cancelled',
    description: 'Cancelada'
  }
];


export {
  deliveryRequestStatusMap, EDeliveryRequestStatusFilterToMap
}
