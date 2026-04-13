import {Text, View, Image} from 'react-native';
import React from 'react';
import AssentoComponente from './AssentoComponente';
import {Assento} from '../types/Bus';

const AtendimentoPreferencial = require("../assets/images/atendimento_preferencial.jpg");

interface Props {
    numeroFileira: number;
    assentos: Assento[];

    setAssentos(v: Assento[]): void;

    assentoSelecionado(v: number): void;

    atual: number | undefined
    ocupados: number[];
}

const FileiraComponente: React.FC<Props> = ({
                                                numeroFileira,
                                                assentos,
                                                setAssentos,
                                                assentoSelecionado,
                                                atual,
                                                ocupados
                                            }: Props) => {
    function selecionarAssento(numero: number) {
        const novosAssentos = assentos.map(x => {
            if (x.numero === numero) {
                x.selecionado = !x.selecionado;
            }
            return x;
        });
        setAssentos(novosAssentos);
        assentoSelecionado(numero);
    }

    return (
        <>
            <View
                style={{
                    width: '100%',
                    justifyContent: 'center',
                }}
            >
                {numeroFileira == 1 ?
                    <Image
                        source={AtendimentoPreferencial}
                        style={{
                            marginVertical: 5,
                            width: '100%',
                            height: 50,
                        }}
                        resizeMode={'contain'}
                    /> : <></>}
            </View>
        <View
            style={{
                flexDirection: 'row',
                justifyContent: 'space-between',
                flex: 1,
                borderBottomWidth: 1
            }}>
            <View
                style={{
                    paddingVertical: 10,
                    paddingHorizontal: 10
                }}>
                <View
                    style={{
                        flexDirection: 'row',
                        justifyContent: 'space-between',
                        flex: 1,
                    }}>
                    <AssentoComponente
                        number={(numeroFileira - 1) * 4 + 1}
                        selected={((numeroFileira - 1) * 4 + 1) == atual}
                        setSelected={() => selecionarAssento((numeroFileira - 1) * 4 + 1)}
                        ocupados={ocupados}
                    />
                    <AssentoComponente
                        number={(numeroFileira - 1) * 4 + 2}
                        selected={((numeroFileira - 1) * 4 + 2) == atual}
                        setSelected={() => selecionarAssento((numeroFileira - 1) * 4 + 2)}
                        ocupados={ocupados}
                    />
                </View>
            </View>
            <View
                style={{
                    paddingVertical: 10,
                    paddingHorizontal: 10,
                }}>
                <View
                    style={{
                        flexDirection: 'row',
                        justifyContent: 'space-between',
                        flex: 1,
                    }}>
                    {((numeroFileira - 1) * 4 + 3 <= 50) &&
                        <AssentoComponente
                            number={(numeroFileira - 1) * 4 + 4}
                            selected={((numeroFileira - 1) * 4 + 4) == atual}
                            setSelected={() => selecionarAssento((numeroFileira - 1) * 4 + 4)}
                            ocupados={ocupados}
                        />}
                    {((numeroFileira - 1) * 4 + 3 <= 50) &&
                        <AssentoComponente
                            number={(numeroFileira - 1) * 4 + 3}
                            selected={((numeroFileira - 1) * 4 + 3) == atual}
                            setSelected={() => selecionarAssento((numeroFileira - 1) * 4 + 3)}
                            ocupados={ocupados}
                        />}
                </View>
            </View>
        </View></>
    );
};

export default FileiraComponente;
