declare module '*.png'
declare module '*.svg'
declare module '*.jpg'

import type ToastContainer from 'react-native-toast-notifications/lib/typescript/toast-container';

declare global {
  // eslint-disable-next-line no-var
  var toast: ToastContainer | null;
}

export {};
