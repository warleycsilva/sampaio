# Sampaio Turismo

Sistema de venda de passagens rodoviárias composto por três aplicações independentes que se comunicam via API REST.

---

## Visão Geral da Arquitetura

```
┌─────────────────────────────────────────────────────────────────┐
│                        SAMPAIO TURISMO                          │
├──────────────────┬──────────────────┬───────────────────────────┤
│   app/           │   backend/       │   web/                    │
│   Mobile App     │   API REST       │   Backoffice              │
│   React Native   │   .NET 7         │   Angular 13              │
│   Expo SDK 51    │   EF Core        │   PrimeNG                 │
│   iOS + Android  │   Hangfire       │   Admin Dashboard         │
└──────────────────┴────────┬─────────┴───────────────────────────┘
                            │
                 ┌──────────▼──────────┐
                 │   Azure SQL Server  │
                 │   sampaio_prod      │
                 └─────────────────────┘
```

**Usuários finais** acessam o app mobile para buscar viagens, escolher assentos e pagar.
**Operadores/admins** usam o backoffice web para criar viagens, gerenciar usuários e acompanhar vendas.
**O backend** centraliza toda a lógica de negócio, pagamento e persistência.

---

## URLs de Produção

| Serviço | URL |
|---------|-----|
| API Backend | `https://sampaioturismo.azurewebsites.net/api` |
| Swagger (docs) | `https://sampaioturismo.azurewebsites.net/swagger` |
| Hangfire Jobs | `https://sampaioturismo.azurewebsites.net/jobs` |
| Web Backoffice | Azure Static Web Apps (`lemon-wave-0d6657a10`) |

---

## Estrutura de Pastas

```
sampaio/
├── app/          # React Native + Expo — app do passageiro
├── backend/      # ASP.NET Core 7 — API e regras de negócio
├── web/          # Angular 13 — painel administrativo
└── README.md
```

---

## Stack Tecnológica

| Camada | Tecnologia | Versão |
|--------|-----------|--------|
| Mobile | React Native + Expo | 0.74.5 / SDK 51 |
| Mobile runtime | TypeScript | 5.3.3 |
| Backend | ASP.NET Core | .NET 7 |
| ORM | Entity Framework Core | — |
| Jobs | Hangfire | — |
| Banco de dados | Azure SQL Server | sampaio_prod |
| Backoffice | Angular | 13.3.0 |
| CI/CD Mobile | EAS (Expo Application Services) | — |
| CI/CD Backend | GitHub Actions → Azure Web App | — |
| CI/CD Web | GitHub Actions → Azure Static Web Apps | — |

---

## Integrações Externas

| Integração | Onde é usada | Para quê |
|-----------|-------------|---------|
| **Pagarme** | Backend (`Sampaio.Integrations.Pagarme`) | Processamento de pagamentos (Pix e cartão) |
| **GerenciaNet** | Backend (`appsettings.json`) | Pagamentos alternativos (Pix) |
| **Stripe** | App mobile (`@stripe/stripe-react-native`) | Cartão de crédito no app (integrado, uso parcial) |
| **Google Maps** | Web backoffice (`environment.ts`) | Autocomplete de endereços |
| **Azure Blob Storage** | Backend (`Sampaio.Infra`) | Upload de arquivos |
| **AWS S3** | Backend (`Sampaio.Infra`) | Armazenamento alternativo |

---

## Fluxo Principal: Compra de Passagem

```
1. Passageiro busca viagem (origem, destino, data)
        ↓
2. Seleciona assento no ônibus
        ↓
3. Preenche dados dos passageiros
        ↓
4. Escolhe forma de pagamento (Pix / Cartão)
        ↓
5. App envia POST /api/v1/viagens/{id}/comprar
        ↓
6. Backend processa → chama Pagarme/GerenciaNet
        ↓
7. Hangfire job sincroniza status do pagamento
        ↓
8. E-mail de confirmação enviado ao comprador
```

---

## Como Rodar em Desenvolvimento

### App Mobile

```bash
cd app
yarn install
yarn start          # Metro bundler
yarn start:clean    # Limpa cache e reinicia
```

O app consome `http://10.0.2.2:5050/api` em desenvolvimento (emulador Android).
Para apontar para produção, edite `app/utils/consts/index.ts` → `ApiBaseUrl`.

### Backend

```bash
cd backend
dotnet restore
dotnet run --project src/Sampaio.Api
# Roda na porta 5050
```

String de conexão configurada em `appsettings.Development.json`.

### Web Backoffice

```bash
cd web
npm install
ng serve             # http://localhost:4200
ng build             # Build dev
ng build --configuration production  # Build prod
```

---

## Build e Deploy

### App Mobile (EAS)

```bash
cd app
npm run build-prod-android   # Gera .aab para Google Play
npm run build-prod-ios       # Gera .ipa para App Store
npm run submit-ios           # Envia para App Store Connect
```

EAS Project ID: `217219ba-2035-4544-9704-c3b53f167c84`

### Backend

Push para `main` → GitHub Actions (`.github/workflows/main_sampaioturismo.yml`) → Azure Web App `sampaioturismo`.

### Web

Push para `main` → GitHub Actions (`.github/workflows/azure-static-web-apps-lemon-wave-0d6657a10.yml`) → Azure Static Web Apps.

---

## Pontos de Atenção

- **Secrets expostos**: `backend/src/Sampaio.Api/appsettings.json` contém credenciais de produção (DB, JWT, Hangfire, GerenciaNet) em texto plano no repositório. Migrar para Azure Key Vault ou variáveis de ambiente.
- **Chave JWT hardcoded**: `SecretKey: "3626EB197F8742FC958FA7957FAC0250"` no `appsettings.json`.
- **Google Maps API Key** exposta em `web/src/environments/environment.ts`.
- **Dockerfile desatualizado**: imagem base `.NET SDK 3.1`, mas o projeto usa .NET 7.
- **Stripe integrado mas não principal**: a lib está no app porém o fluxo principal usa Pagarme/GerenciaNet.
- **Tabelas legadas no banco**: `Product`, `Sale`, `SaleItem`, `Cart`, `CartItem`, `City`, `Driver` — remanescentes de versão anterior do sistema (e-commerce), ainda mapeadas no EF Core.

---

## Documentação Adicional

- [`agents.md`](./agents.md) — Mapa mental detalhado para agentes e desenvolvedores: todos os arquivos relevantes, suas responsabilidades, contratos de API e decisões de arquitetura.
