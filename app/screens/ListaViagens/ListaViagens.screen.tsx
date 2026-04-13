import React from 'react';
import {SafeAreaView} from 'react-native';
import styles from '../Home/Home.style';
import ListaOpcoesPassagens from "../../components/ListaOpcoesPassagens";

const ListaViagens = ({navigation}: any) => {
    return (
        <>
            <SafeAreaView style={styles.ListScreenContainerView}>
                <ListaOpcoesPassagens/>
            </SafeAreaView>
        </>
    );
};

export default ListaViagens;
