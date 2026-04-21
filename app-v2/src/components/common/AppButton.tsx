import React from 'react';
import { ActivityIndicator, StyleSheet, Text, TouchableOpacity } from 'react-native';
import { Colors, Spacing, Typography } from '../../theme';

interface Props {
  text: string;
  onPress: () => void;
  variant?: 'primary' | 'secondary' | 'danger';
  loading?: boolean;
  disabled?: boolean;
}

const AppButton: React.FC<Props> = ({ text, onPress, variant = 'primary', loading = false, disabled = false }) => {
  const isDisabled = disabled || loading;
  return (
    <TouchableOpacity
      style={[styles.base, styles[variant], isDisabled && styles.disabled]}
      onPress={onPress}
      disabled={isDisabled}
      activeOpacity={0.8}
    >
      {loading
        ? <ActivityIndicator color={variant === 'primary' ? Colors.white : Colors.primary} />
        : <Text style={[styles.label, variant === 'primary' ? styles.labelPrimary : styles.labelSecondary]}>{text}</Text>
      }
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  base:           { marginHorizontal: Spacing.md, marginVertical: Spacing.xs, borderRadius: 12, paddingVertical: 14, alignItems: 'center' },
  primary:        { backgroundColor: Colors.primary },
  secondary:      { backgroundColor: 'transparent', borderWidth: 1.5, borderColor: Colors.primary },
  danger:         { backgroundColor: Colors.error },
  disabled:       { opacity: 0.45 },
  label:          { ...Typography.bodyBold },
  labelPrimary:   { color: Colors.white },
  labelSecondary: { color: Colors.primary },
});

export default AppButton;
