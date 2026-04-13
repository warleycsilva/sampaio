import {Text, View} from 'react-native';
import React from 'react';
import {Colors} from "../utils/consts";
import {format} from "date-fns";
import {FontAwesome} from "@expo/vector-icons";
import {Card} from 'react-native-ui-lib';

interface Props {
    item: any
}

const CardViagemCarrinho: React.FC<Props> = ({item}) => {
    return (
        <View style={{width: '100%'}}>
            <Text style={{
                fontSize: 18,
                marginBottom: 10,
                fontWeight: 'bold',
                color: Colors.primary
            }}>{'Dados da viagem:'}</Text>
            <Card padding-card marginB-s4>
                <View style={{justifyContent: 'space-between', flexDirection: 'row', marginTop: 5}}>
                    <Text style={{color: Colors.black, fontSize: 16}}>Origem: </Text>
                </View>
                <View style={{justifyContent: 'space-between', flexDirection: 'row'}}>
                    <Text style={{color: Colors.black, fontSize: 16}}>{item.origem}</Text>
                </View>
                <View style={{justifyContent: 'space-between', flexDirection: 'row', marginTop: 10}}>
                    <Text style={{color: Colors.black, fontSize: 16}}>Destino: </Text>
                </View>
                <View style={{justifyContent: 'space-between', flexDirection: 'row'}}>
                    <Text style={{color: Colors.black, fontSize: 16}}>{item.destino}</Text>
                </View>
                <View style={{width: '100%', backgroundColor: 'black', height: 1, marginVertical: 4}}></View>
                <View style={{justifyContent: 'flex-start', flexDirection: 'row', marginTop: 5}}>
                    <Text style={{
                        fontSize: 16,
                    }}>Embarque: </Text>
                    <Text style={{
                        fontWeight: 'bold',
                        fontSize: 16,
                    }}>{format(new Date(item.dataPartida), 'dd/MM/yyyy')} {format(new Date(item.dataPartida), 'HH:mm')}hs</Text>
                </View>
                <View style={{justifyContent: 'space-between', flexDirection: 'row', marginTop: 5}}>
                    <Text style={{
                        fontWeight: 'bold',
                    }}>Observações: </Text>
                </View>
                <View style={{justifyContent: 'space-between', flexDirection: 'row', marginTop: 5}}>
                    <Text style={{
                    }}>{item.observacoes} </Text>
                </View>
            </Card>
        </View>
    );
};

export default CardViagemCarrinho;
