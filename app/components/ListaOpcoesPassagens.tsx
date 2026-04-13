import {FlatList, Text, TextInput, TouchableOpacity, View,} from 'react-native';
import React, {useEffect, useState} from 'react';
import styles from './styles';
import {useIsFocused, useNavigation} from '@react-navigation/native';
import {format} from 'date-fns';
import {MaskService, TextInputMask} from 'react-native-masked-text';
import {getViagens} from "../services/viagens";
import {Colors} from '../utils/consts';
import {Viagem} from "../types/Viagens";
import {useCarrinho} from "../context/passageirosContext";
import {TextField} from "react-native-ui-lib";

const ListaOpcoesPassagens: React.FC = () => {
    const [data, setData] = useState<Viagem[]>([]);
    const [searchOrigem, setSearchOrigem] = useState<string>('');
    const [searchDestino, setSearchDestino] = useState<string>('');
    const [searchData, setSearchData] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(false);
    const navigation = useNavigation();
    const isFocused = useIsFocused();

    const {atualizarQtdPassageiros, atualizarPassageiros} = useCarrinho();


    async function atualizarViagens() {
        setLoading(true)
        getViagens(searchOrigem, searchDestino, (searchData.length == 10 ? searchData : undefined))
            .then(r => {
                setData(r.data.itens)
            })
            .finally(() => {
                setLoading(false)
            })
    }

    useEffect(() => {
        async function listarViagens() {
            setLoading(true)
            getViagens(searchOrigem, searchDestino, (searchData.length == 10 ? searchData : undefined))
                .then(r => {
                    setData(r.data.itens)
                })
                .finally(() => {
                    setLoading(false)
                })
        }

        listarViagens();
    }, [searchOrigem, searchDestino, searchData, isFocused]);
    return (
        <>
            <View style={[styles.scrollFullHeight, {paddingHorizontal: 20}]}>
                <View>
                    <Text style={styles.textoTitulo}>{'BUSCAR DESTINOS: '}</Text>
                    <TextField
                        style={styles.searchInputRedesigned}
                        placeholderTextColor={'black'}
                        placeholder={'Origem'}
                        floatingPlaceholder
                        onChangeText={setSearchOrigem}
                        maxLength={30}
                        value={searchOrigem}
                    />
                    <TextField
                        style={styles.searchInputRedesigned}
                        placeholderTextColor={'black'}
                        placeholder={'Destino'}
                        floatingPlaceholder
                        onChangeText={setSearchDestino}
                        maxLength={30}
                        value={searchDestino}
                    />
                    <TextInputMask
                        style={{...styles.searchInputRedesigned, marginTop: 10,}}
                        type={'datetime'}
                        placeholder={'Data de embarque (a partir de)'}
                        placeholderTextColor={'gray'}
                        options={{
                            format: 'DD/MM/YYYY'
                        }}
                        value={searchData}
                        onChangeText={(date) => {
                            setSearchData(date)
                        }}
                        returnKeyType={'done'}
                    />
                </View>
                {
                    !data.length && !loading &&
                    <Text
                        style={{textAlign: 'center'}}>{'Nenhuma passagem encontrada para essa busca. \n Tente novamente com datas ou destinos diferentes.'}</Text>
                }
                {
                    loading &&
                    <Text
                        style={{textAlign: 'center'}}>{'Atualizando lista de passagens e assentos disponíveis...'}</Text>
                }
                <FlatList
                    onRefresh={() => atualizarViagens()}
                    refreshing={loading}
                    keyboardShouldPersistTaps={'always'}
                    style={{maxHeight: 500, paddingBottom: 200}}
                    data={data}
                    renderItem={i => {
                        return (
                            <TouchableOpacity
                                key={i.index}
                                onPress={() => {
                                    // @ts-ignore
                                    navigation.navigate('HomeBase', {screen: 'Confirmacao', params: {item: i.item}})
                                    atualizarPassageiros([])
                                    atualizarQtdPassageiros(0)
                                }
                                }
                                style={{
                                    marginVertical: 5,
                                    borderRadius: 5,
                                    padding: 10,
                                    paddingVertical: 15,
                                    width: '100%',
                                    borderWidth: 1,
                                    borderColor: '#CBCBCB',
                                    backgroundColor: '#FAFAFA',
                                }}>
                                <View
                                    style={{
                                        flexDirection: 'row',
                                        justifyContent: 'space-between',
                                        marginBottom: 5,
                                    }}>
                                    <Text
                                        style={{
                                            color: 'black',
                                            fontSize: 18,
                                        }}>{
                                        format(new Date(i.item.dataPartida), 'dd/MM/yyyy HH:mm')
                                    }</Text>
                                    <Text
                                        style={{
                                            color: Colors.primary,
                                            fontWeight: '500',
                                            fontSize: 20,
                                        }}>
                                        {/*@ts-ignore*/}
                                        {`${MaskService.toMask('money', i.item.preco, {
                                            unit: 'R$',
                                            separator: ',',
                                            delimiter: '.',
                                        })}`}
                                    </Text>
                                </View>
                                <View
                                    style={{
                                        flexDirection: 'row',
                                        marginBottom: 5,
                                    }}>
                                    <Text
                                        style={{
                                            color: 'black',
                                            fontWeight: '500'
                                        }}>{`Origem: ${i.item.origem}`}</Text>
                                </View>
                                <View
                                    style={{
                                        flexDirection: 'row',
                                    }}>
                                    <Text
                                        style={{
                                            color: 'black',
                                            fontWeight: '500'
                                        }}>{`Destino: ${i.item.destino}`}</Text>
                                </View>
                                <View style={{backgroundColor: 'gray', height: 1, marginVertical: 5}}></View>

                                <View
                                    style={{
                                        flexDirection: 'row',
                                        justifyContent: 'space-between',
                                    }}>
                                    <View
                                        style={{
                                            flexDirection: 'row',
                                        }}>
                                        <Text
                                            style={{
                                                color: 'black',
                                                marginTop: 5,
                                            }}>{`Vagas restantes: ${i.item.qtdVagas - i.item.vagasVendidas}`}</Text>
                                    </View>
                                </View>
                            </TouchableOpacity>
                        );
                    }}
                />
            </View>
        </>
    );
};

export default ListaOpcoesPassagens;
