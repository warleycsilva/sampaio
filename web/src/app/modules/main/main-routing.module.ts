import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";

import {ForbiddenComponent} from "../shared/components/forbidden/forbidden.component";
import {ActivateAccountComponent} from "./pages/activate-account/activate-account.component";
import {LoginComponent} from "./pages/login/login.component";
import {
  PasswordRecoverWithTokenComponent
} from "./pages/password-recover-with-token/password-recover-with-token.component";
import {TermAndConditionsComponent} from "./pages/term-and-conditions/term-and-conditions.component";

const routes: Routes = [
  {
    path: "",
    component: LoginComponent,
    pathMatch: "full",
    children: [
      {path: "forbidden", component: ForbiddenComponent},
      {path: "term-and-conditions", component: TermAndConditionsComponent},
    ],
  },
  {
    path: "backoffice",
    loadChildren: () =>
      import("../backoffice/backoffice.module").then((m) => m.BackofficeModule)
  },
  {
    path: "password-recover/:token",
    component: PasswordRecoverWithTokenComponent,
  },
  {path: "activate-account/:token", component: ActivateAccountComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class MainRoutingModule {
}
