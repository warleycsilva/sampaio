import React from 'react';
import { ScrollView, StyleSheet, Text, View } from 'react-native';
import { Colors, Spacing, Typography } from '../../../theme';
import SeatButton from './SeatButton';

interface Props {
  ocupados: number[];
  atual?: number;
  onSelect: (n: number) => void;
}

const ROWS = 13;
const COLS = 4;

const LEGEND = [
  { label: 'Disponível',  bg: Colors.white,   border: Colors.primary },
  { label: 'Selecionado', bg: Colors.primary,  border: Colors.primary },
  { label: 'Ocupado',     bg: '#bdbdbd',       border: '#bdbdbd' },
];

const SeatMap: React.FC<Props> = ({ ocupados, atual, onSelect }) => (
  <ScrollView contentContainerStyle={styles.container}>
    <Text style={styles.title}>Escolha um assento</Text>
    <View style={styles.legend}>
      {LEGEND.map(({ label, bg, border }) => (
        <View key={label} style={styles.legendItem}>
          <View style={[styles.swatch, { backgroundColor: bg, borderColor: border }]} />
          <Text style={styles.legendText}>{label}</Text>
        </View>
      ))}
    </View>
    {Array.from({ length: ROWS }, (_, row) => {
      const seats = Array.from({ length: COLS }, (_, col) => row * COLS + col + 1);
      return (
        <View key={row} style={styles.row}>
          <View style={styles.pair}>
            {seats.slice(0, 2).map(n => (
              <SeatButton key={n} numero={n} ocupado={ocupados.includes(n)} selecionado={atual === n} onPress={onSelect} />
            ))}
          </View>
          <View style={styles.aisle} />
          <View style={styles.pair}>
            {seats.slice(2).map(n => (
              <SeatButton key={n} numero={n} ocupado={ocupados.includes(n)} selecionado={atual === n} onPress={onSelect} />
            ))}
          </View>
        </View>
      );
    })}
  </ScrollView>
);

const styles = StyleSheet.create({
  container:  { padding: Spacing.md, alignItems: 'center' },
  title:      { ...Typography.heading3, color: Colors.primary, marginBottom: Spacing.sm },
  legend:     { flexDirection: 'row', alignItems: 'center', gap: 12, marginBottom: Spacing.md },
  legendItem: { flexDirection: 'row', alignItems: 'center', gap: 6 },
  swatch:     { width: 18, height: 18, borderRadius: 4, borderWidth: 1.5 },
  legendText: { ...Typography.caption, color: Colors.gray },
  row:        { flexDirection: 'row', alignItems: 'center', marginVertical: 2 },
  pair:       { flexDirection: 'row' },
  aisle:      { width: 20 },
});

export default SeatMap;
