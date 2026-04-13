import {KeyboardAvoidingView, Platform, Text, TextInput, TouchableOpacity, View} from 'react-native';
import React, {useRef, useState} from 'react';
import styles from "./styles";
import {Colors} from "../utils/consts";
import {Modalize} from "react-native-modalize";
import AppButton from "./AppButton";
import {PassageiroRequest} from "../types/Viagens";
import {TextInputMask} from "react-native-masked-text";
import EscolhaAssentoComponente from "./EscolhaAssento";
import {useCarrinho} from "../context/passageirosContext";

interface Props {
    assentosOcupados: number[],

    escolhaAssentoToggled(opened: boolean): void,

    escolhaModalToggled(opened: boolean): void,
}

const newPassageiro: PassageiroRequest = {
    comprador: false,
    assento: undefined,
    nome: '',
    documento: '',
    telefone: ''
};
const SecaoPassageirosCarrinho: React.FC<Props> = ({
                                                       assentosOcupados,
                                                       escolhaAssentoToggled,
                                                       escolhaModalToggled
                                                   }) => {
    const [escolhaAssentoAberto, setEscolhaAssentoAberto] = useState<boolean>(false)
   const [passageiroAtual, setPassageiroAtual] = useState<PassageiroRequest>(newPassageiro)
    const modalRef = useRef<Modalize>();

    const {  passageiros, atualizarQtdPassageiros, atualizarPassageiros } = useCarrinho();

    function fecharModal() {
        if (!(passageiroAtual.nome.length >= 2) || !passageiroAtual.nome.includes(' ') || !passageiroAtual.nome) {
            toast?.show('Favor preencher o nome completo', {type: 'warning'});
            return
        }
        if (!passageiroAtual.documento || !(passageiroAtual.documento.length >= 5)) {
            toast?.show('Favor preencher o documento do passageiro', {type: 'warning'});
            return
        }
        if (!passageiroAtual.telefone || !(passageiroAtual.telefone.length >= 13)) {
            toast?.show('Favor preencher o telefone do passageiro', {type: 'warning'});
            return
        }
        if ((!passageiroAtual.assento || passageiroAtual.assento == 0)) {
            toast?.show('Escolha o número do assento', {type: 'warning'});
            return
        }
        if(passageiros.some(p => p.assento == passageiroAtual.assento)){
            toast?.show('O assento escolhido já está ocupado por outro passageiro adicionado.', {type: 'warning'});
            return
        }
        if (!!passageiroAtual.assento && !!passageiroAtual.nome.length && !!passageiroAtual.documento.length && !!passageiroAtual.telefone.length) {

            let newPassageirosList = [...passageiros, passageiroAtual];
            setPassageiroAtual(newPassageiro)
            atualizarQtdPassageiros(newPassageirosList.length)
            atualizarPassageiros(newPassageirosList)
            toast?.show('Passageiro adicionado com sucesso!', {type: 'success'});
            escolhaModalToggled(false)
            modalRef.current?.close()
            return
        }
    }

    function removerPassageiro(numeroAssento: number | undefined) {
        try {
            const novoArrayPassageiros = passageiros.filter((passageiro) => passageiro.assento !== numeroAssento);
            atualizarPassageiros(novoArrayPassageiros)
            atualizarQtdPassageiros(novoArrayPassageiros.length)
            setPassageiroAtual(newPassageiro)
            toast?.show('Passageiro removido com sucesso!', {type: 'success'});
            escolhaModalToggled(false)
            modalRef.current?.close()
        } catch {
            toast?.show('Não foi possível remover o passageiro');
        }
    }

    return (
        <>
            <Modalize
                ref={modalRef}
                withOverlay={true}
                adjustToContentHeight={true}
                withHandle={true}
                closeOnOverlayTap={true}
                modalStyle={{paddingHorizontal: 10, position: 'absolute', width: '100%', minHeight: escolhaAssentoAberto ? '260%' : '100%'}}
            >
                <KeyboardAvoidingView
                    behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
                    style={{
                        backgroundColor: 'white',
                        flex: 1,
                        minHeight: '100%',
                        paddingTop: 30,
                        paddingBottom: 50
                    }}>
                    <Text style={styles.textoTitulo}>{'Adicionar passageiro: '}</Text>
                    <TextInput
                        style={{
                            height: 40,
                            borderRadius: 10,
                            borderWidth: 1,
                            borderColor: Colors.primary,
                            marginHorizontal: 20,
                            marginVertical: 5,
                            paddingHorizontal: 10
                        }}
                        returnKeyType={'next'}
                        keyboardType={'name-phone-pad'}
                        spellCheck={false}
                        textContentType={'name'}
                        placeholder={'Nome completo'}
                        placeholderTextColor={Colors.gray}
                        value={passageiroAtual.nome}
                        onChangeText={(t) => setPassageiroAtual({...passageiroAtual, nome: t})}></TextInput>
                    <TextInput
                        style={{
                            height: 40,
                            borderRadius: 10,
                            borderWidth: 1,
                            borderColor: Colors.primary,
                            marginHorizontal: 20,
                            marginVertical: 5,
                            paddingHorizontal: 10
                        }}
                        keyboardType={'default'}
                        returnKeyType={'next'}
                        placeholder={'Documento (RG)'}
                        spellCheck={false}
                        maxLength={15}
                        placeholderTextColor={Colors.gray}
                        value={passageiroAtual.documento}
                        onChangeText={(t) => setPassageiroAtual({...passageiroAtual, documento: t})}></TextInput>
                    <TextInputMask
                        style={{
                            height: 40,
                            borderRadius: 10,
                            borderWidth: 1,
                            borderColor: Colors.primary,
                            marginHorizontal: 20,
                            marginVertical: 5,
                            paddingHorizontal: 10
                        }}
                        type={'custom'}
                        keyboardType={'numeric'}
                        returnKeyType={'done'}
                        placeholder={'Telefone'}
                        placeholderTextColor={Colors.gray}
                        options={{
                            mask: '(99) 99999-9999'
                        }}
                        value={passageiroAtual.telefone}
                        onChangeText={(t) => setPassageiroAtual({...passageiroAtual, telefone: t})}
                    />
                    {!escolhaAssentoAberto &&
                        <TouchableOpacity
                            style={{
                                height: 40,
                                borderRadius: 10,
                                borderWidth: 1,
                                justifyContent: 'center',
                                borderColor: Colors.primary,
                                marginHorizontal: 20,
                                marginVertical: 5,
                                paddingHorizontal: 10
                            }}
                            onPress={() => {
                                escolhaAssentoToggled(!escolhaAssentoAberto)
                                setEscolhaAssentoAberto(!escolhaAssentoAberto)
                            }}>
                            <Text style={{
                                color: !!passageiroAtual.assento ? 'black' : 'gray'
                            }}>{passageiroAtual.assento ?? 'Escolha um assento'}</Text>
                        </TouchableOpacity>}
                    {escolhaAssentoAberto &&
                        <EscolhaAssentoComponente ocupados={assentosOcupados}
                                                  atual={passageiroAtual.assento}
                                                  assentoSelecionado={(n: number) => {
                                                      escolhaAssentoToggled(!escolhaAssentoAberto)
                                                      setEscolhaAssentoAberto(!escolhaAssentoAberto)
                                                      setPassageiroAtual({...passageiroAtual, assento: n})
                                                  }}></EscolhaAssentoComponente>}
                    <AppButton
                        text={'Adicionar passageiro'}
                        onClick={() => fecharModal()}
                    />
                    <AppButton secondary={true}
                               text={'Cancelar'}
                               onClick={() => {
                                   modalRef.current?.close()
                                   escolhaModalToggled(false)
                                   escolhaAssentoToggled(false)
                                   setEscolhaAssentoAberto(false)
                               }}
                    />
                </KeyboardAvoidingView>
            </Modalize>
            <View >
                <Text style={{
                    fontSize: 20,
                    marginVertical: 10,
                    fontWeight: 'bold',
                    color: Colors.primary
                }}>{'Passageiros:'}</Text>
                {passageiros.map(passageiro =>
                    <View key={passageiro.documento} style={{
                        backgroundColor: 'white', borderRadius: 10, padding: 10, marginVertical: 10,
                        borderWidth: 1,
                        borderColor: Colors.primary,
                        elevation: 5,
                    }}>
                        <View style={{justifyContent: 'space-between', flexDirection: 'row'}}>
                            <Text style={{
                                color: Colors.primary,
                                fontWeight: 'bold',
                                fontSize: 18
                            }}>{passageiro.nome}</Text>
                        </View>
                        <View style={{
                            justifyContent: 'space-between',
                            alignItems: 'center',
                            flexDirection: 'row',
                        }}>
                            <View>
                                <Text style={{
                                    color: Colors.black,
                                    fontSize: 14,
                                    fontWeight: 'bold',
                                    marginVertical: 10,
                                }}>{`Assento: `}</Text>
                                <Text style={{
                                    justifyContent: 'center',
                                    alignItems: 'center',
                                    color: Colors.white,
                                    textAlign: 'center',
                                    textAlignVertical: 'center',
                                    fontSize: 20,
                                    borderWidth: 1,
                                    height: 40,
                                    width: 40,
                                    borderRadius: 10,
                                    backgroundColor: Colors.primary
                                }}>{`${passageiro.assento}`}</Text>
                            </View>
                            <View>
                                <Text style={{
                                    color: Colors.black,
                                    fontSize: 16,
                                    padding: 7
                                }}>{`Documento: ${passageiro.documento}`}</Text>
                                <Text style={{
                                    color: Colors.black,
                                    fontSize: 16,
                                    padding: 7
                                }}>{`Telefone: ${passageiro.telefone}`}</Text>
                            </View>
                            <TouchableOpacity onPress={() => removerPassageiro(passageiro.assento)} style={{width: 25}}>
                                <Text style={{color: 'red', fontSize: 25}}>X</Text>
                            </TouchableOpacity>
                        </View>
                    </View>)}
                <TouchableOpacity
                    style={{
                        backgroundColor: 'white',
                        borderWidth: 1,
                        borderColor: Colors.primary,
                        borderRadius: 10,
                        marginVertical: 5,
                        padding: 10,
                        justifyContent: 'center'
                    }}
                    onPress={() => {
                        modalRef.current?.open()
                        escolhaModalToggled(true)
                    }}>
                    <Text style={{
                        color: Colors.primary,
                        fontWeight: 'bold',
                        fontSize: 18,
                        textAlign: 'center'
                    }}>{'Adicionar passageiro'}</Text>
                </TouchableOpacity>
            </View>
        </>
    );
};

export default SecaoPassageirosCarrinho;