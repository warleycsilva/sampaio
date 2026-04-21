import React from 'react';
import { SafeAreaView, StyleSheet, Text, View } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { Colors, Spacing, Typography } from '../../theme';

const RegistrarScreen: React.FC = () => (
  <SafeAreaView style={{ flex: 1, backgroundColor: Colors.background }}>
    <View style={styles.center}>
      <Ionicons name="person-circle-outline" size={80} color={Colors.primary} />
      <Text style={styles.title}>Minha Conta</Text>
      <Text style={styles.sub}>Funcionalidade em breve.</Text>
    </View>
  </SafeAreaView>
);

const styles = StyleSheet.create({
  center: { flex: 1, justifyContent: 'center', alignItems: 'center', padding: Spacing.xl },
  title:  { ...Typography.heading2, color: Colors.primary, marginTop: Spacing.md, marginBottom: Spacing.sm },
  sub:    { ...Typography.body, color: Colors.lightGray },
});

export default RegistrarScreen;
