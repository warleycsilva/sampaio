import { Image } from 'react-native';
import React from 'react';

const LogotipoImage = require('../assets/images/logo.png');

interface Props {}

const LogoImage: React.FC<Props> = ({ ...rest }) => {
  return (
    <Image
      source={LogotipoImage}
      style={{
        marginVertical: 5,
        width: '100%',
        height: 50,
      }}
      resizeMode={'contain'}
    />
  );
};

export default LogoImage;
