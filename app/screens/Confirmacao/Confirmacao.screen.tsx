import React from 'react';
import {SafeAreaView, Text, View} from 'react-native';
import {useRoute} from "@react-navigation/native";
import Carrinho from "../../components/Carrinho";
import LogoImage from "../../components/LogoImage";
import styles from '../Home/Home.style';
import {Viagem} from "../../types/Viagens";

type ConfirmacaoRouteParams = {
    item?: Viagem;
};

const Confirmacao = () => {
    const route = useRoute();
    const params = route?.params as ConfirmacaoRouteParams | undefined;
    const selectedTrip = params?.item;

    return (
        <>
            <View style={styles.container}>
                <SafeAreaView style={styles.SafeAreaView1}/>
            </View>
            <View style={{height: '95%'}}>
                <LogoImage/>
                {selectedTrip ? (
                    <Carrinho item={selectedTrip}/>
                ) : (
                    <View style={{paddingHorizontal: 24}}>
                        <Text style={{textAlign: 'center'}}>
                            {"Nenhuma viagem foi selecionada. Volte e escolha uma passagem para continuar."}
                        </Text>
                    </View>
                )}
            </View>
        </>
    );
};

export default Confirmacao;
