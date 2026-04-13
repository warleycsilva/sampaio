import { ScrollView, Text, TouchableOpacity, View } from 'react-native';
import React from 'react';
import { Colors } from '../utils/consts';
import {useCarrinho} from "../context/passageirosContext";

interface Props {
  ocupados: number[];
  number: number;
  selected?: boolean;
  setSelected(v: boolean): void;
}

const AssentoComponente: React.FC<Props> = ({
  number,
  selected,
  setSelected,
  ocupados
}: Props) => {
    const { passageiros} = useCarrinho();

    const busy: boolean = !!ocupados?.filter(x=>x==number).length || passageiros.some(p => p.assento == number);
  return (
    <TouchableOpacity
      onPress={() => busy ? toast?.show("Esse assento não está disponível") : setSelected(!selected)}
      style={{
        width: 45,
        height: 45,
        backgroundColor: selected ? Colors.primary : busy ? Colors.gray : Colors.secondary,
        justifyContent: 'center',
        alignItems: 'center',
        borderRadius: 5,
        marginHorizontal: 10,
      }}>
      <Text
        style={{
          color: Colors.fullWhite,
          fontWeight: '500',
        }}>
        {number.toString().length === 2
          ? number.toString()
          : `0${number.toString()}`}
      </Text>
    </TouchableOpacity>
  );
};

export default AssentoComponente;
