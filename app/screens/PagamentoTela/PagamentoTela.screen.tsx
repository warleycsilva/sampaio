import React from 'react';
import { SafeAreaView, StatusBar, View } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import ListaOpcoesPassagens from '../../components/ListaOpcoesPassagens';

import styles from '../Home/Home.style';
import PagamentoComponente from "../../components/PagamentoComponente";
const PagamentoTela = ({}: any) => {
  const navigation = useNavigation();
  return (
    <>
      <View style={styles.container}>
        <StatusBar barStyle="dark-content" backgroundColor={'#f9f9f9'} />
        <SafeAreaView style={styles.SafeAreaView1} />
        <SafeAreaView style={styles.SafeAreaView2} />
      </View>
      <SafeAreaView style={styles.ListScreenContainerView}>
        <PagamentoComponente />
      </SafeAreaView>
    </>
  );
};

export default PagamentoTela;
