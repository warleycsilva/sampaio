import {NgModule} from "@angular/core";
import {BackofficeRoutingModule} from "./backoffice-routing.module";
import {SharedModule} from "../shared/shared.module";
import {HTTP_INTERCEPTORS} from "@angular/common/http";
import {AppHttpInterceptor} from "../shared/app-http.interceptor";
import {BackofficeSideBarComponent} from "./components/backoffice-side-bar/backoffice-side-bar.component";
import {HomeComponent} from "./pages/home/home.component";
import {BackofficeUsersComponent} from "./pages/backoffice-users/backoffice-users.component";
import {HangfireLoginComponent} from "./pages/hangfire-login/hangfire-login.component";
import {NgxMaskModule} from "ngx-mask";
import {ImageCropperModule} from "ngx-image-cropper";
import {NgxCurrencyModule} from "ngx-currency";
import { ViagensComponent } from './pages/viagens/viagens.component';
import { ViagemUpsertComponent } from './pages/viagens/viagem-upsert/viagem-upsert.component';
import { RelatoriosComponent } from './pages/relatorios/relatorios.component';

@NgModule({
  declarations: [
    BackofficeSideBarComponent,
    HomeComponent,
    BackofficeUsersComponent,
    HangfireLoginComponent,
    ViagensComponent,
    ViagemUpsertComponent,
    RelatoriosComponent,
  ],
  imports: [
    SharedModule.forRoot(),
    BackofficeRoutingModule,
    NgxMaskModule,
    ImageCropperModule,
    NgxCurrencyModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AppHttpInterceptor,
      multi: true,
    },
  ],
})
export class BackofficeModule {
}
