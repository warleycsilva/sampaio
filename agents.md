# agents.md — Mapa Mental do Projeto Sampaio Turismo

> Documento técnico completo para agentes de IA e desenvolvedores entrando no projeto.
> Cobre cada pasta, arquivo relevante, sua responsabilidade, suas dependências e como ele se encaixa no sistema.

---

## Índice

1. [Monorepo — Raiz](#1-monorepo--raiz)
2. [app/ — Mobile (React Native + Expo)](#2-app--mobile-react-native--expo)
3. [backend/ — API (.NET 7)](#3-backend--api-net-7)
4. [web/ — Backoffice (Angular 13)](#4-web--backoffice-angular-13)
5. [Contratos de API](#5-contratos-de-api)
6. [Banco de Dados](#6-banco-de-dados)
7. [Pagamentos](#7-pagamentos)
8. [CI/CD e Deploy](#8-cicd-e-deploy)
9. [Variáveis de Ambiente e Secrets](#9-variáveis-de-ambiente-e-secrets)
10. [Decisões de Arquitetura e Dívidas Técnicas](#10-decisões-de-arquitetura-e-dívidas-técnicas)

---

## 1. Monorepo — Raiz

```
sampaio/
├── app/
├── backend/
├── web/
├── README.md       ← visão geral do projeto
└── agents.md       ← este arquivo
```

Não há tooling de monorepo (Nx, Turborepo, etc.). As três aplicações são pastas independentes com seus próprios `package.json` / `.csproj`. Não há dependências cruzadas via código — a comunicação entre as partes é exclusivamente via HTTP.

---

## 2. app/ — Mobile (React Native + Expo)

**Tech:** React Native 0.74.5 · Expo SDK 51 · TypeScript 5.3.3 · Yarn 1.22.22 · Node 20.11.1

### 2.1 Arquivos de Configuração

| Arquivo | O que faz |
|---------|-----------|
| `app.json` | Config do Expo: nome do app, ícones, splash screen, bundle IDs (`com.sampaioturapp` iOS, `com.wcssilva.sampaioturapp` Android), versão 25, orientação portrait |
| `eas.json` | Profiles de build EAS: `development` (dev client), `preview` (internal), `production` (aab/ipa). Project ID: `217219ba-2035-4544-9704-c3b53f167c84` |
| `package.json` | Dependências e scripts. Scripts principais: `start`, `start:clean`, `build-prod-android`, `build-prod-ios`, `submit-ios` |
| `tsconfig.json` | Strict mode habilitado, extends `expo/tsconfig.base` |
| `babel.config.js` | Preset `babel-preset-expo` + plugin `react-native-reanimated` (deve ser o último plugin) |
| `RELEASE_NOTES.md` | Histórico de releases. Versão atual: v25 |

### 2.2 Ponto de Entrada

**`App.tsx`** — raiz da aplicação. Responsabilidades:
- Inicializa o sistema de design via `react-native-ui-lib` (Colors, Typography, Spacing com os valores de `utils/consts`)
- Registra a ref global de Toast (`setRef`) para notificações acessíveis em qualquer parte do app
- Renderiza `<Navigation />` como componente raiz

### 2.3 Navegação

**`navigation/navigation.tsx`**

Usa `@react-navigation/native-stack` + `@react-navigation/bottom-tabs`.

```
Bottom Tab Navigator
├── Tab: Pesquisa    → screen: Home
├── Tab: Viagens     → screen: ListaViagens
├── Tab: Assento     → screen: EscolherAssento
├── Tab: Pagamento   → screen: PagamentoTela
├── Tab: Confirmação → screen: Confirmacao
└── Tab: Cadastro    → screen: Registrar
```

> A navegação é linear e reflete o funil de compra. Cada tab corresponde a uma etapa. A navegação entre steps é programática (não pelo usuário tocando nas tabs), mas as tabs ficam visíveis o tempo todo.

### 2.4 Screens

Localização: `screens/`

| Screen | Arquivo | O que renderiza |
|--------|---------|-----------------|
| **Home** | `Home/index.tsx` + `Home.style.tsx` | Formulário de busca: origem, destino, data. Chama `getViagens` e navega para ListaViagens |
| **ListaViagens** | `ListaViagens/index.tsx` | Lista de viagens disponíveis recebidas como parâmetro de rota. Usa `<ListaOpcoesPassagens />` |
| **EscolherAssento** | `EscolherAssento/index.tsx` | Mapa de assentos do ônibus. Usa `<EscolhaAssento />` que contém `<FileiraComponente />` e `<AssentoComponente />` |
| **PagamentoTela** | `PagamentoTela/index.tsx` | Formulário de pagamento. Usa `<PagamentoComponente />` e `<SecaoPagamento />` |
| **Confirmacao** | `Confirmacao/index.tsx` | Tela pós-compra com resumo e status |
| **Registrar** | `Registrar/index.tsx` | Cadastro de conta do passageiro |

### 2.5 Components

Localização: `components/`

| Componente | Arquivo | Responsabilidade |
|-----------|---------|-----------------|
| `AppButton` | `AppButton/` | Botão padrão com estilos do design system |
| `AssentoComponente` | `AssentoComponente/` | Renderiza um assento individual: livre, ocupado, selecionado. Recebe `numero`, `ocupado`, `selecionado`, callback `onPress` |
| `CardViagemCarrinho` | `CardViagemCarrinho/` | Card resumo de uma viagem no carrinho |
| `Carrinho` | `Carrinho/` | Modal com resumo de todos os itens selecionados |
| `EscolhaAssento` | `EscolhaAssento/` | Orquestra o mapa de assentos: recebe lista de assentos ocupados/disponíveis, renderiza `<FileiraComponente />` |
| `FileiraComponente` | `FileiraComponente/` | Renderiza uma fileira do ônibus (normalmente 4 assentos por fileira) |
| `HorizontalNumberSpinner` | `HorizontalNumberSpinner/` | Input numérico com botões +/- para quantidade de passagens |
| `InputBuscaData` | `InputBuscaData/` | Campo de data usando `@react-native-community/datetimepicker` |
| `InputBuscaLugar` | `InputBuscaLugar/` | Campo de texto para origem/destino com sugestões |
| `ListaOpcoesPassagens` | `ListaOpcoesPassagens/` | Lista scrollável de viagens disponíveis, cada item leva à seleção de assento |
| `LogoImage` | `LogoImage/` | Logo da Sampaio Turismo (`assets/logo.png`) |
| `PagamentoComponente` | `PagamentoComponente/` | Container do fluxo de pagamento, orquestra `SecaoPagamento` e `SecaoPassageirosCarrinho` |
| `PesquisaPassagens` | `PesquisaPassagens/` | Formulário de busca reutilizável (origem, destino, data) |
| `SecaoPagamento` | `SecaoPagamento/` | Formulário de forma de pagamento: Pix (exibe QR code) ou Cartão (campos do cartão via Stripe) |
| `SecaoPassageirosCarrinho` | `SecaoPassageirosCarrinho/` | Lista/formulário de dados de cada passageiro |
| `styles.tsx` | `components/styles.tsx` | Estilos compartilhados entre componentes |

### 2.6 Services

Localização: `services/`

**`services/index.ts`** — instância do Axios com:
- `baseURL`: `ApiBaseUrl` (de `utils/consts`)
- Interceptors de request (pode adicionar headers/auth)
- Interceptors de response (tratamento de erros global)

**`services/viagens/index.ts`** — todas as chamadas à API de viagens:

```typescript
getViagens(origem, destino, data)
  → GET /v1/viagens?origem=&destino=&data=

comprarViagem(id, request: ComprarPassagemRequest)
  → POST /v1/viagens/{id}/comprar

obterUltimaViagemPorEmail(email)
  → GET /v1/viagens/by-email/{email}/last
```

**`services/types.ts`** — tipos TypeScript para request/response da API.

### 2.7 Context

**`context/passageirosContext.tsx`** — Context API para estado global do carrinho/passageiros.

Estado mantido:
- Lista de passageiros (`Passageiro[]`)
- Viagem selecionada
- Assentos escolhidos
- Funções para adicionar/remover passageiro

Consumido por: `SecaoPassageirosCarrinho`, `PagamentoComponente`, `Carrinho`.

### 2.8 Types

Localização: `types/`

```
types/
├── Bus/index.ts        # Tipos relacionados ao ônibus (assentos, fileiras)
├── Passageiro/index.ts # Interface Passageiro: nome, documento, telefone, assento, comprador
├── Places/index.ts     # Tipos de lugar/destino
└── Viagens/index.ts    # Interface Viagem: id, origem, destino, dataPartida, preco, assentos
```

### 2.9 Utils

Localização: `utils/`

**`utils/consts/index.ts`** — constantes globais:
```typescript
ApiBaseUrl = 'https://sampaioturismo.azurewebsites.net/api'
// Dev: 'http://10.0.2.2:5050/api'

Colors = {
  primary: '#2D3591',   // azul marinho
  secondary: '#f44336', // vermelho
  alternative: '#FFF200',
  success: '#66bb6a',
  error: '#f44336',
  // ...
}
```

**`utils/interfaces/index.ts`** — interfaces compartilhadas (ex: `IColorPalette`).

**`utils/index.ts`** — funções utilitárias (formatação de data, máscara de CPF, etc.).

### 2.10 Assets

Localização: `assets/`

```
assets/
├── icon.png                          # Ícone do app (1024x1024)
├── splash.png                        # Splash screen
├── adaptive-icon.png                 # Ícone adaptativo Android
├── favicon.png                       # Favicon para web
├── logo.png                          # Logo usada em <LogoImage />
├── atendimento_preferencial.jpg      # Imagem informativa
└── *.ttf (13 fontes)                 # Fonte Ionicons + fontes de ícone do react-native-vector-icons
```

### 2.11 Dependências Chave do App

| Pacote | Versão | Para quê |
|--------|--------|---------|
| `expo` | 51 | Build, runtime, EAS |
| `react-native` | 0.74.5 | Framework base |
| `@react-navigation/native` | — | Navegação |
| `@react-navigation/native-stack` | — | Stack navigator |
| `@react-navigation/bottom-tabs` | — | Tab bar |
| `react-native-ui-lib` | — | Design system / components base |
| `react-native-modalize` | — | Bottom sheet / modal |
| `react-native-reanimated` | — | Animações nativas |
| `@stripe/stripe-react-native` | 0.37.2 | Pagamento cartão |
| `@parse/react-native` + `parse` | — | Backend Parse (integração legada/secundária) |
| `axios` | 1.7.7 | HTTP client |
| `@react-native-async-storage/async-storage` | — | Persistência local |
| `@react-native-community/datetimepicker` | — | Picker de data |
| `react-native-masked-text` | — | Máscaras CPF, telefone |
| `@react-native-picker/picker` | — | Select/dropdown |
| `date-fns` | 2.30.0 | Formatação de datas |
| `qs` | 6.11.0 | Serialização de query strings |

---

## 3. backend/ — API (.NET 7)

**Tech:** ASP.NET Core 7 · Entity Framework Core · MediatR (CQRS) · Hangfire · log4net

### 3.1 Estrutura da Solution

**Arquivo:** `backend/SampaioTur.Api.sln`

```
backend/
├── src/
│   ├── Sampaio.Api/              # 01-Api: entry point, controllers, startup
│   ├── Sampaio.Api.Config/       # 01-Api: configuração de DI e serviços
│   ├── Sampaio.Domain/           # 02-Domain: CQRS, handlers, entidades, regras
│   ├── Sampaio.Shared/           # 02-Domain: DTOs, segurança, utilities
│   ├── Sampaio.SyncServices/     # 02-Domain: jobs de sincronização (Hangfire)
│   ├── Sampaio.Infra/            # 03-Infra: storage (S3, Azure Blob)
│   ├── Sampaio.Data/             # 03-Infra: DbContext, migrations, entity maps
│   ├── Sampaio.Logging/          # 03-Infra: configuração do log4net
│   └── Sampaio.Integrations.Pagarme/ # 04-Integrations: SDK Pagarme
├── Dockerfile
├── azure-pipelines.yml
└── .github/workflows/main_sampaioturismo.yml
```

### 3.2 Entry Point

**`src/Sampaio.Api/Program.cs`**
```csharp
WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
```

**`src/Sampaio.Api/Startup.cs`** — registra todos os serviços:
- EF Core + connection string (SQL Server)
- JWT Authentication
- CORS (permite app e web)
- Compressão de resposta
- MediatR (todos os handlers do Domain)
- Swagger / OpenAPI
- Hangfire (fila de jobs em background)
- Health checks
- Aplica migrations na inicialização
- Registra jobs recorrentes (sincronização de pagamentos)

### 3.3 Controllers

Localização: `src/Sampaio.Api/Controllers/`

#### Controllers Públicos (app mobile)

**`Public/ViagensController.cs`**
```
GET  /api/v1/viagens                    # busca viagens (filtros: origem, destino, data)
GET  /api/v1/viagens/{id}               # detalhe de uma viagem
POST /api/v1/viagens/{id}/comprar       # compra passagem (corpo: passageiros, pagamento)
GET  /api/v1/viagens/by-email/{email}   # viagens de um passageiro
GET  /api/v1/viagens/by-email/{email}/last  # última viagem de um passageiro
```

**`Common/V1/AuthController.cs`** — login/cadastro/recuperação de senha para passageiros.

**`Common/V1/ProfileController.cs`** — perfil do passageiro logado.

**`Signup/V1/SignupController.cs`** — fluxo de criação de conta.

#### Controllers do Backoffice (web admin)

**`Backoffice/V1/ViagensController.cs`**
```
GET    /api/v1/backoffice/viagens            # lista com filtros
GET    /api/v1/backoffice/viagens/{id}       # detalhe
POST   /api/v1/backoffice/viagens            # cria viagem
PUT    /api/v1/backoffice/viagens/{id}       # edita viagem
PATCH  /api/v1/backoffice/viagens/{id}/is-active  # ativa/desativa
POST   /api/v1/backoffice/viagens/{id}/estornar   # estorna pagamento
```

**`Backoffice/V1/AuthController.cs`** — login do admin (retorna JWT).

**`Backoffice/V1/BackofficeUserController.cs`** — CRUD de usuários admin.

### 3.4 Domain Layer (CQRS com MediatR)

Localização: `src/Sampaio.Domain/`

```
Domain/
├── CommandHandlers/   # 18+ handlers: recebem Command, executam lógica, persistem
├── Commands/          # 18+ tipos de comando (um por caso de uso)
├── Queries/           # Query handlers (leitura)
├── Entities/          # Classes de domínio (mapeadas no EF)
├── ViewModels/        # DTOs de resposta da API
├── Validators/        # Validação de input dos Commands
└── Services/          # Serviços de domínio (email, notificação)
```

**Principais Commands e Handlers:**

| Command | Handler | O que faz |
|---------|---------|-----------|
| `CriarViagemCmd` | `ViagemCommandHandler` | Cria nova viagem no banco |
| `AlterarViagemCmd` | `ViagemCommandHandler` | Edita dados de uma viagem |
| `AlterarStatusAtivoViagemCmd` | `ViagemCommandHandler` | Ativa/desativa viagem |
| `ComprarPassagemCmd` | `PaymentCommandHandler` | Processa compra: valida assentos, cria `ViagemPagamento`, chama gateway |
| `EstornarViagemCmd` | `PaymentCommandHandler` | Estorna pagamento via Pagarme/GerenciaNet |
| `SyncStatusPagamentoCommand` | `SyncStatusPagamentoHandler` | Consultado pelo Hangfire: atualiza status de pagamentos pendentes |
| `SignupCmd` | `SignupCommandHandler` | Cria conta de passageiro |

**Fluxo de compra no backend:**
1. Controller recebe POST `/viagens/{id}/comprar`
2. Manda `ComprarPassagemCmd` via MediatR
3. Handler valida: viagem existe, assentos disponíveis, dados dos passageiros
4. Cria `ViagemPagamento` com status `Pending`
5. Chama SDK Pagarme ou GerenciaNet (dependendo do `tipoPagamento`)
6. Atualiza status para `Approved`/`Failed`
7. Dispara e-mail de confirmação (Razor template)
8. Hangfire job (`SyncStatusPagamento`) roda periodicamente para resolver pagamentos que ficaram pendentes

### 3.5 Entidades do Domínio

Localização: `src/Sampaio.Domain/Entities/`

**Viagem**
```
id: Guid
origem: string
destino: string
dataPartida: DateTime
preco: decimal
qtdVagas: int
vagasVendidas: int
assentosDisponiveis: int[]
assentosOcupados: int[]
isActive: bool
viagemPagamentos: ViagemPagamento[]
passageiros: Passageiro[]
```

**ViagemPagamento**
```
id: Guid
viagemId: Guid
quantidadePassagens: int
valorTotal: decimal
idTransacao: string        # ID da transação no Pagarme/GerenciaNet
pagamentoStatus: string    # "Approved" | "Failed" | "Pending"
tipoPagamento: string      # "Pix" | "Credit"
passageiros: Passageiro[]
createdAt: DateTime
```

**Passageiro**
```
id: Guid
viagemId: Guid
viagemPagamentoId: Guid
assento: int
nome: string
documento: string          # CPF
telefone: string
comprador: bool            # true se este passageiro é quem fez o pagamento
email: string
```

**BackofficeUser**
```
id: Guid
email: string
passwordHash: string
isActive: bool
```

> **Nota:** O banco também contém tabelas legadas (`Product`, `Sale`, `SaleItem`, `Cart`, `CartItem`, `City`, `Driver`, `Log`) mapeadas no EF Core. São remanescentes de uma versão anterior (e-commerce genérico). Não são usadas ativamente pela aplicação atual, mas as migrations existem.

### 3.6 Data Layer

**`src/Sampaio.Data/DataContext.cs`** — EF Core DbContext com 20+ `DbSet<>` mapeados.

**`src/Sampaio.Data/Maps/`** — Fluent API de mapeamento para cada entidade (ex: `ViagemMap.cs`, `PassageiroMap.cs`, `ViagemPagamentoMap.cs`).

**Migrations:** em `src/Sampaio.Data/Migrations/`. Para gerar nova migration:
```bash
dotnet ef migrations add NomeDaMigration --project src/Sampaio.Data --startup-project src/Sampaio.Api
dotnet ef database update --project src/Sampaio.Data --startup-project src/Sampaio.Api
```

### 3.7 Infra Layer

**`src/Sampaio.Infra/`** — implementações de infraestrutura:
- `AzureBlobStorage`: upload de arquivos para Azure Blob
- `AwsS3Storage`: upload para S3 (credenciais configuradas em `appsettings.json`)

**`src/Sampaio.Logging/`** — configuração do log4net.

### 3.8 Integrations

**`src/Sampaio.Integrations.Pagarme/`** — wrapper do SDK Pagarme:
- Cliente RestEase configurado
- Modelos de request/response do Pagarme
- Chamado pelo `PaymentCommandHandler`

### 3.9 Background Jobs (Hangfire)

Configurado no `Startup.cs`. Painel acessível em `/jobs` (autenticação própria via `HangfireUserConfig`).

Job recorrente registrado:
- **`SyncStatusPagamento`** — roda periodicamente, consulta status de `ViagemPagamento` com status `Pending` no gateway e atualiza o banco.

### 3.10 Autenticação

- **Passageiros (app):** endpoints públicos ou com auth leve (email/CPF)
- **Admins (web):** JWT gerado pelo `Backoffice/AuthController`. Token armazenado no localStorage do navegador com chave `st.token`. SecretKey hardcoded em `appsettings.json` (ver seção de segurança).

### 3.11 Configurações

**`src/Sampaio.Api/appsettings.json`** — produção. Contém:
```json
{
  "ConnectionStrings": { "DefaultConnection": "Server=sampaio.database.windows.net;Database=sampaio_prod;..." },
  "JwtTokenConfig": { "SecretKey": "3626EB197F8742FC958FA7957FAC0250", "ExpirationSeconds": ... },
  "HangfireUserConfig": { "Username": "hangfireuser", "Password": "..." },
  "GerenciaNetConfig": { "ClientId": "...", "ClientSecret": "...", "Key": "..." },
  "AzureBlobConfig": { ... },
  "AwsS3Config": { ... }
}
```

> **PROBLEMA DE SEGURANÇA:** Secrets de produção estão commitados. Qualquer pessoa com acesso ao repositório tem acesso ao banco de produção.

**`appsettings.Development.json`** / **`appsettings.Staging.json`** — ambientes alternativos.

---

## 4. web/ — Backoffice (Angular 13)

**Tech:** Angular 13.3.0 · TypeScript 4.6.2 · PrimeNG 13 · Angular Material 13 · ngx-toastr · chart.js

### 4.1 Estrutura

```
web/
├── src/
│   ├── main.ts                   # Bootstrap: MainModule
│   ├── app/
│   │   └── modules/
│   │       ├── main/             # Auth (login, signup, recuperação de senha)
│   │       ├── backoffice/       # Dashboard admin
│   │       └── shared/           # Componentes, serviços, guards reutilizáveis
│   ├── environments/
│   │   ├── environment.ts        # Dev
│   │   ├── environment.prod.ts   # Produção
│   │   └── environment.staging.ts
│   └── assets/
├── angular.json
├── tsconfig.json
├── package.json
└── .github/workflows/azure-static-web-apps-lemon-wave-0d6657a10.yml
```

### 4.2 Módulos

#### `modules/main/` — Fluxo de autenticação

| Componente | Responsabilidade |
|-----------|-----------------|
| `login/` | Tela de login. Usa `auth.service.ts` → POST `/v1/backoffice/auth/login` |
| `signup/` | Cadastro de novo admin |
| `forgot-password/` | Recuperação de senha por e-mail |
| `reset-password/` | Redefinição com token |
| `validate-token/` | Validação de token de e-mail |

#### `modules/backoffice/` — Dashboard

**Roteamento:** `backoffice-routing.module.ts` mapeia paths para componentes.

| Componente/Path | Arquivo | O que faz |
|----------------|---------|-----------|
| `home/` | `home.component.ts` | Dashboard inicial com resumo |
| `viagens/` | `viagens.component.ts` | Lista viagens com filtros (origem, destino, data, email, CPF, status). Paginação. Botão para ativar/desativar |
| `viagens/nova` | `viagem-upsert.component.ts` | Formulário para criar nova viagem |
| `viagens/:id/editar` | `viagem-upsert.component.ts` | Formulário de edição (mesmo componente, modo edição) |
| `backoffice-users/` | `backoffice-users.component.ts` | CRUD de usuários admin |
| `relatorios/` | `relatorios.component.ts` | Relatórios e gráficos de vendas |
| `hangfire-login/` | `hangfire-login.component.ts` | Redireciona para painel Hangfire com auth |

#### `modules/shared/` — Reutilizáveis

**Serviços** (`services/`):

| Serviço | O que faz |
|---------|-----------|
| `auth.service.ts` | `login()`, `loginBackoffice()`, `signup()`, `forgotPassword()`, `resetPassword()`. Armazena JWT em localStorage (`st.token`) |
| `backoffice.viagens.service.ts` | `obterPorId()`, `criar()`, `editar()`, `estornar()`, `toggleActivation()` — todas as chamadas ao backoffice da API |
| `app.service.ts` | Estado global: spinner, toastr, `accessToken`, dados do usuário logado. Injetado em quase todos os componentes |
| `profile.service.ts` | Operações de perfil do usuário logado |
| `address.service.ts` | Lookup de endereços (via Google Maps API) |
| `account.service.ts` | Configurações de conta |

**Guards** (`guards/`):
- `auth.guard.ts` — protege rotas que exigem login. Redireciona para `/login` se não autenticado.

**Interceptors** (`interceptors/`):
- HTTP interceptor que injeta o header `Authorization: Bearer <token>` em todas as requests para a API.

**Componentes compartilhados** (`components/`):

| Componente | Para quê |
|-----------|---------|
| `base/` | `BaseComponent` — componente base com lifecycle hooks comuns |
| `grid-pagination/` | Controles de paginação de tabelas |
| `grid-loading/` | Spinner de carregamento de tabelas |
| `image-upload/` | Upload de imagens com preview |
| `input-autocomplete/` | Campo de texto com sugestão de opções |
| `address-information-form/` | Formulário de endereço (rua, cidade, CEP) |
| `login-layout/` | Layout das telas de autenticação |
| `forbidden/` | Tela 403 |
| `message-user-blocked/` | Aviso de conta bloqueada |

### 4.3 Environments

**`src/environments/environment.ts`** (dev):
```typescript
export const environment = {
  production: false,
  colors: { primary: '#2D3591', secondary: '#f44336', ... },
  googleMapsApi: "AIzaSyAaE6yTN2lFSGtYG7-__K2BT97rGPexphA",
  urls: {
    api: { v1: "https://sampaioturismo.azurewebsites.net/api/v1" },
    hangfire: "https://sampaioturismo.azurewebsites.net/jobs",
    web: "https://app.sampaioturismo.com/"
  }
}
```

> Todos os ambientes (dev, staging, prod) apontam para a mesma API de produção. Não há ambiente de staging do backend separado atualmente.

### 4.4 Build

```bash
ng build                                # Dev
ng build --configuration production     # Produção → dist/sampaio
ng build --configuration staging        # Staging
```

---

## 5. Contratos de API

### 5.1 Autenticação

**POST `/api/v1/backoffice/auth/login`**
```json
Request:  { "email": "admin@sampaio.com", "password": "..." }
Response: { "token": "eyJ...", "user": { "id": "...", "email": "..." } }
```

### 5.2 Viagens (Público)

**GET `/api/v1/viagens`**
```
Query params: origem, destino, data (YYYY-MM-DD)
Response: Viagem[]
```

**GET `/api/v1/viagens/{id}`**
```
Response: Viagem (com assentosOcupados, assentosDisponiveis)
```

**POST `/api/v1/viagens/{id}/comprar`**
```json
Request:
{
  "email": "passageiro@email.com",
  "cpf": "12345678901",
  "tipoPagamento": "Pix",  // ou "Credit"
  "cartaoCredito": {        // apenas se tipoPagamento = "Credit"
    "numero": "...",
    "nomeTitular": "...",
    "validade": "...",
    "cvv": "..."
  },
  "passageiros": [
    {
      "nome": "João Silva",
      "documento": "12345678901",
      "telefone": "11999999999",
      "assento": 15,
      "comprador": true
    }
  ]
}

Response:
{
  "idTransacao": "...",
  "pagamentoStatus": "Approved",
  "pixQrCode": "...",    // se Pix
  "valorTotal": 150.00
}
```

### 5.3 Viagens (Backoffice)

**POST `/api/v1/backoffice/viagens`** — cria viagem
```json
{
  "origem": "São Paulo",
  "destino": "Campinas",
  "dataPartida": "2025-12-01T08:00:00",
  "preco": 75.00,
  "qtdVagas": 44
}
```

**PATCH `/api/v1/backoffice/viagens/{id}/is-active`** — toggle ativação (sem body)

**POST `/api/v1/backoffice/viagens/{id}/estornar`** — estorna pagamento

---

## 6. Banco de Dados

**Tipo:** Azure SQL Server (não PostgreSQL — apesar do título original do projeto)
**Host:** `sampaio.database.windows.net`
**Database:** `sampaio_prod`
**ORM:** Entity Framework Core com Fluent API

### Tabelas Ativas (core do negócio)

| Tabela | Descrição |
|--------|-----------|
| `Viagens` | Viagens cadastradas pelo admin |
| `ViagemPagamentos` | Uma compra = um pagamento com N passageiros |
| `Passageiros` | Passageiros vinculados a uma compra |
| `BackofficeUsers` | Admins do painel web |
| `Payments` | Registro genérico de transações |

### Tabelas Legadas (não usar)

`Products`, `Sales`, `SaleItems`, `Carts`, `CartItems`, `Cities`, `Drivers`, `Logs` — remanescentes de versão anterior. Ainda mapeadas no EF mas sem uso no código atual.

### Migrations

Localizadas em `src/Sampaio.Data/Migrations/`. Aplicadas automaticamente na inicialização da API (`Startup.cs` chama `context.Database.Migrate()`).

---

## 7. Pagamentos

### Fluxo Pix

```
App → POST /viagens/{id}/comprar (tipoPagamento: "Pix")
    → Backend cria ViagemPagamento (Pending)
    → Chama GerenciaNet API (cert: prod.p12)
    → Recebe QR code e código copia-e-cola
    → Retorna para o app
    → App exibe QR code em <SecaoPagamento />
    → Hangfire job verifica periodicamente se o Pix foi pago
    → Atualiza pagamentoStatus para "Approved"
    → E-mail de confirmação enviado
```

### Fluxo Cartão de Crédito

```
App → <SecaoPagamento /> coleta dados do cartão (via Stripe SDK no app)
    → POST /viagens/{id}/comprar (tipoPagamento: "Credit", cartaoCredito: {...})
    → Backend processa via Pagarme SDK
    → Retorna pagamentoStatus: "Approved" | "Failed"
```

### Certificados GerenciaNet

- `backend/homolog.p12` — certificado de homologação
- `backend/prod.p12` — certificado de produção

Esses arquivos `.p12` precisam estar presentes no servidor em tempo de execução.

---

## 8. CI/CD e Deploy

### App Mobile

**Ferramenta:** EAS (Expo Application Services)

```yaml
# Profiles em app/eas.json
development:
  android: { buildType: "apk" }
  ios: { simulator: true }

preview:
  android: { buildType: "apk" }
  distribution: internal

production:
  android: { buildType: "app-bundle" }  # .aab para Google Play
  ios: {}                                # .ipa para App Store
```

Não há CI automático para o app. Builds são feitos manualmente via `npm run build-prod-android` / `npm run build-prod-ios`.

### Backend

**Arquivo:** `backend/.github/workflows/main_sampaioturismo.yml`

```
Trigger: push em main
Steps:
  1. dotnet restore
  2. dotnet build
  3. dotnet publish -o ./publish
  4. Deploy para Azure Web App: sampaioturismo
Segredo necessário: AZUREAPPSERVICE_PUBLISHPROFILE_*
```

> **Atenção:** O `Dockerfile` usa .NET SDK 3.1 (desatualizado — o projeto roda em .NET 7). O deploy atual é via GitHub Actions, não Docker.

### Web Backoffice

**Arquivo:** `web/.github/workflows/azure-static-web-apps-lemon-wave-0d6657a10.yml`

```
Trigger: push em main, PRs
Steps:
  1. npm install
  2. ng build --configuration production (output: dist/sampaio)
  3. Deploy para Azure Static Web Apps
Segredo necessário: AZURE_STATIC_WEB_APPS_API_TOKEN_LEMON_WAVE_0D6657A10
```

---

## 9. Variáveis de Ambiente e Secrets

### Onde cada secret vive (situação atual — não ideal)

| Secret | Onde está | Como deveria estar |
|--------|-----------|-------------------|
| DB connection string (prod) | `backend/appsettings.json` (commitado) | Azure Key Vault / env var |
| JWT SecretKey | `backend/appsettings.json` (commitado) | Azure Key Vault / env var |
| Hangfire credentials | `backend/appsettings.json` (commitado) | Azure Key Vault / env var |
| GerenciaNet ClientId/Secret | `backend/appsettings.json` (commitado) | Azure Key Vault / env var |
| Google Maps API Key | `web/src/environments/environment.ts` (commitado) | Variável de build / restrição por domínio |
| API URL no app | `app/utils/consts/index.ts` (hardcoded) | `.env` com `expo-constants` |
| Azure deploy token (web) | GitHub Secrets | OK |
| Azure publish profile (backend) | GitHub Secrets | OK |

### Para desenvolvimento local

**App:** Edite `app/utils/consts/index.ts` e descomente a URL local:
```typescript
export const ApiBaseUrl = 'http://10.0.2.2:5050/api'; // emulador Android
// export const ApiBaseUrl = 'http://localhost:5050/api'; // iOS simulator
```

**Backend:** Configure `appsettings.Development.json` com uma string de conexão local ou de staging.

**Web:** Em `src/environments/environment.ts`, altere `urls.api.v1` se necessário.

---

## 10. Decisões de Arquitetura e Dívidas Técnicas

### Decisões

| Decisão | Justificativa |
|---------|--------------|
| CQRS com MediatR no backend | Separação clara entre operações de leitura e escrita. Facilita testes unitários dos handlers. |
| Hangfire para jobs | Jobs de sincronização de pagamento precisam rodar em background e de forma resiliente. |
| Bottom Tab Navigation no app | Reflete o funil linear de compra (busca → lista → assento → pagamento → confirmação). |
| Context API (não Redux/Zustand) | Estado do carrinho é simples e local ao fluxo de compra. Context é suficiente. |
| Angular com PrimeNG no backoffice | Stack madura com componentes ricos para tabelas/forms de admin. |

### Dívidas Técnicas

| Dívida | Impacto | Prioridade sugerida |
|--------|---------|-------------------|
| Secrets em `appsettings.json` no repo | **Crítico** — qualquer acesso ao repo expõe produção | Alta |
| Dockerfile com .NET 3.1 (projeto usa .NET 7) | Build via Docker não funciona | Média |
| Tabelas legadas mapeadas no EF | Migrations desnecessárias, confusão no schema | Baixa |
| Stripe integrado mas não sendo o gateway principal | Conflito com Pagarme/GerenciaNet, biblioteca desnecessária no bundle | Baixa |
| Todos os ambientes (dev/staging/prod) da web apontam para API de produção | Risco de dados de teste contaminarem produção | Média |
| Sem testes automatizados identificados | Nenhum teste unitário/integração encontrado | Alta |
| Parse SDK no app | `@parse/react-native` instalado mas o backend é .NET, não Parse Server. Provavelmente legado. | Baixa |
| EAS build sem CI | Builds de produção do app são manuais | Média |

---

## Guia Rápido: "Onde fica X?"

| O que procuro | Onde está |
|--------------|-----------|
| URL da API | `app/utils/consts/index.ts` → `ApiBaseUrl` |
| Cores do design system | `app/utils/consts/index.ts` → `Colors` |
| Chamadas HTTP do app | `app/services/viagens/index.ts` |
| Estado do carrinho | `app/context/passageirosContext.tsx` |
| Tela inicial (busca) | `app/screens/Home/index.tsx` |
| Mapa de assentos | `app/components/EscolhaAssento/` + `FileiraComponente/` + `AssentoComponente/` |
| Formulário de pagamento | `app/components/SecaoPagamento/` |
| Config de build do app | `app/eas.json` + `app/app.json` |
| Entry point da API | `backend/src/Sampaio.Api/Program.cs` |
| Registro de serviços da API | `backend/src/Sampaio.Api/Startup.cs` |
| Endpoints de compra | `backend/src/Sampaio.Api/Controllers/Public/ViagensController.cs` |
| Lógica de compra | `backend/src/Sampaio.Domain/CommandHandlers/PaymentCommandHandler.cs` |
| Entidade Viagem | `backend/src/Sampaio.Domain/Entities/Viagem.cs` |
| DbContext | `backend/src/Sampaio.Data/DataContext.cs` |
| Integração Pagarme | `backend/src/Sampaio.Integrations.Pagarme/` |
| Jobs Hangfire | `backend/src/Sampaio.SyncServices/` |
| Secrets da API | `backend/src/Sampaio.Api/appsettings.json` ⚠️ |
| Serviço de viagens (web) | `web/src/app/modules/shared/services/backoffice.viagens.service.ts` |
| Tela de viagens (web) | `web/src/app/modules/backoffice/pages/viagens/viagens.component.ts` |
| Autenticação (web) | `web/src/app/modules/shared/services/auth.service.ts` |
| Environment (web) | `web/src/environments/environment.ts` |
| CI/CD backend | `backend/.github/workflows/main_sampaioturismo.yml` |
| CI/CD web | `web/.github/workflows/azure-static-web-apps-lemon-wave-0d6657a10.yml` |
