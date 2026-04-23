import React from 'react';
import { StyleSheet, TextInput, TextInputProps, View } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { Colors, Spacing } from '../../theme';

interface Props extends TextInputProps {
  icon?: React.ComponentProps<typeof Ionicons>['name'];
}

const FormInput: React.FC<Props> = ({ icon, style, ...rest }) => (
  <View style={styles.wrapper}>
    {icon && <Ionicons name={icon} size={18} color={Colors.primary} style={styles.icon} />}
    <TextInput
      style={[styles.input, style]}
      placeholderTextColor={Colors.lightGray}
      {...rest}
    />
  </View>
);

const styles = StyleSheet.create({
  wrapper: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1.5,
    borderColor: Colors.border,
    borderRadius: 10,
    paddingHorizontal: Spacing.sm,
    backgroundColor: '#fafafa',
    marginBottom: Spacing.sm,
  },
  icon:  { marginRight: Spacing.sm },
  input: { flex: 1, height: 48, fontSize: 15, color: Colors.black },
});

export default FormInput;
