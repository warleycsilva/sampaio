import {Text, View} from 'react-native';
import React, {useState} from 'react';
import styles from './styles';
import {Colors} from '../utils/consts';

import SectionedMultiSelect from 'react-native-sectioned-multi-select';
import {Place} from "../types/Places";

import Ionicons from '@expo/vector-icons/Ionicons';

interface Props {
    isOrigin?: boolean | undefined;
    label: string;
    places: Place[];
}

const InputBuscaLugar: React.FC<Props> = ({label, places, isOrigin}) => {
    const [place, setPlace] = useState<Place | undefined>();
    const placeholder = `Escolha ${isOrigin ? 'a origem' : 'o destino'}`
    return (
        <View style={styles.inputContainer}>
            <Text style={styles.inputLabel}>{label}</Text>
            <View style={styles.inputBorder}>
                <SectionedMultiSelect
                    IconRenderer={<Ionicons name="checkmark-circle" size={32} color="green"/>}
                    styles={{
                        separator: {
                            backgroundColor: Colors.secondary
                        },
                        button: {
                            backgroundColor: Colors.success
                        },
                        container: {
                            maxHeight: '80%',
                        },
                    }}
                    items={places}
                    uniqueKey="id"
                    subKey="children"
                    searchPlaceholderText={placeholder}
                    selectText={place?.name ?? placeholder}
                    showDropDowns
                    single
                    selectedItems={[place?.name]}
                    onSelectedItemsChange={t => {
                        setPlace(places.filter(p => p.id === t[0])[0]);
                    }}
                />
            </View>
        </View>
    );
};

export default InputBuscaLugar;
