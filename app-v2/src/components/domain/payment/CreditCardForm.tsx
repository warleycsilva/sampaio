import React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import { CartaoCredito } from '../../../types/viagens';
import { Colors, Spacing, Typography } from '../../../theme';
import FormInput from '../../common/FormInput';
import AppButton from '../../common/AppButton';

interface Props {
  card: CartaoCredito | undefined;
  onChange: (card: CartaoCredito) => void;
  onSubmit: () => void;
  onCancel: () => void;
  loading: boolean;
  error?: string;
}

const CreditCardForm: React.FC<Props> = ({ card, onChange, onSubmit, onCancel, loading, error }) => {
  const update = (field: keyof CartaoCredito) => (value: string | number) =>
    onChange({ ...card, [field]: value });

  return (
    <View>
      <Text style={styles.sectionTitle}>Dados do titular</Text>
      <FormInput icon="person-outline" placeholder="Nome do titular"
        value={card?.nomeTitularCartao ?? ''} onChangeText={update('nomeTitularCartao')} returnKeyType="next" />
      <FormInput icon="card-outline" placeholder="CPF do titular" keyboardType="numeric" maxLength={11}
        value={card?.docTitularCartao ?? ''} onChangeText={update('docTitularCartao')} returnKeyType="next" />

      <Text style={styles.sectionTitle}>Dados do cartão</Text>
      <FormInput placeholder="Número do cartão" keyboardType="numeric" maxLength={16}
        textContentType="creditCardNumber"
        value={card?.numeroCartao ?? ''} onChangeText={update('numeroCartao')} returnKeyType="next" />
      <View style={styles.row}>
        <FormInput style={styles.halfLeft} placeholder="Mês exp." keyboardType="numeric" maxLength={2}
          value={card?.mesExpiracao ? String(card.mesExpiracao) : ''}
          onChangeText={v => update('mesExpiracao')(v ? Number(v) : '')} returnKeyType="next" />
        <FormInput style={styles.halfRight} placeholder="Ano exp. (AA)" keyboardType="numeric" maxLength={2}
          value={card?.anoExpiracao ? String(card.anoExpiracao) : ''}
          onChangeText={v => update('anoExpiracao')(v ? Number(v) : '')} returnKeyType="next" />
      </View>
      <FormInput placeholder="CVV" keyboardType="numeric" maxLength={3}
        value={card?.codigoSegurancaCartao ?? ''} onChangeText={update('codigoSegurancaCartao')} returnKeyType="next" />

      <Text style={styles.sectionTitle}>Endereço do titular</Text>
      <View style={styles.row}>
        <FormInput style={styles.flex2} placeholder="CEP" keyboardType="numeric" maxLength={8}
          value={card?.enderecoCepCartao ?? ''} onChangeText={update('enderecoCepCartao')} returnKeyType="next" />
        <FormInput style={styles.flex1} placeholder="UF" maxLength={2}
          value={card?.enderecoEstadoCartao ?? ''} onChangeText={update('enderecoEstadoCartao')} returnKeyType="next" />
      </View>
      <View style={styles.row}>
        <FormInput style={styles.flex3} placeholder="Rua"
          value={card?.enderecoRuaCartao ?? ''} onChangeText={update('enderecoRuaCartao')} returnKeyType="next" />
        <FormInput style={styles.flex2} placeholder="Número" keyboardType="numeric"
          value={card?.enderecoNumeroCartao ?? ''} onChangeText={update('enderecoNumeroCartao')} returnKeyType="next" />
      </View>
      <FormInput placeholder="Bairro"
        value={card?.enderecoBairroCartao ?? ''} onChangeText={update('enderecoBairroCartao')} returnKeyType="next" />
      <FormInput placeholder="Cidade"
        value={card?.enderecoCidadeCartao ?? ''} onChangeText={update('enderecoCidadeCartao')} returnKeyType="done" />

      {!!error && <Text style={styles.error}>{error}</Text>}
      <AppButton text={loading ? 'Processando...' : 'Pagar com cartão'} onPress={onSubmit} loading={loading} />
      <AppButton text="Cancelar" onPress={onCancel} variant="secondary" />
    </View>
  );
};

const styles = StyleSheet.create({
  sectionTitle: { ...Typography.caption, color: Colors.gray, fontWeight: '600', marginBottom: Spacing.xs, marginTop: Spacing.sm },
  row:          { flexDirection: 'row', gap: Spacing.xs },
  halfLeft:     { flex: 1, marginRight: Spacing.xs },
  halfRight:    { flex: 1 },
  flex1:        { flex: 1 },
  flex2:        { flex: 2, marginRight: Spacing.xs },
  flex3:        { flex: 3, marginRight: Spacing.xs },
  error:        { color: Colors.error, borderWidth: 1, borderColor: Colors.error, borderRadius: 8, padding: Spacing.sm, marginBottom: Spacing.sm, fontSize: 13 },
});

export default CreditCardForm;
