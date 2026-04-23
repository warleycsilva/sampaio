import React from 'react';
import { StyleSheet, Text, TouchableOpacity, View } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { format } from 'date-fns';
import { Viagem } from '../../types/viagens';
import { Colors, Spacing, Typography } from '../../theme';

interface Props { item: Viagem; onPress: () => void; }

const formatPreco = (preco: number) =>
  preco.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });

const TripCard: React.FC<Props> = ({ item, onPress }) => {
  const vagas = item.qtdVagas - item.vagasVendidas;
  const baixasVagas = vagas <= 5;

  return (
    <TouchableOpacity style={styles.card} onPress={onPress} activeOpacity={0.85}>
      <View style={styles.route}>
        <Text style={styles.city}>{item.origem}</Text>
        <Ionicons name="arrow-down" size={14} color={Colors.primary} style={{ marginVertical: 2 }} />
        <Text style={styles.city}>{item.destino}</Text>
      </View>
      <View style={styles.divider} />
      <View style={styles.footer}>
        <View style={styles.info}>
          <Ionicons name="time-outline" size={14} color={Colors.gray} />
          <Text style={styles.infoText}>{format(new Date(item.dataPartida), 'dd/MM/yyyy HH:mm')}</Text>
        </View>
        <View style={styles.info}>
          <Ionicons name="people-outline" size={14} color={baixasVagas ? Colors.error : Colors.success} />
          <Text style={[styles.infoText, { color: baixasVagas ? Colors.error : Colors.gray }]}>
            {vagas} {vagas === 1 ? 'vaga' : 'vagas'}
          </Text>
        </View>
        <Text style={styles.price}>{formatPreco(item.preco)}</Text>
      </View>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  card: {
    backgroundColor: Colors.card, borderRadius: 14, padding: Spacing.md, marginVertical: 6,
    shadowColor: '#000', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.08, shadowRadius: 6, elevation: 3,
  },
  route:    { flexDirection: 'column' },
  city:     { ...Typography.bodyBold, color: Colors.black },
  price:    { ...Typography.bodyBold, color: Colors.primary },
  divider:  { height: 1, backgroundColor: '#f0f0f0', marginVertical: Spacing.sm },
  footer:   { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center' },
  info:     { flexDirection: 'row', alignItems: 'center', gap: 4 },
  infoText: { ...Typography.caption, color: Colors.gray, marginLeft: 4 },
});

export default TripCard;
