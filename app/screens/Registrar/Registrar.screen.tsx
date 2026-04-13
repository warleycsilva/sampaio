import React, { useState } from 'react';
import {Button, TextInput, View} from 'react-native';

const Registrar = ({}: any) => {
  // If null, no SMS has been sent
  const [confirm, setConfirm] = useState(null);

  const [code, setCode] = useState('');


  return (
    <View style={{flex: 1}}>
      <TextInput style={{height: 100, width: '100%'}} value={code} onChangeText={text => setCode(text)} />
      <Button title="Confirm Code" onPress={() => console.log('registrarb')} />
    </View>
  );
};

export default Registrar;
