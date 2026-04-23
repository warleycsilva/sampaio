import Reactotron from 'reactotron-react-native';

Reactotron.configure({ name: 'Sampaio Turismo' })
  .useReactNative({
    networking: {
      ignoreUrls: /symbolicate/,
    },
  })
  .connect();

export default Reactotron;
