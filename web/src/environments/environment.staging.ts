// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: true,
  colors: {
    primary: '#2D3591',
    secondary: '#f44336',
    alternative: '#FFF200',
    black: '#1f1f1f',
    gray: '#5f5f5f',
    error: '#f44336',
    success: '#66bb6a',
    warn: '#ffb74d',
    white: '#e3f2fd',
    fullWhite: '#fff',
  },
  googleMapsApi: "AIzaSyAaE6yTN2lFSGtYG7-__K2BT97rGPexphA",
  localStore: {
    token: "st.token",
    lang: "st.lang",
    user: "st.user",
  },
  urls: {
    web: "https://sampaio-hml.azurewebsites.net/",
    api2: "https://sampaio-hml-api.azurewebsites.net/api",
    api: {
      v1: "https://sampaio-hml-api.azurewebsites.net/api/v1",
    },
    hangfire: "https://sampaio-hml-api.azurewebsites.net/jobs",
  },
  defaultAvatar: "assets/images/avatar.png",
  userTypes: [
    {
      value: "Renter",
      name: "Dono do Veículo",
    },
    {
      value: "Lessee",
      name: "Motorista",
    },
  ],
  benefitTypes: [
    {
      value: "Product",
      name: "Produto",
    },
    {
      value: "Service",
      name: "Serviço",
    },
  ],
  discountTypes: [
    {
      value: "ByPercentage",
      name: "Por porcentagem",
    },
    {
      value: "ByFinalPrice",
      name: "Por preço final",
    },
  ],
  IdentificationTypes: [
    { value: "Cpf", name: "CPF" },
    { value: "Cnpj", name: "CNPJ" },
  ],
  PaymentReceiveOptions: [
    {
      value: "D2",
      name: "D+2 (Plano antecipado)",
    },
    {
      value: "D30",
      name: "D+30 (Plano econômico)",
    },
  ],
  fuelTypes: [
    {
      value: "Gasoline",
      name: "Gasolina",
    },
    {
      value: "GasolineAndCng",
      name: "Gasolina e GNV",
    },
    {
      value: "Flex",
      name: "Flex",
    },
    {
      value: "FlexAndCng",
      name: "Flex e GNV",
    },
    {
      value: "Alcohol",
      name: "Alcool",
    },
    {
      value: "AlcoholAndCng",
      name: "Alcool e GNV",
    },
  ],
  contractTypes: [
    { value: "Recurring", name: "Recorrente" },
    { value: "Detached", name: "Avulso" },
  ],
  recurringModels: [
    { value: "Daily", name: "Diário" },
    { value: "Weekly", name: "Semanal" },
    { value: "Monthly", name: "Mensal" },
  ],
  schedulingTypes: [
    { value: "Inspection", name: "Vistoria" },
    { value: "Maintence", name: "Manutenção" },
  ],
  movementType: [
    { value: "Repair", name: "Reparação" },
    { value: "Penalty", name: "Multa" },
    { value: "Others", name: "Outros" },
  ],
  paymentTypes: [
    { value: "Money", name: "Dinheiro" },
    { value: "Transfer", name: "Transferência Bancária" },
    { value: "Debit", name: "Cartão de Débito" },
    { value: "Credit", name: "Cartão de Crédito" },
  ],
  deliveryPaymentTypes: [
    { value: "Pix", name: "Pix" },
    { value: "Credit", name: "Cartão de Crédito" },
  ],
  DaysOfWeek: [
    { value: "Sunday", name: "Domingo" },
    { value: "Monday", name: "Segunda-feira" },
    { value: "Tuesday", name: "Terça-feira" },
    { value: "Wednesday", name: "Quarta-feira" },
    { value: "Thursday", name: "Quinta-feira" },
    { value: "Friday", name: "Sexta-feira" },
    { value: "Saturday ", name: "Sabado" },
  ],
  PhoneType: [
    { value: "Phone", name: "Telefone" },
    { value: "CellPhone", name: "Celular" },
    { value: "Fax", name: "Fax" },
  ],
  ContractSituations: [
    { value: "UpToDate", name: "Em dia" },
    { value: "Debt", name: "Em Débito" },
    { value: "Pending", name: "Pendente" },
    { value: "Rejected", name: "Rejeitado" },
  ],
  DeliveryRequestStatuses: [
    { value: "New", name: "Nova" },
    { value: "WaitingDriver", name: "Aguardando motorista" },
    { value: "DriverAssigned", name: "Motorista atribuído" },
    { value: "InRoute", name: "Em rota" },
    { value: "Done", name: "Entregue" },
    { value: "Cancelled", name: "Cancelado" },
    { value: "Scheduled", name: "Agendado" },
  ],
  typesDiscount: [
    { name: "Oferta", value: "Sale" },
    { name: "Evento", value: "Event" },
    { name: "Cliente", value: "Customer" },
    { name: "Recomendação", value: "Recommendation" },
  ],
  typesDevolution: [
    { name: "Devolução", value: "Devolution" },
    { name: "Descarte", value: "Discard" },
  ],
  bankAccountTypes: [
    { name: "Corrente", value: "Checking" },
    { name: "Poupança", value: "Savings" },
  ],
  TargetPublicTypes: [
    { value: "General", name: "Geral" },
    { value: "Lessees", name: "Motoristas" },
    { value: "Establishment", name: "Estabelecimentos" },
  ],
  PixKeyTypes: [
    { value: "Cpf", name: "CPF" },
    { value: "Cnpj", name: "CNPJ" },
    { value: "Email", name: "E-mail" },
    { value: "Phone", name: "Celular" },
    { value: "RandonKey", name: "Chave aleatória" },
  ],
  VehicleTypes: [
    { value: "Car", name: "Carro" },
    { value: "Motorcycle", name: "Moto" },
  ],
  benefitKinds: [
    {
      value: "Payment",
      name: "Pagamento",
    },
    {
      value: "Url",
      name: "URL",
    },
    {
      value: "Coupon",
      name: "Cupom",
    }
  ],
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
