import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { RootStackParamList } from './types';
import HomeScreen from '../screens/Home/HomeScreen';
import ConfirmacaoScreen from '../screens/Confirmacao/ConfirmacaoScreen';
import EscolherAssentoScreen from '../screens/EscolherAssento/EscolherAssentoScreen';
import PagamentoTelaScreen from '../screens/PagamentoTela/PagamentoTelaScreen';
import RegistrarScreen from '../screens/Registrar/RegistrarScreen';

const Stack = createNativeStackNavigator<RootStackParamList>();

const AppNavigator: React.FC = () => (
  <NavigationContainer>
    <Stack.Navigator screenOptions={{ headerShown: false }}>
      <Stack.Screen name="Home" component={HomeScreen} />
      <Stack.Screen name="Confirmacao" component={ConfirmacaoScreen} />
      <Stack.Screen name="EscolherAssento" component={EscolherAssentoScreen} />
      <Stack.Screen name="PagamentoTela" component={PagamentoTelaScreen} />
      <Stack.Screen name="Registrar" component={RegistrarScreen} />
    </Stack.Navigator>
  </NavigationContainer>
);

export default AppNavigator;
