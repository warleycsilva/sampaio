import { Text, TouchableOpacity, View } from 'react-native';
import React, { useState } from 'react';
import styles from './styles';
import { Colors } from '../utils/consts';

interface Props {
  onClick(): void;
  text: string;
}

const HorizontalNumberSpinner: React.FC<Props> = ({
  onClick,
  text,
  ...rest
}) => {
  const [counter, setCounter] = useState(0);
  return (
    <View
      style={{
        flexDirection: 'row',
        alignItems: 'center',
      }}>
      {counter === 0 && (
        <TouchableOpacity
          onPress={() => setCounter(counter + 1)}
          style={{
            justifyContent: 'center',
            alignItems: 'center',
            width: 100,
            height: 30,
            borderRadius: 30,
            backgroundColor: Colors.primary,
          }}>
          <Text
            style={{
              paddingVertical: 5,
              color: Colors.white,
              textAlign: 'center',
              fontSize: 16,
              fontWeight: 'bold',
            }}>
            Comprar
          </Text>
        </TouchableOpacity>
      )}
      {counter > 0 && (
        <>
          <TouchableOpacity
            onPress={() => setCounter(counter - 1)}
            style={{
              justifyContent: 'center',
              alignItems: 'center',
              width: 30,
              height: 30,
              borderRadius: 30,
              backgroundColor: Colors.primary,
            }}>
            <Text
              style={{
                paddingVertical: 5,
                color: Colors.white,
                textAlign: 'center',
                fontSize: 16,
                fontWeight: 'bold',
              }}>
              -
            </Text>
          </TouchableOpacity>
          <Text
            style={{ marginHorizontal: 10, color: Colors.black, fontSize: 20 }}>
            {counter}
          </Text>
          <TouchableOpacity
            onPress={() => setCounter(counter + 1)}
            style={{
              justifyContent: 'center',
              alignItems: 'center',
              width: 30,
              height: 30,
              borderRadius: 30,
              backgroundColor: Colors.primary,
            }}>
            <Text
              style={{
                paddingVertical: 5,
                color: Colors.white,
                textAlign: 'center',
                fontSize: 16,
                fontWeight: 'bold',
              }}>
              +
            </Text>
          </TouchableOpacity>
        </>
      )}
    </View>
  );
};

export default HorizontalNumberSpinner;
