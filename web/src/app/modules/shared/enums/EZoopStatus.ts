enum EZoopStatus {
    active = 'Ativo',
    pending = 'Pendente',
    notSent = 'Não Enviado',
    denied = 'Negado'
  }

  const zoopStatus = [
    { description: 'Ativo', value: 'active' },
    { description: 'Pendente', value: 'pending'},
    { description: 'Não Enviado', value: 'notSent'},
    { description: 'Negado', value: 'denied'}
  ]
  export {
    zoopStatus,EZoopStatus
  }