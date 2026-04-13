import React, {createContext, useContext, useEffect, useState} from "react";
import {PassageiroRequest} from "../types/Viagens";

interface CarrinhoContextType {
    passageiros: PassageiroRequest[];
    qtdPassageiros: number;
    atualizarQtdPassageiros: (novaQtd: number) => void;
    atualizarPassageiros: (novosPassageiros: PassageiroRequest[]) => void;
}

const CarrinhoContext = createContext<CarrinhoContextType | undefined>(undefined);

interface CarrinhoProviderProps {
    children: React.ReactNode; // Certifique-se de que você tem esta linha
}

export const CarrinhoProvider: React.FC<CarrinhoProviderProps> = ({children}) => {
    const [qtdPassageiros, setQtdPassageiros] = useState<number>(0);
    const [passageiros, setPassageiros] = useState<PassageiroRequest[]>([]);
    const atualizarQtdPassageiros = (novaQtd: number) => {
        setQtdPassageiros(novaQtd);
    };

    const atualizarPassageiros = (novosPassageiros: any[]) => {
        setPassageiros(novosPassageiros);
    };
    useEffect(() => {
        setQtdPassageiros(passageiros.length);
    }, [passageiros]);

    return (
        <CarrinhoContext.Provider value={{qtdPassageiros, passageiros, atualizarQtdPassageiros, atualizarPassageiros}}>
            {children}
        </CarrinhoContext.Provider>
    );
};
export const useCarrinho = () => {
    const context = useContext(CarrinhoContext);
    if (context === undefined) {
        throw new Error('useCarrinho deve ser usado dentro de um CarrinhoProvider');
    }
    return context;
};