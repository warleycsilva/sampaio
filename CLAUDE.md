# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository layout

This is a polyglot monorepo for **Sampaio Turismo** (Brazilian bus-ticket sales platform). Four independent sub-projects live at the root, each with its own toolchain and deployment pipeline — there is **no root-level build, lockfile, or test runner**. Always `cd` into the relevant sub-project before running any command.

| Path | Stack | Role |
|---|---|---|
| `backend/` | .NET 7 / ASP.NET Core, EF Core (SQL Server), MediatR, Hangfire | REST API `sampaioturismo.azurewebsites.net` |
| `web/` | Angular 13, PrimeNG, Angular Material | Backoffice admin SPA |
| `app/` | Expo SDK 55, RN 0.83, React 19.2, `react-native-ui-lib`, Stripe, Parse | Mobile app (legacy, production) |
| `app-v2/` | Expo SDK 55, RN 0.83 | Modernized mobile app (clean architecture rewrite in progress) |

The two mobile apps coexist intentionally — `app-v2` is the in-progress rewrite and is NOT yet a drop-in replacement. Don't delete `app/` or assume parity.

## Commands (per sub-project)

### `backend/` (.NET 7)
```bash
dotnet restore                           # from backend/, uses SampaioTur.Api.sln
dotnet build --configuration Release     # or Debug / Staging (custom config)
dotnet run --project src/Sampaio.Api     # runs on http://localhost:5050
dotnet publish -c Release -o out
```
- Target framework: `net7.0`. Configurations: `Debug | Release | Staging` (note the non-standard `Staging` config defined in every `.csproj`).
- Local env vars live in `src/Sampaio.Api/dev.env` (env-file format: `Section__Key=value`).
- EF Core migrations live in `src/Sampaio.Data/Migrations` and are applied automatically on startup by `AppUseMigrations` (see `Sampaio.Api.Config/Startup/DatabaseConfig.cs`). To add one: `dotnet ef migrations add <Name> --project src/Sampaio.Data --startup-project src/Sampaio.Api`.
- No test project exists in the solution — the `tests/` folder referenced by `Dockerfile` is a leftover (the Dockerfile also references the wrong app name `PayRentee.Api` and is stale; CI/CD uses GitHub Actions, not Docker).
- Deployment: `.github/workflows/main_sampaioturismo.yml` builds + publishes to Azure Web App `sampaioturismo` on push to `main`. A second pipeline `azure-pipelines.yml` (Windows VSBuild) also exists but is the legacy path.

### `web/` (Angular 13)
```bash
yarn install                             # or npm install
npm start                                # ng serve on :4200
npm test                                 # ng test (Karma + Jasmine)
npm run lint                             # ng lint (TSLint — not ESLint)
npm run build-staging                    # → dist/staging
npm run build-prod                       # → dist/prod
```
- Output path for CI is `dist/sampaio` (see `angular.json` and Azure Static Web Apps workflow).
- Environment files: `src/environments/environment{,.staging,.pre-staging,.prod}.ts` swapped via `fileReplacements`.
- Use `npm` commands (not `yarn`) — the scripts are npm-oriented and there is no `yarn.lock`.

### `app/` and `app-v2/` (Expo)
Both apps use **Yarn 1.22.22 + Node 20** (pinned in `package.json` engines and `eas.json`). Same script surface:
```bash
yarn install
yarn start                               # expo start (Metro bundler)
yarn start:clean                         # wipes watchman + Metro cache — use after dep changes
yarn ios / yarn android / yarn web
yarn build-prod-android                  # eas build --profile production --platform android
yarn build-prod-ios
yarn build-prod-all
yarn submit-ios                          # eas submit -p ios
```
- There is no test runner, linter, or type-check script configured. Run `npx tsc --noEmit` manually to type-check.
- EAS project id `217219ba-2035-4544-9704-c3b53f167c84` is shared by both apps (both ship as `com.wcssilva.sampaioturapp` / `com.sampaioturapp`) — do not bump the slug or bundle id casually.
- `app/` bundles Stripe (`@stripe/stripe-react-native`) and Parse (`@parse/react-native`); `app-v2/` deliberately omits both.
- When editing lockfiles or upgrading Expo, always run `yarn start:clean` before testing.

## Architecture

### Backend — Clean Architecture + CQRS (MediatR)

Projects are ordered by solution folders `01-Api → 02-Domain → 03-Infra → 04-Integrations`:

- **`Sampaio.Api`** — ASP.NET Core host. Controllers are thin; they build a request, call `_mediator.Send(...)`, and wrap the result via `BaseApiController.CreateResponse(...)`. Controllers are grouped by audience under `Controllers/{Public,Backoffice,Signup,Common}/V1/`. `ApiExplorerSettings(GroupName = "public:v1" | "backoffice:v1" | ...)` drives Swagger grouping.
- **`Sampaio.Api.Config`** — Startup plumbing split into extension methods (`AppAddDatabase`, `AppAddMediator`, `AppAddAuthorization`, `AppAddHangfire`, …) called from `Startup.ConfigureServices`. Also contains `BaseApiController`, action filters, `SecurityPolices.cs` (sic, note spelling), and the `ValidatorBehavior<,>` MediatR pipeline that runs FluentValidation before handlers.
- **`Sampaio.Domain`** — CQRS core. Pattern: for feature X there is `Commands/X/XCmd.cs`, `CommandHandlers/X/XCommandHandler.cs`, `Queries/X/GetXQuery.cs`, `QueryHandlers/X/XQueryHandler.cs`, plus `Validators/XValidator.cs` and `ViewModels/X/XVm.cs`. Entities live in `Entities/`. Handlers inherit from `BaseCommandHandler` / `BaseQueryHandler` and emit domain errors through `IDomainNotification` instead of throwing.
- **`Sampaio.Data`** — EF Core. `DataContext` + `UnitOfWork` + typed `Repositories/`. Entity mappings in `Maps/` are wired via `MapExtensions`. Migrations auto-apply on startup.
- **`Sampaio.Infra`** — Side effects: `MailService` (SMTP), `AwsS3StorageService` / Azure Blob (`StorageService`), `PushNotificationService` (Firebase), `ViewRenderService` (Razor email templates in `Sampaio.Api/Views/Emails`), `JobService` (Hangfire), `LoggedUser` (reads JWT claims into `SessionUser`).
- **`Sampaio.Integrations.Pagarme`** — Payment gateway wrapper (uses vendored `PagarmeApiSDK.Standard`). GerenciaNet (Pix) integration lives in `src/GerenciaNet`.
- **`Sampaio.Shared`** — Cross-cutting: `EnvelopResult` / `EnvelopDataResult<T>` response envelope, `DomainNotification`, `PagedList<T>`, `StatusCodeDescriptions`, `CommonMessages`, config POCOs (`AppConfig`, `EmailConfig`, `JwtTokenConfig`, `PagarmeConfig`, …).

**Response envelope is mandatory** — never return raw objects from a controller. Use `CreateResponse(data)` / `CreatedResponse(data, url)` / `NotFoundResponse()` / `UnprocessableResponse()` from `BaseApiController`. These inspect `IDomainNotification` and downgrade 2xx → 400 when notifications are present.

**Adding a new endpoint** typically means: command/query class → handler → validator (auto-registered via `AddValidatorsFromAssembly`) → view model → controller method. IoC for domain services is convention-based: `IoCServicesConfig` scans `typeof(PasswordPolicyService).Assembly` for classes in the `Services` namespace that implement an interface, and registers them scoped.

### Web — Angular feature-module layout

`src/app/modules/`:
- `main/` — unauthenticated flows (`login`, `activate-account`, `change-password`, `password-recover-with-token`, `term-and-conditions`).
- `backoffice/` — authenticated admin (`home`, `backoffice-users`, `viagens`, `relatorios`, `hangfire-login`), lazy-loaded via `backoffice-routing.module.ts`.
- `shared/` — `services/` (one per API domain: `auth`, `account`, `backoffice`, `backoffice.viagens`, `profile`, `search`, `graphic`, …), `guards/`, `validators/`, `pipes/`, `directives/`, `enums/`, `models/`, `components/`, plus the global `AppHttpInterceptor` (`app-http.interceptor.ts`) that centralizes HTTP error handling via `toastr`.

Component selector prefix is `main` (set in `angular.json`). The `projects.<name>` key in `angular.json` is still `descarpack-solutions-portal` (inherited from a previous project) — leave it unless doing a deliberate rename, as the build scripts reference it.

### Mobile apps — screen flow

Both apps model the same ticket-purchase funnel:
`Home` (search origin/destination/date) → `ListaViagens` (trip list) → `EscolherAssento` (seat map) → `PagamentoTela` (payment) → `Registrar` (register passenger data) → `Confirmacao` (success).

- `app/` uses a `createBottomTabNavigator` with one tab per screen (atypical — the tab bar is forced to `height: 1` so it's visually hidden). Context provider: `CarrinhoProvider` (`context/passageirosContext.tsx`). Theming: `react-native-ui-lib` (`Colors.loadColors` / `Typography.loadTypographies` in `App.tsx`). Constants live in `utils/consts/index.ts` (including `ApiBaseUrl`).
- `app-v2/` uses a flat `createNativeStackNavigator`. Context provider: `CartProvider` (`src/context/CartContext.tsx`, English names). Theming: custom `src/theme/{colors,spacing,typography}.ts` — no `react-native-ui-lib`. API base URL is hardcoded in `src/api/client.ts`.

**Both apps share a toast-based error pattern**: `App.tsx` attaches the `<Toast>` ref to `global.toast`, and the axios response interceptor (`app/services/index.ts` and `app-v2/src/api/client.ts`) calls `global.toast?.show(...)` with Portuguese messages keyed off HTTP status. When adding API calls, rely on this interceptor for user-visible error feedback — don't re-toast in components for generic errors.

**API surface for mobile** is the `/api/v1/viagens` family in `backend/.../Controllers/Public/ViagensController.cs` (search trips, buy, get by email/cpf, refund). All calls are unauthenticated (`Public` group).

## Conventions

- **Domain terminology is Portuguese**: `Viagem` (trip), `Passageiro` (passenger), `Carrinho` (cart), `Assento` (seat), `Pagamento` (payment), `Estornar` (refund), `Comprar` (buy). Keep these names in backend code and in `app/`. `app-v2/` is progressively translating to English (`Cart`, `Passenger`, `Trip`), but API contracts remain Portuguese.
- **Secrets in `appsettings.json` are placeholders marked `XXXXXXX`** or sandbox keys. The committed `ConnectionStrings.Default` and `JwtTokenConfig.SecretKey` in `backend/src/Sampaio.Api/appsettings.json` point at a prod DB — treat this file as sensitive and never echo its real values into generated code or commits. Prefer populating via `dev.env` locally.
- **No test suites exist** in any sub-project (confirmed by `RELEASE_NOTES.md`). Don't claim a change is "tested" based on non-existent suites; call this out when reporting.
- **Versioning policy for mobile**: `app.json` `version` + `ios.buildNumber` + `android.versionCode` are bumped together. `eas.json` `production.autoIncrement: true` handles the native counters; only bump `version` manually for marketing releases.
- **Committed binaries**: `backend/src/Sampaio.Api/{homolog,prod}.{p12,pem}` are GerenciaNet Pix certificates required at runtime — do not remove them even if they look like secrets.

## Branch & workflow notes

- `.claude/settings.local.json` pre-allows common yarn/npm/expo/tsc/git operations — check it before asking for permission.
- The `web/` and `backend/` projects auto-deploy from `main` via GitHub Actions; be deliberate about what lands there. Mobile ships manually through EAS (`yarn build-prod-*`).
