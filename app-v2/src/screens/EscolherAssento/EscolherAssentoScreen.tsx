import React from 'react';
import { SafeAreaView, StyleSheet, Text, View } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { Colors, Spacing, Typography } from '../../theme';

const EscolherAssentoScreen: React.FC = () => (
  <SafeAreaView style={{ flex: 1, backgroundColor: Colors.background }}>
    <View style={styles.center}>
      <Ionicons name="bus-outline" size={64} color={Colors.border} />
      <Text style={styles.title}>Escolha de assento</Text>
      <Text style={styles.sub}>Selecione uma viagem na aba Pesquisa para escolher seu assento durante a compra.</Text>
    </View>
  </SafeAreaView>
);

const styles = StyleSheet.create({
  center: { flex: 1, justifyContent: 'center', alignItems: 'center', padding: Spacing.xl },
  title:  { ...Typography.heading2, color: Colors.primary, marginTop: Spacing.md, marginBottom: Spacing.sm },
  sub:    { ...Typography.body, color: Colors.lightGray, textAlign: 'center', lineHeight: 22 },
});

export default EscolherAssentoScreen;
