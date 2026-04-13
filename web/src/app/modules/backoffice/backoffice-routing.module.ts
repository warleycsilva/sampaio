import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import {BackofficeSideBarComponent} from "./components/backoffice-side-bar/backoffice-side-bar.component";
import {ForbiddenComponent} from "../shared/components/forbidden/forbidden.component";
import {HomeComponent} from "./pages/home/home.component";
import {BackofficeUsersComponent} from "./pages/backoffice-users/backoffice-users.component";
import {HangfireLoginComponent} from "./pages/hangfire-login/hangfire-login.component";

import {LoginBackofficeGuard} from "../shared/guards/login-backoffice.guard";
import {BackofficeVerificationLogedGuard} from "../shared/guards/backoffice-verification-loged.guard";
import {ViagensComponent} from "./pages/viagens/viagens.component";
import {ViagemUpsertComponent} from "./pages/viagens/viagem-upsert/viagem-upsert.component";
import {RelatoriosComponent} from "./pages/relatorios/relatorios.component";

const routes: Routes = [
  {
    path: "",
    component: HomeComponent,
  },
  {path: "home", component: HomeComponent},
  {path: "hangfire-login", component: HangfireLoginComponent},
  {
    path: "",
    component: BackofficeSideBarComponent,
    canActivate: [BackofficeVerificationLogedGuard],
    children: [
      {
        path: "",
        redirectTo: "home",
        pathMatch: "full",
        canActivate: [LoginBackofficeGuard],
      },
      {path: "login", component: HomeComponent},
      {path: "home", component: HomeComponent},
      {path: "forbidden", component: ForbiddenComponent},
      {path: "backoffice-users", component: BackofficeUsersComponent},
      {path: "viagens", component: ViagensComponent},
      {path: "viagens/:id/editar", component: ViagemUpsertComponent},
      {path: "viagens/:id/detalhes", component: ViagemUpsertComponent},
      {path: "viagens/nova", component: ViagemUpsertComponent},
      {path: "relatorios", component: RelatoriosComponent},
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BackofficeRoutingModule {
}
