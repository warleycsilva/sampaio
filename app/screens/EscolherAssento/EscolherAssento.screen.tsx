import React from 'react';
import {SafeAreaView, StatusBar, View} from 'react-native';
import styles from '../Home/Home.style';
import EscolhaAssentoComponente from '../../components/EscolhaAssento';

const noop = () => {};

const EscolherAssento = () => {
    return (
        <>
            <View style={styles.container}>
                <StatusBar barStyle="dark-content" backgroundColor={'#f9f9f9'}/>
                <SafeAreaView style={styles.SafeAreaView1}/>
                <SafeAreaView style={styles.SafeAreaView2}/>
            </View>
            <SafeAreaView style={styles.ListScreenContainerView}>
                <EscolhaAssentoComponente
                    assentoSelecionado={noop}
                    atual={undefined}
                    ocupados={[]}
                />
            </SafeAreaView>
        </>
    );
};

export default EscolherAssento;
