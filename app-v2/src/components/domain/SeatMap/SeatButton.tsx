import React from 'react';
import { StyleSheet, Text, TouchableOpacity } from 'react-native';
import { Colors } from '../../../theme';

interface Props {
  numero: number;
  ocupado: boolean;
  selecionado: boolean;
  onPress: (n: number) => void;
}

const SeatButton: React.FC<Props> = ({ numero, ocupado, selecionado, onPress }) => {
  const bg = ocupado ? '#bdbdbd' : selecionado ? Colors.primary : Colors.white;
  const border = ocupado ? '#bdbdbd' : Colors.primary;
  const textColor = selecionado || ocupado ? Colors.white : Colors.primary;

  return (
    <TouchableOpacity
      style={[styles.seat, { backgroundColor: bg, borderColor: border }]}
      onPress={() => !ocupado && onPress(numero)}
      disabled={ocupado}
      activeOpacity={0.7}
    >
      <Text style={[styles.label, { color: textColor }]}>{numero}</Text>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  seat:  { width: 40, height: 40, borderRadius: 8, borderWidth: 1.5, justifyContent: 'center', alignItems: 'center', margin: 4 },
  label: { fontSize: 13, fontWeight: '600' },
});

export default SeatButton;
