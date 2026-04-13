export default interface IResponseRoutes {
  origin: {
    latitude: string;
    longitude: string;
  };
  destination: {
    latitude: string;
    longitude: string;
  };
  waypoints: [
    {
      latitude: string;
      longitude: string;
    }
  ];
  hasDriver: boolean;
  hasRoute: boolean;
  message: string;
}
