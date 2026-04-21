import React, { useState } from 'react';
import { ActivityIndicator, FlatList, SafeAreaView, StyleSheet, Text, TouchableOpacity, View } from 'react-native';
import { TextInputMask } from 'react-native-masked-text';
import { Ionicons } from '@expo/vector-icons';
import { useNavigation, useIsFocused } from '@react-navigation/native';
import { useTrips } from '../../hooks/useTrips';
import { useCart } from '../../context/CartContext';
import { Viagem } from '../../types/viagens';
import { Colors, Spacing, Typography } from '../../theme';
import TripCard from '../../components/domain/TripCard';
import LogoImage from '../../components/common/LogoImage';
import FormInput from '../../components/common/FormInput';

const HomeScreen: React.FC = () => {
  const [origem, setOrigem] = useState('');
  const [destino, setDestino] = useState('');
  const [data, setData] = useState('');
  const { trips, loading, refresh } = useTrips(origem, destino, data);
  const navigation = useNavigation();
  const { setPassengers } = useCart();

  function handleSelect(item: Viagem) {
    setPassengers([]);
    // @ts-ignore
    navigation.navigate('HomeBase', { screen: 'Confirmacao', params: { item } });
  }

  return (
    <SafeAreaView style={styles.safe}>
      <LogoImage />
      <View style={styles.searchCard}>
        <Text style={styles.searchTitle}>Buscar passagens</Text>
        <FormInput icon="location-outline" placeholder="Origem" value={origem} onChangeText={setOrigem} maxLength={30} />
        <FormInput icon="navigate-outline" placeholder="Destino" value={destino} onChangeText={setDestino} maxLength={30} />
        <TextInputMask
          style={styles.dateInput} type="datetime"
          placeholder="Data de embarque (DD/MM/AAAA)" placeholderTextColor={Colors.lightGray}
          options={{ format: 'DD/MM/YYYY' }} value={data} onChangeText={setData} returnKeyType="done"
        />
        <TouchableOpacity style={styles.searchBtn} onPress={refresh}>
          <Ionicons name="search" size={18} color={Colors.white} />
          <Text style={styles.searchBtnText}>Buscar</Text>
        </TouchableOpacity>
      </View>

      {loading && (
        <View style={styles.centered}>
          <ActivityIndicator size="large" color={Colors.primary} />
          <Text style={styles.loadingText}>Buscando passagens...</Text>
        </View>
      )}
      {!loading && !trips.length && (
        <View style={styles.centered}>
          <Ionicons name="bus-outline" size={48} color={Colors.border} />
          <Text style={styles.emptyText}>Nenhuma passagem encontrada.{'\n'}Tente outros destinos ou datas.</Text>
        </View>
      )}

      <FlatList
        data={trips}
        keyExtractor={i => i.id}
        contentContainerStyle={styles.list}
        onRefresh={refresh}
        refreshing={loading}
        keyboardShouldPersistTaps="always"
        renderItem={({ item }) => <TripCard item={item} onPress={() => handleSelect(item)} />}
      />
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  safe:          { flex: 1, backgroundColor: Colors.background },
  searchCard:    {
    backgroundColor: Colors.card, marginHorizontal: Spacing.md, marginBottom: Spacing.sm,
    borderRadius: 16, padding: Spacing.md,
    shadowColor: '#000', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.07, shadowRadius: 8, elevation: 3,
  },
  searchTitle:   { ...Typography.heading3, color: Colors.primary, marginBottom: Spacing.sm },
  dateInput:     {
    height: 48, borderRadius: 10, borderWidth: 1.5, borderColor: Colors.border,
    paddingHorizontal: Spacing.sm, marginBottom: Spacing.sm, fontSize: 15, color: Colors.black, backgroundColor: '#fafafa',
  },
  searchBtn:     {
    backgroundColor: Colors.primary, borderRadius: 10, paddingVertical: 12,
    flexDirection: 'row', justifyContent: 'center', alignItems: 'center', gap: 8,
  },
  searchBtnText: { ...Typography.bodyBold, color: Colors.white },
  list:          { paddingHorizontal: Spacing.md, paddingBottom: Spacing.md },
  centered:      { alignItems: 'center', marginTop: Spacing.xl, paddingHorizontal: Spacing.xl },
  loadingText:   { ...Typography.caption, color: Colors.gray, marginTop: Spacing.sm },
  emptyText:     { ...Typography.body, color: Colors.lightGray, textAlign: 'center', marginTop: Spacing.sm, lineHeight: 22 },
});

export default HomeScreen;
