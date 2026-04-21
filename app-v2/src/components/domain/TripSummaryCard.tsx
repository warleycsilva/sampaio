import React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { format } from 'date-fns';
import { MaskService } from 'react-native-masked-text';
import { Viagem } from '../../types/viagens';
import { Colors, Spacing, Typography } from '../../theme';

interface Props { viagem: Viagem; }

const TripSummaryCard: React.FC<Props> = ({ viagem }) => {
  const preco = MaskService.toMask('money', String(viagem.preco), { unit: 'R$ ', separator: ',', delimiter: '.' });
  return (
    <View style={styles.card}>
      <View style={styles.route}>
        <View style={styles.cityBlock}>
          <Ionicons name="location" size={16} color={Colors.primary} />
          <Text style={styles.cityLabel}>Origem</Text>
          <Text style={styles.city}>{viagem.origem}</Text>
        </View>
        <Ionicons name="arrow-forward" size={24} color={Colors.primary} />
        <View style={styles.cityBlock}>
          <Ionicons name="flag" size={16} color={Colors.error} />
          <Text style={styles.cityLabel}>Destino</Text>
          <Text style={styles.city}>{viagem.destino}</Text>
        </View>
      </View>
      <View style={styles.divider} />
      <View style={styles.infoRow}>
        <View style={styles.infoItem}>
          <Ionicons name="calendar-outline" size={14} color={Colors.gray} />
          <Text style={styles.infoText}>{format(new Date(viagem.dataPartida), 'dd/MM/yyyy HH:mm')}</Text>
        </View>
        <View style={styles.infoItem}>
          <Ionicons name="pricetag-outline" size={14} color={Colors.gray} />
          <Text style={styles.infoText}>{preco} / passageiro</Text>
        </View>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  card: {
    backgroundColor: Colors.card, borderRadius: 16, padding: Spacing.md, marginBottom: Spacing.md,
    shadowColor: '#000', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.07, shadowRadius: 8, elevation: 3,
  },
  route:     { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center' },
  cityBlock: { alignItems: 'center', flex: 1 },
  cityLabel: { ...Typography.label, color: Colors.lightGray, marginTop: 2 },
  city:      { ...Typography.heading3, color: Colors.black, textAlign: 'center' },
  divider:   { height: 1, backgroundColor: '#f0f0f0', marginVertical: Spacing.sm },
  infoRow:   { flexDirection: 'row', justifyContent: 'space-between' },
  infoItem:  { flexDirection: 'row', alignItems: 'center', gap: 6 },
  infoText:  { ...Typography.caption, color: Colors.gray },
});

export default TripSummaryCard;
