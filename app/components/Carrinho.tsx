import {ScrollView, Text, TouchableOpacity, View,} from 'react-native';
import React, {useEffect, useState} from 'react';
import styles from './styles';
import CardViagemCarrinho from "./CardViagemCarrinho";
import SecaoPassageirosCarrinho from "./SecaoPassageirosCarrinho";
import SecaoPagamento from "./SecaoPagamento";
import {Viagem} from "../types/Viagens";
import {getViagensById} from "../services/viagens";
import {useNavigation} from "@react-navigation/native";

interface Props {
    item: Viagem
}

const Carrinho: React.FC<Props> = ({item}) => {
    const [pixQrCode, setPixQrCode] = useState<string | null>()
    const [viagem, setViagem] = useState<Viagem>(item)
    const [paid, setPaid] = useState<boolean>(false)
    const [escolhendoAssentos, setEscolhendoAssentos] = useState<boolean>(false)
    const [modalPassageiros, setModalPassageiros] = useState<boolean>(false)
    const [modalPagamento, setModalPagamento] = useState<boolean>(false)
    const navigation = useNavigation()

    async function getViagem() {
        getViagensById(item.id).then(r => {
            setViagem(r)
        })
    }

    useEffect(() => {
        getViagem();
    }, [item.id]);

    return (
        <ScrollView contentContainerStyle={{padding: 15, minHeight: escolhendoAssentos ? '260%' : modalPagamento ? '120%' :'90%'}}>
            <View>
                <Text style={styles.textoTitulo}>{'RESUMO DA COMPRA: '}</Text>
            </View>
            <>
                {!modalPassageiros && !modalPagamento ? <CardViagemCarrinho item={viagem}></CardViagemCarrinho> : <></>}
                {!pixQrCode && !paid &&
                    <SecaoPassageirosCarrinho assentosOcupados={viagem.passageiros.map(x => x.assento)}
                                              escolhaAssentoToggled={(v) => setEscolhendoAssentos(v)}
                                              escolhaModalToggled={(v) => {
                                                  setModalPassageiros(v)
                                                  getViagem()
                                              }}
                    ></SecaoPassageirosCarrinho>
                }
                {!modalPassageiros ? <SecaoPagamento setPixQrCode={(code) => setPixQrCode(code)}
                                                setPaymentCompleted={setPaid}
                                                id={viagem.id}
                                                valorPassagem={viagem.preco}
                                                modalToggled={(v)=> setModalPagamento(v)}></SecaoPagamento> : <></>}
            </>
            <View>
                <TouchableOpacity onPress={() =>
                    navigation.goBack()
                }>
                    <Text style={{textAlign: 'center', textDecorationStyle: 'solid', textDecorationLine: 'underline', marginTop: 20}}>Voltar ao início</Text>
                </TouchableOpacity>
            </View>
        </ScrollView>
    );
};

export default Carrinho;

