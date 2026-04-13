import { ScrollView, Text, View } from 'react-native';
import React from 'react';
import styles from './styles';

interface Props {}

const PagamentoComponente: React.FC<Props> = ({}: Props) => {
  return (
    <>
      <View style={{ width: '100%' }}>
      </View>
      <ScrollView style={styles.scrollFullHeight}>
        <View>
          <Text style={styles.textoTitulo}>{'PAGAMENTO: '}</Text>
        </View>
      </ScrollView>
    </>
  );
};

export default PagamentoComponente;
