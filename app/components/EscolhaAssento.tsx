import {ScrollView, Text, View} from 'react-native';
import React, {useState} from 'react';
import styles from './styles';
import {useNavigation} from '@react-navigation/native';
import AppButton from './AppButton';
import {Colors} from '../utils/consts';
import FileiraComponente from './FileiraComponente';
import {Assento} from '../types/Bus';

interface Props {
    assentoSelecionado(n: number): void;
    atual: number | undefined;
    ocupados: number[];
}

const EscolhaAssentoComponente: React.FC<Props> = ({assentoSelecionado, atual, ocupados}) => {
    const [assentos, setAssentos] = useState<Assento[]>([]);
    const navigation = useNavigation();
    return (
        <>
            <View>
                <View style={{justifyContent: 'center', width: '100%'}}>
                    <Text style={styles.textoTitulo}>
                        {'Escolha um assento disponível: '}
                    </Text>
                    <View
                        style={{
                            backgroundColor: Colors.fullWhite,
                            width: '100%',
                            borderTopLeftRadius: 50,
                            borderTopRightRadius: 50,
                            borderColor: Colors.primary,
                            borderWidth: 1,
                            justifyContent: 'center',
                            paddingHorizontal: 10
                        }}>
                        <View
                            style={{
                                width: '100%',
                                height: 50,
                                borderBottomWidth: 0.5,
                                borderColor: 'gray',
                                justifyContent: 'center',
                                alignItems: 'flex-start',
                                paddingLeft: '10%',
                            }}>
                            <View style={{justifyContent: 'center', alignItems: 'center'}}>
                                <Text style={{fontSize: 12}}>Motorista</Text>
                            </View>
                        </View>
                        <FileiraComponente
                            numeroFileira={1}
                            assentos={assentos}
                            setAssentos={a => setAssentos(a)}
                            assentoSelecionado={n => assentoSelecionado(n)}
                            atual={atual}
                            ocupados={ocupados}
                        />
                        <FileiraComponente
                            numeroFileira={2}
                            assentos={assentos}
                            setAssentos={a => setAssentos(a)}
                            assentoSelecionado={n => assentoSelecionado(n)}
                            atual={atual}
                            ocupados={ocupados}
                        />
                        <FileiraComponente
                            numeroFileira={3}
                            assentos={assentos}
                            setAssentos={a => setAssentos(a)}
                            assentoSelecionado={n => assentoSelecionado(n)}
                            atual={atual}
                            ocupados={ocupados}
                        />
                        <FileiraComponente
                            numeroFileira={4}
                            assentos={assentos}
                            setAssentos={a => setAssentos(a)}
                            assentoSelecionado={n => assentoSelecionado(n)}
                            atual={atual}
                            ocupados={ocupados}
                        />
                        <FileiraComponente
                            numeroFileira={5}
                            assentos={assentos}
                            setAssentos={a => setAssentos(a)}
                            assentoSelecionado={n => assentoSelecionado(n)}
                            atual={atual}
                            ocupados={ocupados}
                        />
                        <FileiraComponente
                            numeroFileira={6}
                            assentos={assentos}
                            setAssentos={a => setAssentos(a)}
                            assentoSelecionado={n => assentoSelecionado(n)}
                            atual={atual}
                            ocupados={ocupados}
                        />
                        <FileiraComponente
                            numeroFileira={7}
                            assentos={assentos}
                            setAssentos={a => setAssentos(a)}
                            assentoSelecionado={n => assentoSelecionado(n)}
                            atual={atual}
                            ocupados={ocupados}
                        />
                        <FileiraComponente
                            numeroFileira={8}
                            assentos={assentos}
                            setAssentos={a => setAssentos(a)}
                            assentoSelecionado={n => assentoSelecionado(n)}
                            atual={atual}
                            ocupados={ocupados}
                        />
                        <FileiraComponente
                            numeroFileira={9}
                            assentos={assentos}
                            setAssentos={a => setAssentos(a)}
                            assentoSelecionado={n => assentoSelecionado(n)}
                            atual={atual}
                            ocupados={ocupados}
                        />
                        <FileiraComponente
                            numeroFileira={10}
                            assentos={assentos}
                            setAssentos={a => setAssentos(a)}
                            assentoSelecionado={n => assentoSelecionado(n)}
                            atual={atual}
                            ocupados={ocupados}
                        />
                        <FileiraComponente
                            numeroFileira={11}
                            assentos={assentos}
                            setAssentos={a => setAssentos(a)}
                            assentoSelecionado={n => assentoSelecionado(n)}
                            atual={atual}
                            ocupados={ocupados}
                        />
                        <FileiraComponente
                            numeroFileira={12}
                            assentos={assentos}
                            setAssentos={a => setAssentos(a)}
                            assentoSelecionado={n => assentoSelecionado(n)}
                            atual={atual}
                            ocupados={ocupados}
                        />
                        <FileiraComponente
                            numeroFileira={13}
                            assentos={assentos}
                            setAssentos={a => setAssentos(a)}
                            assentoSelecionado={n => assentoSelecionado(n)}
                            atual={atual}
                            ocupados={ocupados}
                        />
                    </View>
                </View>
            </View>
            {/*<View style={{width: '100%'}}>*/}
            {/*    <AppButton*/}
            {/*        text={'Voltar'}*/}
            {/*        onClick={() => navigation.navigate('Home', {})}*/}
            {/*    />*/}
            {/*</View>*/}
        </>
    );
};

export default EscolhaAssentoComponente;
