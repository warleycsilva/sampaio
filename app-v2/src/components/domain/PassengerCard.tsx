import React from 'react';
import { StyleSheet, Text, TouchableOpacity, View } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { PassageiroRequest } from '../../types/viagens';
import { Colors, Spacing, Typography } from '../../theme';

interface Props {
  passenger: PassageiroRequest;
  onRemove: () => void;
}

const PassengerCard: React.FC<Props> = ({ passenger, onRemove }) => (
  <View style={styles.card}>
    <View style={styles.header}>
      <Text style={styles.name}>{passenger.nome}</Text>
      <TouchableOpacity onPress={onRemove} hitSlop={{ top: 8, right: 8, bottom: 8, left: 8 }}>
        <Ionicons name="trash-outline" size={18} color={Colors.error} />
      </TouchableOpacity>
    </View>
    <View style={styles.body}>
      <View style={styles.seatBadge}>
        <Text style={styles.seatNumber}>{passenger.assento}</Text>
      </View>
      <View style={styles.details}>
        <Text style={styles.detail}>Doc: {passenger.documento}</Text>
        <Text style={styles.detail}>Tel: {passenger.telefone}</Text>
      </View>
    </View>
  </View>
);

const styles = StyleSheet.create({
  card: {
    backgroundColor: Colors.card, borderRadius: 12, padding: Spacing.md, marginBottom: 10,
    shadowColor: '#000', shadowOffset: { width: 0, height: 1 }, shadowOpacity: 0.06, shadowRadius: 4, elevation: 2,
  },
  header:     { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: Spacing.sm },
  name:       { ...Typography.bodyBold, color: Colors.black, flex: 1 },
  body:       { flexDirection: 'row', alignItems: 'center' },
  seatBadge:  { width: 44, height: 44, borderRadius: 10, backgroundColor: Colors.primary, justifyContent: 'center', alignItems: 'center', marginRight: Spacing.md },
  seatNumber: { ...Typography.heading3, color: Colors.white },
  details:    { flex: 1 },
  detail:     { ...Typography.caption, color: Colors.gray, marginBottom: 2 },
});

export default PassengerCard;
