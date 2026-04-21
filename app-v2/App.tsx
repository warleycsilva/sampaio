import 'react-native-gesture-handler';
import React from 'react';
import { Platform, StatusBar } from 'react-native';
import { GestureHandlerRootView } from 'react-native-gesture-handler';
import Toast from 'react-native-toast-notifications';
import { CartProvider } from './src/context/CartContext';
import AppNavigator from './src/navigation/AppNavigator';
import { Colors } from './src/theme';

export default function App() {
  return (
    <GestureHandlerRootView style={{ flex: 1 }}>
      <StatusBar barStyle="light-content" backgroundColor={Colors.primary} />
      <CartProvider>
        <>
          <Toast
            ref={(ref) => { (global as any).toast = ref; }}
            placement="top"
            style={{ marginTop: Platform.OS === 'ios' ? 44 : 30, zIndex: 9999 }}
            textStyle={{ fontSize: 15 }}
            successColor={Colors.success}
            dangerColor={Colors.error}
            warningColor={Colors.warn}
          />
          <AppNavigator />
        </>
      </CartProvider>
    </GestureHandlerRootView>
  );
}
