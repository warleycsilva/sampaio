import { StyleSheet } from 'react-native';
import { Colors } from '../../utils/consts';

export default StyleSheet.create({
  container: {
    flexGrow: 1,
    backgroundColor: Colors.primary,
  },
  SafeAreaView1: {
    backgroundColor: Colors.secondary,
    flex: 1,
    borderBottomRightRadius: 500,
    borderBottomWidth: 5,
    borderRightWidth: 80,
    borderBottomColor: Colors.alternative,
    borderRightColor: Colors.alternative,
  },
  SafeAreaView2: { flex: 1, backgroundColor: Colors.primary },
  ScreenContainerView: {
    backgroundColor: Colors.white,
    position: 'absolute',
    alignSelf: 'center',
    marginVertical: '20%',
    minWidth: '90%',
    marginHorizontal: '5%',
    minHeight: '80%',
    maxHeight: '85%',
    borderRadius: 5,
    shadowColor: Colors.black,
    elevation: 80,
    opacity: 0.9,
    justifyContent: 'flex-start',
    alignItems: 'center'
  },
  ListScreenContainerView: {
    backgroundColor: Colors.white,
    position: 'absolute',
    alignSelf: 'center',
    minWidth: '100%',
    marginHorizontal: '5%',
    height: '100%',
    padding: '5%',
    borderRadius: 5,
    shadowColor: Colors.black,
    elevation: 80,
    opacity: 0.9,
    justifyContent: 'flex-start',
    alignItems: 'center'
  },
  outerWrapper: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FFF',
  },
  buttonStyle: {
    backgroundColor: '#EEE',
    paddingHorizontal: 40,
    paddingVertical: 30,
    borderWidth: 0.5,
    borderColor: '#F0F0F0',
    borderRadius: 10,
  },
  text: { fontSize: 18, color: '#808080', fontWeight: 'bold' },

});
