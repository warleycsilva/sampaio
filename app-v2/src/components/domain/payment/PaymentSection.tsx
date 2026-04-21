import React, { useRef, useState } from 'react';
import { ActivityIndicator, Platform, ScrollView, StyleSheet, Text, TouchableOpacity, View } from 'react-native';
import { Modalize } from 'react-native-modalize';
import Clipboard from '@react-native-clipboard/clipboard';
import { Ionicons } from '@expo/vector-icons';
import { MaskService } from 'react-native-masked-text';
import { useNavigation } from '@react-navigation/native';
import { comprarViagem, getUltimaViagemPorEmail } from '../../../api/viagens';
import { CartaoCredito } from '../../../types/viagens';
import { useCart } from '../../../context/CartContext';
import { Colors, Spacing, Typography } from '../../../theme';
import AppButton from '../../common/AppButton';
import FormInput from '../../common/FormInput';
import CreditCardForm from './CreditCardForm';

interface Props {
  id: string;
  pricePerSeat: number;
  onQrCode: (code: string | null) => void;
  onPaid: (paid: boolean) => void;
  onModalOpen: (open: boolean) => void;
}

interface Buyer { email?: string; cpf?: string; }

const PaymentSection: React.FC<Props> = ({ id, pricePerSeat, onQrCode, onPaid, onModalOpen }) => {
  const [qrCode, setQrCode] = useState<string | null>(null);
  const [paid, setPaid] = useState(false);
  const [loading, setLoading] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);
  const [creditCard, setCreditCard] = useState<CartaoCredito | undefined>();
  const [buyer, setBuyer] = useState<Buyer>({});
  const [cardError, setCardError] = useState<string | undefined>();
  const modalRef = useRef<Modalize>(null);
  const navigation = useNavigation();
  const { passengers, count, clearCart } = useCart();

  const totalValue = MaskService.toMask('money', String(count * pricePerSeat), { unit: 'R$ ', separator: ',', delimiter: '.' });
  const unitValue  = MaskService.toMask('money', String(pricePerSeat),         { unit: 'R$ ', separator: ',', delimiter: '.' });
  const t = (global as any).toast;

  function guard(): boolean {
    if (!passengers.length) { t?.show('Adicione ao menos 1 passageiro.', { type: 'warning' }); return false; }
    if (!buyer.email)        { t?.show('Preencha o E-mail.', { type: 'warning' }); return false; }
    if (!buyer.cpf)          { t?.show('Preencha o CPF.', { type: 'warning' }); return false; }
    return true;
  }

  const passageirosPayload = passengers.map((p, i) => ({ ...p, comprador: i === 0 }));

  function payWithPix() {
    if (!guard()) return;
    setLoading(true);
    comprarViagem(id, { cpf: buyer.cpf, email: buyer.email, tipoPagamento: 'Pix', passageiros: passageirosPayload })
      .then(r => { if (r?.success) { t?.show(r.data.message); setQrCode(r.data.pixQrCode); onQrCode(r.data.pixQrCode); } })
      .finally(() => setLoading(false));
  }

  function openCardModal()  { if (!guard()) return; setModalOpen(true); modalRef.current?.open(); onModalOpen(true); }
  function closeCardModal() { setModalOpen(false); modalRef.current?.close(); onModalOpen(false); }

  function payWithCard() {
    setLoading(true);
    comprarViagem(id, { cpf: buyer.cpf, email: buyer.email, tipoPagamento: 'Credit', passageiros: passageirosPayload, cartaoCredito: creditCard })
      .then(r => {
        if (r?.data?.status === 'Approved') {
          t?.show(r.data.message); setPaid(true); onPaid(true); clearCart(); setCardError(undefined); closeCardModal();
        }
        if (r?.data?.status === 'Failed') setCardError(r?.data?.message);
      })
      .catch(() => t?.show('Falha no pagamento.', { type: 'warning' }))
      .finally(() => setLoading(false));
  }

  function checkStatus() {
    if (!buyer.email) { t?.show('Preencha o E-mail.', { type: 'warning' }); return; }
    getUltimaViagemPorEmail(buyer.email).then(r => {
      if (r.pagamentoStatus === 'Pending')        t?.show('Aguardando pagamento. Você receberá um e-mail quando aprovado.', { type: 'warning' });
      else if (r.pagamentoStatus === 'Approved')  { t?.show('Pagamento aprovado!', { type: 'success' }); setPaid(true); onPaid(true); }
      else                                         t?.show('Pagamento não identificado.', { type: 'warning' });
    });
  }

  if (paid) return (
    <View style={styles.resultCenter}>
      <Ionicons name="checkmark-circle" size={72} color={Colors.success} />
      <Text style={[styles.resultTitle, { color: Colors.success }]}>Pagamento aprovado!</Text>
      <Text style={styles.resultSub}>Comprovante enviado para seu e-mail.{'\n'}Obrigado por viajar conosco!</Text>
      <AppButton text="Voltar ao início" onPress={() => { navigation.goBack(); onQrCode(null); setPaid(false); onPaid(false); }} variant="secondary" />
    </View>
  );

  if (qrCode) return (
    <View style={styles.resultCenter}>
      <Ionicons name="qr-code-outline" size={56} color={Colors.primary} />
      <Text style={[styles.resultTitle, { color: Colors.primary }]}>Pague via PIX</Text>
      <Text style={styles.resultSub}>Copie o código abaixo e use no app do seu banco:</Text>
      <View style={styles.pixBox}><Text style={styles.pixCode} selectable>{qrCode}</Text></View>
      <AppButton text="Copiar código PIX" onPress={() => Clipboard.setString(qrCode)} />
      <Text style={styles.resultSub}>Comprovante enviado para: {buyer.email}</Text>
      <AppButton text="Verificar pagamento" onPress={checkStatus} variant="secondary" />
      <AppButton text="Voltar" onPress={() => { navigation.goBack(); onQrCode(null); setQrCode(null); }} variant="secondary" />
    </View>
  );

  return (
    <View>
      <Text style={styles.sectionTitle}>Pagamento</Text>
      <View style={styles.summaryCard}>
        <View style={styles.summaryRow}>
          <Text style={styles.summaryLabel}>Passageiros</Text>
          <Text style={styles.summaryValue}>{count}</Text>
        </View>
        <View style={styles.summaryRow}>
          <Text style={styles.summaryLabel}>Valor unitário</Text>
          <Text style={styles.summaryValue}>{unitValue}</Text>
        </View>
        <View style={[styles.summaryRow, styles.totalRow]}>
          <Text style={styles.totalLabel}>Total</Text>
          <Text style={styles.totalValue}>{totalValue}</Text>
        </View>
      </View>
      <FormInput icon="mail-outline" placeholder="E-mail" keyboardType="email-address" textContentType="emailAddress"
        value={buyer.email ?? ''} onChangeText={v => setBuyer({ ...buyer, email: v })} />
      <FormInput icon="person-outline" placeholder="CPF (somente números)" keyboardType="numeric" maxLength={11}
        value={buyer.cpf ?? ''} onChangeText={v => setBuyer({ ...buyer, cpf: v })} />
      <View style={styles.payButtons}>
        <TouchableOpacity style={styles.payBtn} onPress={openCardModal}>
          <Ionicons name="card-outline" size={20} color={Colors.primary} />
          <Text style={styles.payBtnLabel}>Cartão de crédito</Text>
        </TouchableOpacity>
        <TouchableOpacity style={[styles.payBtn, styles.payBtnPix]} onPress={payWithPix}>
          {loading
            ? <ActivityIndicator color={Colors.white} />
            : (<><Ionicons name="qr-code-outline" size={20} color={Colors.white} /><Text style={[styles.payBtnLabel, { color: Colors.white }]}>PIX</Text></>)
          }
        </TouchableOpacity>
      </View>
      <Modalize ref={modalRef} withOverlay withHandle adjustToContentHeight
        keyboardAvoidingBehavior={Platform.OS === 'ios' ? 'padding' : 'height'}
        onClose={() => onModalOpen(false)}
      >
        <ScrollView keyboardShouldPersistTaps="always" style={{ padding: Spacing.md }}>
          <CreditCardForm card={creditCard} onChange={setCreditCard} onSubmit={payWithCard} onCancel={closeCardModal} loading={loading} error={cardError} />
        </ScrollView>
      </Modalize>
    </View>
  );
};

const styles = StyleSheet.create({
  sectionTitle: { ...Typography.heading3, color: Colors.primary, marginBottom: Spacing.sm },
  summaryCard:  {
    backgroundColor: Colors.card, borderRadius: 12, padding: Spacing.md, marginBottom: Spacing.md,
    shadowColor: '#000', shadowOffset: { width: 0, height: 1 }, shadowOpacity: 0.06, shadowRadius: 4, elevation: 2,
  },
  summaryRow:   { flexDirection: 'row', justifyContent: 'space-between', marginBottom: Spacing.xs },
  totalRow:     { borderTopWidth: 1, borderTopColor: '#f0f0f0', paddingTop: Spacing.sm, marginTop: Spacing.xs },
  summaryLabel: { ...Typography.body, color: Colors.gray },
  summaryValue: { ...Typography.body, color: Colors.black },
  totalLabel:   { ...Typography.bodyBold, color: Colors.black },
  totalValue:   { ...Typography.heading2, color: Colors.primary },
  payButtons:   { flexDirection: 'row', gap: Spacing.sm, marginBottom: Spacing.md },
  payBtn:       {
    flex: 1, flexDirection: 'row', alignItems: 'center', justifyContent: 'center', gap: 8,
    borderWidth: 1.5, borderColor: Colors.primary, borderRadius: 12, paddingVertical: 14, backgroundColor: Colors.white,
  },
  payBtnPix:    { backgroundColor: Colors.primary, borderColor: Colors.primary },
  payBtnLabel:  { ...Typography.bodyBold, color: Colors.primary },
  resultCenter: { alignItems: 'center', paddingTop: Spacing.lg },
  resultTitle:  { ...Typography.heading2, marginTop: Spacing.md, marginBottom: Spacing.sm, textAlign: 'center' },
  resultSub:    { ...Typography.body, color: Colors.gray, textAlign: 'center', paddingHorizontal: Spacing.lg, marginBottom: Spacing.sm },
  pixBox:       { backgroundColor: '#f5f5f5', borderRadius: 10, padding: Spacing.md, marginVertical: Spacing.md, width: '100%' },
  pixCode:      { fontSize: 11, color: Colors.black, textAlign: 'center', fontFamily: 'monospace' },
});

export default PaymentSection;
