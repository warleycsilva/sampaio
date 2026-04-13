enum EVehicleStatus {
    AguardandoAnalise = 'Aguardando Análise',
    Aprovado = 'Aprovado',
    Reprovado = 'Reprovado',
  }
  const todosStatus= [{
    value: 'AguardandoAnalise',
    description: 'Aguardando Análise'
  }, {
    value: 'Aprovado',
    description: 'Aprovado'
  }, {
    value: 'Reprovado',
    description: 'Reprovado'
  }
  ];

  const respostaStatus= [
   {
    value: 'Aprovado',
    description: 'Aprovado'
  }, {
    value: 'Reprovado',
    description: 'Reprovado'
  }
  ];


  
  export {
    todosStatus, respostaStatus,EVehicleStatus
  }
  
  