import React from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { NavigationContainer } from '@react-navigation/native';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import Home from '../screens/Home/Home.screen';
import Profile from '../screens/Confirmacao/Confirmacao.screen';
import { Colors } from '../utils/consts';
import EscolherAssento from "../screens/EscolherAssento/EscolherAssento.screen";
import PagamentoTela from "../screens/PagamentoTela/PagamentoTela.screen";
import Registrar from "../screens/Registrar/Registrar.screen";
import ListaViagens from "../screens/ListaViagens/ListaViagens.screen";
import Confirmacao from "../screens/Confirmacao/Confirmacao.screen";

const Stack = createNativeStackNavigator();
const Tab = createBottomTabNavigator();

function MyTabs() {
  return (
    <Tab.Navigator screenOptions={{
        tabBarStyle: {
            height: 1
        }
    }}>
      <Tab.Screen
        name="Home"
        component={Home}
        options={{
          title: 'Pesquisa',
          headerShown: false,
          tabBarActiveBackgroundColor: Colors.primary,
          tabBarInactiveBackgroundColor: Colors.secondary,
          tabBarActiveTintColor: Colors.white,
          tabBarInactiveTintColor: Colors.white,
        }}
      />
      <Tab.Screen
        name="ListaViagens"
        component={ListaViagens}
        options={{
          title: 'Lista',
          headerShown: false,
          tabBarActiveBackgroundColor: Colors.primary,
          tabBarInactiveBackgroundColor: Colors.secondary,
          tabBarActiveTintColor: Colors.white,
          tabBarInactiveTintColor: Colors.white,
        }}
      />
      <Tab.Screen
        name="ChoosePlace"
        component={EscolherAssento}
        options={{
          title: 'Assento',
          headerShown: false,
          tabBarActiveBackgroundColor: Colors.primary,
          tabBarInactiveBackgroundColor: Colors.secondary,
          tabBarActiveTintColor: Colors.white,
          tabBarInactiveTintColor: Colors.white,
        }}
      />
      <Tab.Screen
        name="Payment"
        component={PagamentoTela}
        options={{
          title: 'Pagamento',
          headerShown: false,
          tabBarActiveBackgroundColor: Colors.primary,
          tabBarInactiveBackgroundColor: Colors.secondary,
          tabBarActiveTintColor: Colors.white,
          tabBarInactiveTintColor: Colors.white,
        }}
      />
      <Tab.Screen
        name="Confirmacao"
        component={Confirmacao}
        options={{
          title: 'Confirmação',
          headerShown: false,
          tabBarActiveBackgroundColor: Colors.primary,
          tabBarInactiveBackgroundColor: Colors.secondary,
          tabBarActiveTintColor: Colors.white,
          tabBarInactiveTintColor: Colors.white,
        }}
      />
      <Tab.Screen
        name="Registrar"
        component={Registrar}
        options={{
          title: 'Registrar',
          headerShown: false,
          tabBarActiveBackgroundColor: Colors.primary,
          tabBarInactiveBackgroundColor: Colors.secondary,
          tabBarActiveTintColor: Colors.white,
          tabBarInactiveTintColor: Colors.white,
        }}
      />
    </Tab.Navigator>
  );
}

const MainNavigation = () => {
  return (
    <NavigationContainer>
      <Stack.Navigator screenOptions={{ headerShown: false }}>
        <Stack.Screen
          name="HomeBase"
          options={{ headerShown: false }}
          component={MyTabs}
        />
      </Stack.Navigator>
    </NavigationContainer>
  );
};

export default MainNavigation;
