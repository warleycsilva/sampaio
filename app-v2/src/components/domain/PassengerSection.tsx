import React, { useRef, useState } from 'react';
import { KeyboardAvoidingView, Platform, StyleSheet, Text, TouchableOpacity, View } from 'react-native';
import { Modalize } from 'react-native-modalize';
import { TextInputMask } from 'react-native-masked-text';
import { Ionicons } from '@expo/vector-icons';
import { PassageiroRequest } from '../../types/viagens';
import { useCart } from '../../context/CartContext';
import { Colors, Spacing, Typography } from '../../theme';
import AppButton from '../common/AppButton';
import FormInput from '../common/FormInput';
import PassengerCard from './PassengerCard';
import SeatMap from './SeatMap/SeatMap';

interface Props {
  occupiedSeats: number[];
  onSeatMapToggled: (open: boolean) => void;
  onModalToggled: (open: boolean) => void;
}

const EMPTY: PassageiroRequest = { comprador: false, assento: undefined, nome: '', documento: '', telefone: '' };

const PassengerSection: React.FC<Props> = ({ occupiedSeats, onSeatMapToggled, onModalToggled }) => {
  const [current, setCurrent] = useState<PassageiroRequest>(EMPTY);
  const [seatMapOpen, setSeatMapOpen] = useState(false);
  const modalRef = useRef<Modalize>(null);
  const { passengers, setPassengers } = useCart();

  function validateAndAdd() {
    const t = (global as any).toast;
    if (!current.nome.includes(' ') || current.nome.length < 2) { t?.show('Informe o nome completo', { type: 'warning' }); return; }
    if (!current.documento || current.documento.length < 5)     { t?.show('Informe o documento (RG)', { type: 'warning' }); return; }
    if (!current.telefone || current.telefone.length < 13)      { t?.show('Informe o telefone', { type: 'warning' }); return; }
    if (!current.assento)                                        { t?.show('Escolha um assento', { type: 'warning' }); return; }
    if (passengers.some(p => p.assento === current.assento))    { t?.show('Assento já selecionado por outro passageiro', { type: 'warning' }); return; }

    setPassengers([...passengers, current]);
    t?.show('Passageiro adicionado!', { type: 'success' });
    setCurrent(EMPTY);
    onModalToggled(false);
    modalRef.current?.close();
  }

  function removePassenger(assento: number | undefined) {
    setPassengers(passengers.filter(p => p.assento !== assento));
    (global as any).toast?.show('Passageiro removido.', { type: 'success' });
  }

  function openModal()  { modalRef.current?.open();  onModalToggled(true); }
  function closeModal() { modalRef.current?.close(); onModalToggled(false); setSeatMapOpen(false); onSeatMapToggled(false); }

  return (
    <>
      <Modalize
        ref={modalRef}
        withOverlay adjustToContentHeight withHandle closeOnOverlayTap
        modalStyle={{ paddingHorizontal: Spacing.md }}
      >
        <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : 'height'} style={styles.modalContent}>
          <Text style={styles.modalTitle}>Adicionar passageiro</Text>
          <FormInput
            icon="person-outline" placeholder="Nome completo"
            value={current.nome} onChangeText={t => setCurrent({ ...current, nome: t })}
            returnKeyType="next" keyboardType="name-phone-pad" textContentType="name"
          />
          <FormInput
            icon="card-outline" placeholder="Documento (RG)"
            value={current.documento} maxLength={15}
            onChangeText={t => setCurrent({ ...current, documento: t })} returnKeyType="next"
          />
          <TextInputMask
            style={styles.maskedInput} type="custom" keyboardType="numeric" returnKeyType="done"
            placeholder="Telefone (99) 99999-9999" placeholderTextColor={Colors.lightGray}
            options={{ mask: '(99) 99999-9999' }}
            value={current.telefone} onChangeText={t => setCurrent({ ...current, telefone: t })}
          />
          {!seatMapOpen && (
            <TouchableOpacity style={styles.seatSelector} onPress={() => { setSeatMapOpen(true); onSeatMapToggled(true); }}>
              <Ionicons name="bus-outline" size={18} color={Colors.primary} />
              <Text style={{ color: current.assento ? Colors.black : Colors.lightGray, fontSize: 15, flex: 1 }}>
                {current.assento ? `Assento ${current.assento} selecionado` : 'Escolha um assento'}
              </Text>
              <Ionicons name="chevron-forward" size={16} color={Colors.primary} />
            </TouchableOpacity>
          )}
          {seatMapOpen && (
            <SeatMap
              ocupados={occupiedSeats} atual={current.assento}
              onSelect={n => { setSeatMapOpen(false); onSeatMapToggled(false); setCurrent({ ...current, assento: n }); }}
            />
          )}
          <AppButton text="Adicionar passageiro" onPress={validateAndAdd} />
          <AppButton text="Cancelar" onPress={closeModal} variant="secondary" />
        </KeyboardAvoidingView>
      </Modalize>

      <View style={styles.section}>
        <Text style={styles.sectionTitle}>Passageiros</Text>
        {passengers.map(p => (
          <PassengerCard key={p.documento} passenger={p} onRemove={() => removePassenger(p.assento)} />
        ))}
        <TouchableOpacity style={styles.addBtn} onPress={openModal}>
          <Ionicons name="person-add-outline" size={18} color={Colors.primary} />
          <Text style={styles.addBtnText}>Adicionar passageiro</Text>
        </TouchableOpacity>
      </View>
    </>
  );
};

const styles = StyleSheet.create({
  modalContent:  { paddingTop: 24, paddingBottom: 40, minHeight: 200 },
  modalTitle:    { ...Typography.heading3, color: Colors.primary, marginBottom: Spacing.md },
  maskedInput:   {
    height: 48, borderRadius: 10, borderWidth: 1.5, borderColor: Colors.border,
    paddingHorizontal: Spacing.sm, marginBottom: Spacing.sm, fontSize: 15, color: Colors.black, backgroundColor: '#fafafa',
  },
  seatSelector:  {
    flexDirection: 'row', alignItems: 'center', gap: Spacing.sm,
    borderWidth: 1.5, borderColor: Colors.border, borderRadius: 10,
    paddingHorizontal: Spacing.sm, height: 48, backgroundColor: '#fafafa', marginBottom: Spacing.sm,
  },
  section:       { marginTop: Spacing.sm },
  sectionTitle:  { ...Typography.heading3, color: Colors.primary, marginBottom: Spacing.sm },
  addBtn:        {
    flexDirection: 'row', alignItems: 'center', justifyContent: 'center', gap: Spacing.sm,
    borderWidth: 1.5, borderColor: Colors.primary, borderRadius: 12, paddingVertical: 14,
    backgroundColor: Colors.white, marginBottom: Spacing.md,
  },
  addBtnText:    { ...Typography.bodyBold, color: Colors.primary },
});

export default PassengerSection;
