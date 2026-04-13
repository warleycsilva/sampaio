import React from 'react';
import {SafeAreaView, View} from 'react-native';
import styles from './Home.style';
import LogoImage from '../../components/LogoImage';
import ListaOpcoesPassagens from "../../components/ListaOpcoesPassagens";

const Home = () => {
    return (
        <>
            <View style={styles.container}>
                <SafeAreaView style={styles.SafeAreaView1}/>
            </View>
            <View style={{height: '95%'}}>
                <LogoImage/>
                <ListaOpcoesPassagens></ListaOpcoesPassagens>
            </View>
        </>
    );
};

export default Home;
