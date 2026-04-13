import {Text, TouchableOpacity} from 'react-native';
import React from 'react';
import styles from "./styles";

interface Props {
    onClick(): void;

    text: string
    secondary?: boolean | undefined
    color?: string
}

const AppButton: React.FC<Props> = ({onClick, color, text, secondary = false, ...rest}) => {
    return (
        !secondary ?
            <TouchableOpacity onPress={() => onClick()} style={styles.searchButton}>
                <Text style={styles.searchButtonText}>{text}</Text>
            </TouchableOpacity> :
            <TouchableOpacity onPress={() => onClick()} style={styles.secondaryButton}>
                <Text style={styles.secondaryButtonText}>{text}</Text>
            </TouchableOpacity>
    );
};

export default AppButton;
