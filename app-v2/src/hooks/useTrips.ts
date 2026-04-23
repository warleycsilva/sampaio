import { useEffect, useState } from 'react';
import { getViagens } from '../api/viagens';
import { Viagem } from '../types/viagens';

interface UseTripsResult {
  trips: Viagem[];
  loading: boolean;
  refresh: () => void;
}

export function useTrips(origem: string, destino: string, data: string): UseTripsResult {
  const [trips, setTrips] = useState<Viagem[]>([]);
  const [loading, setLoading] = useState(false);

  function fetch() {
    setLoading(true);
    getViagens(origem, destino, data.length === 10 ? data : undefined)
      .then(r => setTrips(r.data.itens))
      .finally(() => setLoading(false));
  }

  useEffect(() => { fetch(); }, [origem, destino, data]);

  return { trips, loading, refresh: fetch };
}
