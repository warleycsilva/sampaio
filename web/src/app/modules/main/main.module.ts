import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {NgModel} from "@angular/forms";
import {NgModule} from "@angular/core";
import {HTTP_INTERCEPTORS} from "@angular/common/http";

import {MainRoutingModule} from "./main-routing.module";

import {LoginComponent} from "./pages/login/login.component";
import {AppHttpInterceptor} from "../shared/app-http.interceptor";
import {MainComponent} from "./main.component";
import {SharedModule} from "../shared/shared.module";
import {MainSideBarComponent} from "./components/main-side-bar/main-side-bar.component";
import {
  PasswordRecoverWithTokenComponent
} from "./pages/password-recover-with-token/password-recover-with-token.component";
import {ActivateAccountComponent} from "./pages/activate-account/activate-account.component";
import {ChangePasswordComponent} from "./pages/change-password/change-password.component";
import {TermAndConditionsComponent} from "./pages/term-and-conditions/term-and-conditions.component";
import {NgxMaskModule} from "ngx-mask";
import {ImageCropperModule} from "ngx-image-cropper";

@NgModule({
  declarations: [
    MainComponent,
    LoginComponent,
    MainSideBarComponent,
    PasswordRecoverWithTokenComponent,
    ActivateAccountComponent,
    ChangePasswordComponent,
    TermAndConditionsComponent,
  ],
  imports: [
    NgxMaskModule.forRoot(),
    BrowserAnimationsModule,
    SharedModule.forRoot(),
    MainRoutingModule,
    ImageCropperModule,
  ],
  providers: [
    NgModel,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AppHttpInterceptor,
      multi: true,
    },
  ],
  bootstrap: [MainComponent],
})
export class MainModule {
}
