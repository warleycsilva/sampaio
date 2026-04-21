import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { Ionicons } from '@expo/vector-icons';
import { RootStackParamList, RootTabParamList } from './types';
import { Colors } from '../theme';
import HomeScreen from '../screens/Home/HomeScreen';
import ListaViagensScreen from '../screens/ListaViagens/ListaViagensScreen';
import EscolherAssentoScreen from '../screens/EscolherAssento/EscolherAssentoScreen';
import PagamentoTelaScreen from '../screens/PagamentoTela/PagamentoTelaScreen';
import ConfirmacaoScreen from '../screens/Confirmacao/ConfirmacaoScreen';
import RegistrarScreen from '../screens/Registrar/RegistrarScreen';

const Stack = createNativeStackNavigator<RootStackParamList>();
const Tab = createBottomTabNavigator<RootTabParamList>();

type IoniconsName = React.ComponentProps<typeof Ionicons>['name'];

const TAB_ICONS: Record<keyof RootTabParamList, { active: IoniconsName; inactive: IoniconsName }> = {
  Home:         { active: 'search',           inactive: 'search-outline' },
  ListaViagens: { active: 'list',             inactive: 'list-outline' },
  ChoosePlace:  { active: 'bus',              inactive: 'bus-outline' },
  Payment:      { active: 'card',             inactive: 'card-outline' },
  Confirmacao:  { active: 'bag-check',        inactive: 'bag-check-outline' },
  Registrar:    { active: 'person',           inactive: 'person-outline' },
};

function BottomTabs() {
  return (
    <Tab.Navigator
      screenOptions={({ route }) => ({
        headerShown: false,
        tabBarStyle: { backgroundColor: Colors.primary, height: 60, paddingBottom: 8 },
        tabBarActiveTintColor: Colors.white,
        tabBarInactiveTintColor: 'rgba(255,255,255,0.45)',
        tabBarIcon: ({ focused, color }) => {
          const icon = TAB_ICONS[route.name as keyof RootTabParamList];
          return <Ionicons name={focused ? icon.active : icon.inactive} size={22} color={color} />;
        },
      })}
    >
      <Tab.Screen name="Home"         component={HomeScreen}           options={{ title: 'Pesquisa' }} />
      <Tab.Screen name="ListaViagens" component={ListaViagensScreen}   options={{ title: 'Viagens' }} />
      <Tab.Screen name="ChoosePlace"  component={EscolherAssentoScreen} options={{ title: 'Assento' }} />
      <Tab.Screen name="Payment"      component={PagamentoTelaScreen}  options={{ title: 'Pagamento' }} />
      <Tab.Screen name="Confirmacao"  component={ConfirmacaoScreen}    options={{ title: 'Resumo' }} />
      <Tab.Screen name="Registrar"    component={RegistrarScreen}      options={{ title: 'Conta' }} />
    </Tab.Navigator>
  );
}

const AppNavigator: React.FC = () => (
  <NavigationContainer>
    <Stack.Navigator screenOptions={{ headerShown: false }}>
      <Stack.Screen name="HomeBase" component={BottomTabs} />
    </Stack.Navigator>
  </NavigationContainer>
);

export default AppNavigator;
