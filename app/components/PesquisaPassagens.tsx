import { Alert, Text, TouchableOpacity, View } from 'react-native';
import React, { useState } from 'react';
import styles from './styles';
import InputBuscaLugar from './InputBuscaLugar';
import InputBuscaData from './InputBuscaData';
import { Place, SearchForm } from '../types/Places';
import { useFocusEffect } from '@react-navigation/native';
import AppButton from './AppButton';
import {getViagens} from "../services/viagens";

interface Props {}

const PesquisaPassagens: React.FC<Props> = ({ ...rest }) => {
  const data: Place[] = [
    { id: 'BTM', name: 'Betim' },
    { id: 'BHZ', name: 'Belo Horizonte' },
    { id: 'CTG', name: 'Contagem' },
    { id: 'GUA', name: 'Guarapari' },
    { id: 'SAO', name: 'São Paulo' },
    { id: 'CBW', name: 'Curitiba' },
    { id: 'RJZ', name: 'Rio de Janeiro' },
    { id: 'SAL', name: 'Salvador' },
    { id: 'BRL', name: 'Brasilia' },
    { id: 'SJC', name: 'São José dos Campos' },
  ];
  const [form, setForm] = useState<Partial<SearchForm>>({
    initialDate: new Date(),
  });
  useFocusEffect(() => {
    data.sort((a, b) => {
      if (a.name < b.name) {
        return -1;
      }
      if (a.name > b.name) {
        return 1;
      }
      return 0;
    });
  });
  return (
    <View style={styles.cardPesquisa}>
      <Text style={styles.textoTitulo}>{'PASSAGENS DISPONÍVEIS: '}</Text>
      <AppButton
        text={'Pesquisar'}
        onClick={() => {
          Alert.alert(
            'Buscando passangens',
            'Aguarde enquando encontramos as melhores passagens...',
            [
              {
                text: 'Ok',
                onPress: () => {
                  console.log('ok');
                },
              },
            ],
          );
        }}
      />
    </View>
  );
};

export default PesquisaPassagens;
