import React, { createContext, useContext, useEffect, useState } from 'react';
import { PassageiroRequest } from '../types/viagens';

interface CartContextValue {
  passengers: PassageiroRequest[];
  count: number;
  setPassengers: (passengers: PassageiroRequest[]) => void;
  clearCart: () => void;
}

const CartContext = createContext<CartContextValue | undefined>(undefined);

export const CartProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [passengers, setPassengersState] = useState<PassageiroRequest[]>([]);
  const [count, setCount] = useState(0);

  useEffect(() => { setCount(passengers.length); }, [passengers]);

  const setPassengers = (list: PassageiroRequest[]) => setPassengersState(list);
  const clearCart = () => { setPassengersState([]); setCount(0); };

  return (
    <CartContext.Provider value={{ passengers, count, setPassengers, clearCart }}>
      {children}
    </CartContext.Provider>
  );
};

export const useCart = (): CartContextValue => {
  const ctx = useContext(CartContext);
  if (!ctx) throw new Error('useCart must be used inside CartProvider');
  return ctx;
};
