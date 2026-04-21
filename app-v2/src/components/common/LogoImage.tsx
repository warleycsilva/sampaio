import React from 'react';
import { Image, StyleSheet, View } from 'react-native';
import { Spacing } from '../../theme';

const LogoImage: React.FC = () => (
  <View style={styles.container}>
    <Image source={require('../../../assets/images/logo.png')} style={styles.logo} resizeMode="contain" />
  </View>
);

const styles = StyleSheet.create({
  container: { alignItems: 'center', paddingVertical: Spacing.sm },
  logo:      { width: 160, height: 60 },
});

export default LogoImage;
