import {Text, TextInput, TouchableOpacity, View} from 'react-native';
import React, {useState} from 'react';
import styles from './styles';
import {Colors} from '../utils/consts';
import DatePicker from "react-native-date-picker";
import {  format } from 'date-fns'
interface Props {
    label: string;
    isOrigin?: boolean | undefined;
    value?: Date | undefined;
    setValue(v: Date): void;
}

const InputBuscaData: React.FC<Props> = ({label, isOrigin, value, setValue}) => {
    const [open, setOpen] = useState(false)
    const placeholder = `Data de ${isOrigin ? 'ida' : 'volta'}`
    const confirmText = `Confirmar`
    const cancelText = `Cancelar`
    return (
        <View
            style={styles.inputContainer}>
            <Text style={styles.inputLabel}>{label}</Text>
            <TouchableOpacity
                onPress={() => setOpen(true)}
                style={styles.dateButton}>
                <TextInput
                    editable={false}
                    value={value ? format(value, 'dd/MM/yyyy') : undefined}
                    onChangeText={t => setValue(new Date(t))}
                    style={styles.textInputLugar}
                    placeholder={placeholder}
                    placeholderTextColor={Colors.gray}
                />
            </TouchableOpacity>
            <DatePicker
                modal
                minimumDate={new Date()}
                mode={'date'}
                locale={'pt-br'}
                open={open}
                title={placeholder}
                confirmText={confirmText}
                cancelText={cancelText}
                theme={'light'}
                textColor={Colors.primary}
                date={value ?? new Date()}
                onConfirm={(date) => {
                    setOpen(false)
                    setValue(date)
                }}
                onCancel={() => {
                    setOpen(false)
                }}
            />
        </View>
    );
};

export default InputBuscaData;
