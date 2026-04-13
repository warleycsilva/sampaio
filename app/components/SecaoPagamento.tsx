import Clipboard from '@react-native-clipboard/clipboard';
import React, {useEffect, useRef, useState} from 'react';
import {Platform, ScrollView, Text, TextInput, TouchableOpacity, View} from 'react-native';
import {Colors} from "../utils/consts";
import AppButton from "./AppButton";
import {MaskService} from "react-native-masked-text";
import {comprarViagem, obterUltimaViagemPorEmail} from "../services/viagens";
import {useNavigation} from "@react-navigation/native";
import {Modalize} from "react-native-modalize";
import styles from "./styles";
import {CartaoCredito} from "../types/Viagens";
import {useCarrinho} from "../context/passageirosContext";
import {Modal} from "react-native-ui-lib";
import toMask = MaskService.toMask;

interface Props {
    id: string
    valorPassagem: number

    setPixQrCode(qrCode: string | null): void

    setPaymentCompleted(paid: boolean): void

    modalToggled(opened: boolean): void,
}

export interface Comprador {
    email?: string
    cpf?: string
}

const SecaoPagamento: React.FC<Props> = ({id, setPaymentCompleted, valorPassagem, setPixQrCode, modalToggled}) => {
    const [qrCode, setQrCode] = useState<string | null>()
    const [error, setError] = useState<string | undefined>()
    const [payType, setPayType] = useState<'pix' | 'card' | undefined>()
    const [paid, setPaid] = useState<boolean>(false)
    const [loading, setLoading] = useState<boolean>(false)
    const [modalIsOpened, setModalIsOpened] = useState<boolean>(false)
    const [creditCard, setCreditCard] = useState<CartaoCredito | undefined>()
    const navigation = useNavigation()
    const [comprador, setComprador] = useState<Comprador | undefined>();
    const modalRef = useRef<Modalize>(null);

    const {qtdPassageiros, passageiros, atualizarPassageiros, atualizarQtdPassageiros} = useCarrinho();

    useEffect(() => {
        modalIsOpened ? modalRef?.current?.open() : modalRef.current?.close()
        modalToggled(modalIsOpened)
    }, [modalIsOpened])

    function pay(type: 'pix' | 'card') {
        efetuarCompra(type)
    }

    function payCard() {
        setLoading(true)
        comprarViagem(id, {
            cpf: comprador?.cpf,
            email: comprador?.email,
            tipoPagamento: 'Credit',
            passageiros: passageiros.map((p, i) => {
                return {
                    assento: p.assento,
                    nome: p.nome,
                    documento: p.documento,
                    telefone: p.telefone,
                    comprador: i == 0
                }
            }),
            cartaoCredito: creditCard
        }).then((result) => {
            console.log({result})
            if (result?.data.status == "Approved") {
                toast?.show(result.data.message)
                setPaid(true)
                setPaymentCompleted(true)
                atualizarPassageiros([])
                atualizarQtdPassageiros(0)
                setError("")
            }
            if (result?.data.status == "Failed") {
                setError(result?.data?.message);
            }
        })
            .catch((e) => {
                toast?.show("Falha no pagamento", {type: "warning"})
            })
            .finally(() => setLoading(false))
    }

    function efetuarCompra(type: 'pix' | 'card') {
        if (!passageiros.length) {
            toast?.show('Favor adicionar no mínimo 1 passageiro.', {type: 'warning'})
            return
        }
        if (!comprador?.email) {
            toast?.show("Preencha o E-mail.", {type: 'warning '})
            return;
        }
        if (!comprador?.cpf) {
            toast?.show("Preencha o CPF.", {type: 'warning'})
            return
        }
        setPayType(type)
        if (type == 'pix') {
            comprarViagem(id, {
                cpf: comprador?.cpf,
                email: comprador?.email,
                tipoPagamento: 'Pix',
                passageiros: passageiros.map((p, i) => {
                    return {
                        assento: p.assento,
                        nome: p.nome,
                        documento: p.documento,
                        telefone: p.telefone,
                        comprador: i == 0
                    }
                }),
            }).then((result) => {
                console.log(result)
                if (result?.success) {
                    toast?.show(result.data.message)
                    setQrCode(result.data.pixQrCode)
                    setPixQrCode(result.data.pixQrCode)
                }
            })
        } else if (type == 'card') {
            setModalIsOpened(true)
        } else {
            toast?.show("Verifique os dados informados do cartão e tente novamente.", {type: 'warning '})
            return;
        }
    }

    function updatePaymentStatus() {
        if (!comprador?.email) {
            toast?.show("Preencha o E-mail.", {type: 'warning '})
            return;
        }
        obterUltimaViagemPorEmail(comprador?.email).then((result) => {
            console.log(result)
            if (result.pagamentoStatus == "Pending") {
                toast?.show("Seu pagamento ainda não foi identificado, pode levar alguns minutos. Avisaremos por e-mail quando o pagamento for aprovado.", {type: 'warning'})
            } else if (result.pagamentoStatus == "Approved") {
                toast?.show("Pagamento aprovado! Verifique seu e-mail.", {type: 'success'})
                setPaid(true)
                setPaymentCompleted(true)
            } else {
                toast?.show("Não foi identificado o pagamento.", {type: 'warning'})
            }
        })
    }

    return (
        <View>
            {!qrCode && !paid &&
                <>
                    <Text style={{
                        fontSize: 18,
                        marginVertical: 10,
                        fontWeight: 'bold',
                        color: Colors.primary
                    }}>{'Forma de pagamento:'}</Text>
                    <View style={{flexDirection: 'row', justifyContent: 'space-between', paddingHorizontal: 10}}>
                        <View style={{justifyContent: 'center', alignItems: 'flex-start'}}>
                            <Text style={{fontSize: 18, fontWeight: '300'}}>{'Passageiros: '}</Text>
                            <Text style={{fontSize: 18, fontWeight: '300'}}>{'Valor da passagem: '}</Text>
                            <Text style={{fontSize: 20, fontWeight: '500', marginTop: 10}}>{'Valor a pagar: '}</Text>
                        </View>
                        <View style={{justifyContent: 'center', alignItems: 'flex-end'}}>
                            <Text style={{fontSize: 18, fontWeight: '300'}}>{qtdPassageiros}</Text>
                            <Text style={{fontSize: 18, fontWeight: '300'}}>{toMask('money', String(valorPassagem))}</Text>
                            <Text style={{
                                fontSize: 20,
                                fontWeight: '500',
                                marginTop: 10
                            }}>{toMask('money', String(qtdPassageiros * valorPassagem))}</Text>
                        </View>
                    </View>
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
                        keyboardType={'email-address'}
                        textContentType={'emailAddress'}
                        placeholder={'E-mail'}
                        placeholderTextColor={Colors.gray}
                        value={comprador?.email}
                        onChangeText={(t) => setComprador({...comprador, email: t})}></TextInput>
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
                        keyboardType={'numeric'}
                        placeholder={'CPF (Somente números)'}
                        maxLength={11}
                        placeholderTextColor={Colors.gray}
                        value={comprador?.cpf?.toString()}
                        onChangeText={(t) => setComprador({...comprador, cpf: (String)(t)})}></TextInput>
                    <View style={{flexDirection: 'row', justifyContent: 'space-evenly'}}>
                        <TouchableOpacity
                            style={{
                                flex: 1,
                                borderWidth: 1,
                                borderColor: Colors.primary,
                                backgroundColor: 'white',
                                borderRadius: 10,
                                marginVertical: 5,
                                marginHorizontal: 5,
                                padding: 15,
                                justifyContent: 'center'
                            }}
                            onPress={() => {
                                pay('card')
                            }}>
                            <Text style={{
                                color: Colors.primary,
                                fontWeight: 'bold',
                                fontSize: 18,
                                textAlign: 'center'
                            }}>{'Cartão de crédito'}</Text>
                        </TouchableOpacity>
                        <TouchableOpacity
                            style={{
                                flex: 1,
                                borderWidth: 1,
                                borderColor: Colors.primary,
                                backgroundColor: 'white',
                                borderRadius: 10,
                                marginVertical: 5,
                                marginHorizontal: 5,
                                padding: 15,
                                justifyContent: 'center'
                            }}
                            onPress={() => {
                                pay('pix')
                            }}>
                            <Text style={{
                                color: Colors.primary,
                                fontWeight: 'bold',
                                fontSize: 18,
                                textAlign: 'center'
                            }}>{'PIX'}</Text>
                        </TouchableOpacity>
                    </View>
                    <Modal visible={modalIsOpened} onBackgroundPress={() => console.log('background pressed')}>
                        <ScrollView
                            keyboardShouldPersistTaps={'always'}
                            style={{
                                backgroundColor: 'white',
                                flex: 1,
                            }}>
                            <Text style={styles.textoTitulo}>{'Cartão de crédito: '}</Text>
                            <View
                                style={{
                                    marginHorizontal: 10,
                                    marginVertical: 5,
                                    paddingHorizontal: 10
                                }}>
                                <Text>Dados do titular</Text>
                            </View>
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
                                placeholder={'Titular do cartão'}
                                placeholderTextColor={Colors.gray}
                                value={creditCard?.nomeTitularCartao}
                                textContentType={'name'}
                                keyboardType={'default'}
                                returnKeyType={'next'}
                                onChangeText={(t) => setCreditCard({...creditCard, nomeTitularCartao: t})}/>
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
                                maxLength={11}
                                returnKeyType={'next'}
                                placeholder={'CPF do titular do cartão'}
                                placeholderTextColor={Colors.gray}
                                value={creditCard?.docTitularCartao}
                                keyboardType={'numeric'}
                                onChangeText={(t) => setCreditCard({...creditCard, docTitularCartao: t})}/>
                            <View
                                style={{
                                    marginHorizontal: 10,
                                    marginVertical: 5,
                                    paddingHorizontal: 10
                                }}>
                                <Text>Dados do cartão</Text>
                            </View>
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
                                placeholder={'Número do cartão'}
                                placeholderTextColor={Colors.gray}
                                value={creditCard?.numeroCartao}
                                textContentType={'creditCardNumber'}
                                keyboardType={'numeric'}
                                maxLength={16}
                                onChangeText={(t) => setCreditCard({...creditCard, numeroCartao: t})}/>
                            <View style={{flexDirection: 'row', justifyContent: 'space-between'}}>
                                <TextInput
                                    style={{
                                        flex: 1,
                                        height: 40,
                                        borderRadius: 10,
                                        borderWidth: 1,
                                        borderColor: Colors.primary,
                                        marginHorizontal: 20,
                                        marginVertical: 5,
                                        paddingHorizontal: 10
                                    }}
                                    returnKeyType={'next'}
                                    placeholder={'Mês de expiração'}
                                    placeholderTextColor={Colors.gray}
                                    value={creditCard?.mesExpiracao ? String(creditCard.mesExpiracao) : ''}
                                    keyboardType={'numeric'}
                                    maxLength={2}
                                    onChangeText={(t) => setCreditCard({...creditCard, mesExpiracao: t ? Number(t) : undefined})}/>
                                <TextInput
                                    style={{
                                        flex: 1,
                                        height: 40,
                                        borderRadius: 10,
                                        borderWidth: 1,
                                        borderColor: Colors.primary,
                                        marginHorizontal: 20,
                                        marginVertical: 5,
                                        paddingHorizontal: 10
                                    }}
                                    returnKeyType={'next'}
                                    placeholder={'Ano de expiração'}
                                    placeholderTextColor={Colors.gray}
                                    value={creditCard?.anoExpiracao ? String(creditCard.anoExpiracao) : ''}
                                    keyboardType={'numeric'}
                                    maxLength={2}
                                    onChangeText={(t) => setCreditCard({...creditCard, anoExpiracao: t ? Number(t) : undefined})}/>
                            </View>
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
                                placeholder={'Código de segurança'}
                                placeholderTextColor={Colors.gray}
                                value={creditCard?.codigoSegurancaCartao}
                                keyboardType={'numeric'}
                                maxLength={3}
                                onChangeText={(t) => setCreditCard({...creditCard, codigoSegurancaCartao: t})}/>
                            <View
                                style={{
                                    marginHorizontal: 10,
                                    marginVertical: 5,
                                    paddingHorizontal: 10
                                }}>
                                <Text>Endereço do titular do cartão</Text>
                            </View>
                            <View style={{
                                flexDirection: 'row',
                                marginHorizontal: 20,
                                justifyContent: 'space-between'
                            }}>
                                <TextInput
                                    style={{
                                        flex: 3,
                                        height: 40,
                                        borderRadius: 10,
                                        borderWidth: 1,
                                        borderColor: Colors.primary,
                                        marginHorizontal: 5,
                                        marginVertical: 5,
                                        paddingHorizontal: 10
                                    }}
                                    returnKeyType={'next'}
                                    placeholder={'CEP (Só números)'}
                                    placeholderTextColor={Colors.gray}
                                    value={creditCard?.enderecoCepCartao}
                                    keyboardType={'numeric'}
                                    maxLength={8}
                                    onChangeText={(t) => setCreditCard({...creditCard, enderecoCepCartao: t})}/>
                                <TextInput
                                    style={{
                                        flex: 2,
                                        height: 40,
                                        borderRadius: 10,
                                        borderWidth: 1,
                                        borderColor: Colors.primary,
                                        marginHorizontal: 5,
                                        marginVertical: 5,
                                        paddingHorizontal: 10
                                    }}
                                    placeholder={'UF (Estado)'}
                                    placeholderTextColor={Colors.gray}
                                    value={creditCard?.enderecoEstadoCartao}
                                    maxLength={2}
                                    returnKeyType={'next'}
                                    onChangeText={(t) => setCreditCard({...creditCard, enderecoEstadoCartao: t})}/>
                            </View>
                            <View style={{
                                flexDirection: 'row',
                                marginHorizontal: 20,
                                justifyContent: 'space-between'
                            }}>
                                <TextInput
                                    style={{
                                        flex: 3,
                                        height: 40,
                                        borderRadius: 10,
                                        borderWidth: 1,
                                        borderColor: Colors.primary,
                                        marginHorizontal: 5,
                                        marginVertical: 5,
                                        paddingHorizontal: 10
                                    }}
                                    returnKeyType={'next'}
                                    placeholder={'Rua'}
                                    placeholderTextColor={Colors.gray}
                                    value={creditCard?.enderecoRuaCartao}
                                    onChangeText={(t) => setCreditCard({...creditCard, enderecoRuaCartao: t})}/>
                                <TextInput
                                    style={{
                                        flex: 2,
                                        height: 40,
                                        borderRadius: 10,
                                        borderWidth: 1,
                                        borderColor: Colors.primary,
                                        marginHorizontal: 5,
                                        marginVertical: 5,
                                        paddingHorizontal: 10
                                    }}
                                    returnKeyType={'next'}
                                    placeholder={'Número'}
                                    placeholderTextColor={Colors.gray}
                                    value={creditCard?.enderecoNumeroCartao}
                                    onChangeText={(t) => setCreditCard({...creditCard, enderecoNumeroCartao: t})}/>
                            </View>
                            <View style={{
                                flexDirection: 'row',
                                marginHorizontal: 20,
                                justifyContent: 'space-between'
                            }}>
                                <TextInput
                                    style={{
                                        flex: 3,
                                        height: 40,
                                        borderRadius: 10,
                                        borderWidth: 1,
                                        borderColor: Colors.primary,
                                        marginHorizontal: 5,
                                        marginVertical: 5,
                                        paddingHorizontal: 10
                                    }}
                                    returnKeyType={'next'}
                                    placeholder={'Cidade'}
                                    placeholderTextColor={Colors.gray}
                                    value={creditCard?.enderecoCidadeCartao}
                                    onChangeText={(t) => setCreditCard({...creditCard, enderecoCidadeCartao: t})}/>
                                <TextInput
                                    style={{
                                        flex: 3,
                                        height: 40,
                                        borderRadius: 10,
                                        borderWidth: 1,
                                        borderColor: Colors.primary,
                                        marginHorizontal: 5,
                                        marginVertical: 5,
                                        paddingHorizontal: 10
                                    }}
                                    returnKeyType={'next'}
                                    placeholder={'Bairro'}
                                    placeholderTextColor={Colors.gray}
                                    value={creditCard?.enderecoBairroCartao}
                                    onChangeText={(t) => setCreditCard({...creditCard, enderecoBairroCartao: t})}/>
                            </View>
                            {!!error && <Text style={{
                                textAlign: 'left',
                                color: 'red',
                                marginTop: 10,
                                marginHorizontal: '5%',
                                borderWidth: 1,
                                borderColor: 'red',
                                padding: 5,
                                borderRadius: 5
                            }}>{error}</Text>}
                            <AppButton text={loading ? 'Carregando...' : 'Enviar'}
                                       onClick={() => {
                                           if (loading) return;
                                           if (
                                               !creditCard?.codigoSegurancaCartao
                                               && !creditCard?.nomeTitularCartao
                                               && !creditCard?.numeroCartao
                                               && !creditCard?.mesExpiracao
                                               && !creditCard?.anoExpiracao
                                               && !creditCard?.docTitularCartao
                                               && !creditCard?.enderecoCepCartao
                                               && !creditCard?.enderecoEstadoCartao
                                               && !creditCard?.enderecoBairroCartao
                                               && !creditCard?.enderecoRuaCartao
                                               && !creditCard?.enderecoNumeroCartao
                                               && !creditCard?.enderecoCidadeCartao
                                           ) {
                                               setCreditCard({...creditCard, valido: false})
                                               toast?.show("Verifique o cartão informado e tente novamente");
                                               return
                                           }
                                           setCreditCard({...creditCard, valido: true})
                                           setPayType('card')
                                           payCard()
                                       }}></AppButton>
                            <AppButton text={'Cancelar'}
                                       secondary
                                       onClick={() => {
                                           setPayType(undefined)
                                           setModalIsOpened(false)
                                       }}></AppButton>

                        </ScrollView>
                    </Modal>
                    <Modalize ref={modalRef}
                              withOverlay={true}
                              keyboardAvoidingBehavior={Platform.OS === 'ios' ? 'padding' : 'height'}
                              withHandle={true}
                              modalStyle={{
                                  paddingHorizontal: 10,
                                  paddingTop: '5%',
                                  width: 1,
                                  height: 1,
                                  borderWidth: 1
                              }}
                    >
                    </Modalize>
                </>
            }
            {!!qrCode && !paid &&
                <View style={{flexDirection: 'column', justifyContent: 'center'}}>
                    <Text style={{
                        fontSize: 18,
                        marginVertical: 10,
                        fontWeight: 'bold',
                        color: Colors.primary
                    }}>{'QR Code para pagamento:'}</Text>
                    <Text style={{paddingHorizontal: 30, textAlign: 'center'}}>Abra o app do seu banco e use o seguinte
                        código para pagar:</Text>
                    <Text>{qrCode}</Text>
                    <AppButton text={'Copiar'} onClick={() => Clipboard.setString(qrCode)
                    }></AppButton>
                    <Text style={{paddingHorizontal: 30, textAlign: 'center'}}>Após o pagamento você receberá um
                        comprovante no e-mail informado ({comprador?.email}).</Text>
                    <AppButton text={'Atualizar status'} onClick={() => updatePaymentStatus()
                    }></AppButton>
                    <AppButton text={'Voltar'}
                               secondary
                               onClick={() => {
                                   navigation.goBack()
                                   setPixQrCode(null)
                                   setQrCode(null)
                               }}></AppButton>
                </View>}
            {paid &&
                <View style={{flexDirection: 'column', justifyContent: 'center'}}>
                    <Text style={{
                        fontSize: 18,
                        marginVertical: 10,
                        fontWeight: 'bold',
                        textAlign: 'center',
                        color: Colors.success
                    }}>{'Pagamento aprovado!'}</Text>
                    <Text style={{paddingHorizontal: 30, textAlign: 'center'}}>
                        Foi enviado um comprovante para seu e-mail!</Text>
                    <Text style={{paddingHorizontal: 30, textAlign: 'center'}}>{'Obrigado por viajar conosco!'}</Text>
                    <AppButton text={'Voltar'}
                               secondary
                               onClick={() => {
                                   navigation.goBack()
                                   setPixQrCode(null)
                                   setQrCode(null)
                                   setPaid(false)
                                   setPaymentCompleted(false)
                                   setCreditCard(undefined)
                               }}></AppButton>
                </View>}
        </View>
    );
};

export default SecaoPagamento;
