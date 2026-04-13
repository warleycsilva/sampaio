# Release notes - v25

## Destaques
- Atualização para Expo SDK 51 / React Native 0.74 mantendo compatibilidade com `targetSdkVersion` 35 e `compileSdkVersion` 35 definidos no `app.json`.
- Garantia de builds determinísticos com Node 20.11.1 e Yarn 1.22.22 configurados em `package.json`.
- Fluxos de limpeza de cache e inicialização via `npm run start:clean` para evitar artefatos locais.

## Validações recomendadas
1. **Limpar caches**: `npm run start:clean` executa `watchman watch-del-all` (se disponível) e inicia o Metro com `expo start -c`.
2. **Testes manuais**: abrir o bundler com `npm run start` e validar em dispositivos físicos ou emulators iOS/Android, observando logs do Metro.
3. **Testes automatizados**: não há suíte configurada; considerar adicionar smoke tests para fluxos críticos ao avançar.

## Builds e distribuição
- **EAS Build**: usar `npm run build-prod-android` e `npm run build-prod-ios` para gerar `*.aab`/`*.ipa` com o perfil `production`.
- **Submissões**: publicar via `eas submit -p ios` (ou EAS web) e fluxo padrão do Google Play Console, confirmando que o bundle `targetSdkVersion` 35 atende aos requisitos atuais da Play Store.
- **Permissões**: revisar o uso da câmera listado em `app.json > ios.infoPlist.NSCameraUsageDescription` e validar na App Store Connect/Play Store antes de enviar.

## Observações
- Durante esta iteração não foi possível executar builds EAS ou validar em dispositivos reais/virtuais dentro do ambiente automatizado. Execute os passos acima em uma máquina de integração ou local com as credenciais adequadas antes de publicar.
