import React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { Ionicons } from '@expo/vector-icons';
import { Colors, Spacing, Typography } from '../../theme';

const PagamentoTelaScreen: React.FC = () => (
  <SafeAreaView style={{ flex: 1, backgroundColor: Colors.background }}>
    <View style={styles.center}>
      <Ionicons name="card-outline" size={64} color={Colors.border} />
      <Text style={styles.title}>Pagamento</Text>
      <Text style={styles.sub}>O pagamento é realizado no resumo da compra. Selecione uma passagem para continuar.</Text>
    </View>
  </SafeAreaView>
);

const styles = StyleSheet.create({
  center: { flex: 1, justifyContent: 'center', alignItems: 'center', padding: Spacing.xl },
  title:  { ...Typography.heading2, color: Colors.primary, marginTop: Spacing.md, marginBottom: Spacing.sm },
  sub:    { ...Typography.body, color: Colors.lightGray, textAlign: 'center', lineHeight: 22 },
});

export default PagamentoTelaScreen;
