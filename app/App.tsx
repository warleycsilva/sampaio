import 'react-native-gesture-handler';
import React from "react";
import MainNavigation from "./navigation/navigation";
import {Platform} from "react-native";
import {GestureHandlerRootView} from 'react-native-gesture-handler';
import Toast from 'react-native-toast-notifications';
import {Colors, Spacings, Typography} from 'react-native-ui-lib';
import {CarrinhoProvider} from "./context/passageirosContext";

export default function App() {

    Colors.loadColors({
        primaryColor: '#2D3591',
        secondaryColor: '#f44336',
        textColor: '#221D23',
        errorColor: '#E63B2E',
        successColor: '#ADC76F',
        warnColor: '#FF963C'
    });

    Typography.loadTypographies({
        heading: {fontSize: 36, fontWeight: '600'},
        subheading: {fontSize: 28, fontWeight: '500'},
        body: {fontSize: 18, fontWeight: '400'}
    });

    Spacings.loadSpacings({
        page: 20,
        card: 12,
        gridGutter: 16
    });

    return (
        <GestureHandlerRootView style={{flex: 1}}>
            <CarrinhoProvider>
                <>
                    <Toast
                        ref={ref => (global.toast = ref)}
                        placement="top"
                        style={{
                            marginTop: Platform.OS === 'ios' ? 40 : 30,
                            zIndex: 9999,
                        }}
                        textStyle={{fontSize: 16}}
                        successColor={Colors.success}
                        dangerColor={Colors.error}
                        warningColor={Colors.warn}
                    />
                    <MainNavigation/>
                </>
            </CarrinhoProvider>
        </GestureHandlerRootView>
    );
}
