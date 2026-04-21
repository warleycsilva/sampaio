import React, { useEffect, useState } from 'react';
import { SafeAreaView, ScrollView, StyleSheet, Text, TouchableOpacity, View } from 'react-native';
import { useNavigation, useRoute } from '@react-navigation/native';
import { Viagem } from '../../types/viagens';
import { getViagemById } from '../../api/viagens';
import { Colors, Spacing, Typography } from '../../theme';
import TripSummaryCard from '../../components/domain/TripSummaryCard';
import PassengerSection from '../../components/domain/PassengerSection';
import PaymentSection from '../../components/domain/payment/PaymentSection';
import LogoImage from '../../components/common/LogoImage';

type RouteParams = { item?: Viagem };

const ConfirmacaoScreen: React.FC = () => {
  const route = useRoute();
  const navigation = useNavigation();
  const params = route?.params as RouteParams | undefined;
  const [viagem, setViagem] = useState<Viagem | undefined>(params?.item);
  const [pixQrCode, setPixQrCode] = useState<string | null>(null);
  const [paid, setPaid] = useState(false);
  const [seatMapOpen, setSeatMapOpen] = useState(false);
  const [passengerModalOpen, setPassengerModalOpen] = useState(false);
  const [paymentModalOpen, setPaymentModalOpen] = useState(false);

  async function refresh() {
    if (!params?.item?.id) return;
    getViagemById(params.item.id).then(r => setViagem(r));
  }

  useEffect(() => { refresh(); }, [params?.item?.id]);

  if (!viagem) return (
    <SafeAreaView style={styles.safe}>
      <LogoImage />
      <View style={styles.empty}>
        <Text style={styles.emptyText}>Nenhuma viagem selecionada.{'\n'}Volte e escolha uma passagem.</Text>
        <TouchableOpacity style={styles.backBtn} onPress={() => navigation.goBack()}>
          <Text style={styles.backBtnText}>Voltar</Text>
        </TouchableOpacity>
      </View>
    </SafeAreaView>
  );

  return (
    <SafeAreaView style={styles.safe}>
      <LogoImage />
      <ScrollView
        contentContainerStyle={[
          styles.scroll,
          { minHeight: seatMapOpen ? '260%' : paymentModalOpen ? '120%' : '90%' },
        ]}
        keyboardShouldPersistTaps="always"
      >
        <Text style={styles.pageTitle}>Resumo da compra</Text>

        {!passengerModalOpen && !paymentModalOpen && <TripSummaryCard viagem={viagem} />}

        {!pixQrCode && !paid && (
          <PassengerSection
            occupiedSeats={viagem.passageiros.map(p => p.assento)}
            onSeatMapToggled={setSeatMapOpen}
            onModalToggled={v => { setPassengerModalOpen(v); refresh(); }}
          />
        )}

        {!passengerModalOpen && (
          <PaymentSection
            id={viagem.id}
            pricePerSeat={viagem.preco}
            onQrCode={setPixQrCode}
            onPaid={setPaid}
            onModalOpen={setPaymentModalOpen}
          />
        )}

        <TouchableOpacity onPress={() => navigation.goBack()} style={styles.voltarLink}>
          <Text style={styles.voltarText}>Voltar ao início</Text>
        </TouchableOpacity>
      </ScrollView>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  safe:        { flex: 1, backgroundColor: Colors.background },
  scroll:      { padding: Spacing.md },
  pageTitle:   { ...Typography.heading2, color: Colors.primary, marginBottom: Spacing.md },
  empty:       { flex: 1, alignItems: 'center', justifyContent: 'center', padding: Spacing.xl },
  emptyText:   { ...Typography.body, color: Colors.gray, textAlign: 'center', lineHeight: 22 },
  backBtn:     { marginTop: Spacing.lg, backgroundColor: Colors.primary, borderRadius: 10, paddingHorizontal: Spacing.lg, paddingVertical: 12 },
  backBtnText: { ...Typography.bodyBold, color: Colors.white },
  voltarLink:  { alignItems: 'center', marginTop: Spacing.lg },
  voltarText:  { ...Typography.body, color: Colors.primary, textDecorationLine: 'underline' },
});

export default ConfirmacaoScreen;
