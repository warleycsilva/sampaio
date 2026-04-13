import type { ImageSourcePropType } from 'react-native';
import type ToastContainer from 'react-native-toast-notifications/lib/typescript/toast-container';

declare global {
  // eslint-disable-next-line no-var
  var toast: ToastContainer | null;
}

declare module '*.png' {
  const value: ImageSourcePropType;
  export default value;
}

declare module '*.jpg' {
  const value: ImageSourcePropType;
  export default value;
}

declare module '*.svg' {
  const value: ImageSourcePropType;
  export default value;
}

export {};
