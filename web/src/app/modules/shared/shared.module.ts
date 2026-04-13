import {LOCALE_ID, ModuleWithProviders, NgModule} from "@angular/core";
import {CommonModule, DatePipe, registerLocaleData} from "@angular/common";
import {FormsModule, NgModel, ReactiveFormsModule} from "@angular/forms";
import {RouterModule} from "@angular/router";
import {HttpClientModule} from "@angular/common/http";
import {NgSelectModule} from "@ng-select/ng-select";
import {NgxChameleonModule} from "ngx-chameleon";
import {NgxCurrencyModule} from "ngx-currency";
import {NgxSpinnerModule} from "ngx-spinner";
import {ToastrModule} from "ngx-toastr";
import {NgxPagedListModule} from "ngx-paged-list";
import {ImageCropperModule} from "ngx-image-cropper";
import localePtBR from "@angular/common/locales/pt";
import {NgxMaskModule} from "ngx-mask";
import {CKEditorModule} from "@ckeditor/ckeditor5-angular";
import {DragDropModule} from "@angular/cdk/drag-drop";
import {NgxQRCodeModule} from "@techiediaries/ngx-qrcode";
import {GoogleMapsModule} from "@angular/google-maps";
import {GooglePlaceModule} from "ngx-google-places-autocomplete";

import {DateValueAccessor} from "src/app/modules/shared/directives/date-value-accessor.directive";
import {NoActionHrefDirective} from "src/app/modules/shared/directives/no-action-href.directive";
import {ScrollTopDirective} from "src/app/modules/shared/directives/scroll-top.directive";
import {GridLoadingComponent} from "./components/grid-loading/grid-loading.component";
import {GridPaginationComponent} from "./components/grid-pagination/grid-pagination.component";
import {BaseComponent} from "./components/base/base.component";
import {DisableControlDirective} from "./directives/disable-control.directive";
import {LoginLayoutComponent} from "./components/login-layout/login-layout.component";
import {SafeUrlPipe} from "./pipes/safe-url.pipe";
import {RemoveClassOnClickDirective} from "./directives/remove-class-on-click-directive.directive";
import {ImageUploadComponent} from "./components/image-upload/image-upload.component";
import {LimitToPipe} from "./pipes/limit-to.pipe";
import {FilterPipe} from "./pipes/filter.pipe";
import {ForbiddenComponent} from "./components/forbidden/forbidden.component";
import {PhoneMaskDirective} from "./directives/phone-mask.directive";
import {MessageUserBlockedComponent} from "./components/message-user-blocked/message-user-blocked.component";
import {MessageWarningComponent} from "./components/message-warning/message-warning.component";
import {InputAutocompleteComponent} from './components/input-autocomplete/input-autocomplete.component';
import {ModalBlockUserComponent} from "./components/modal-block-user/modal-block-user.component";

registerLocaleData(localePtBR);

@NgModule({
  declarations: [
    DateValueAccessor,
    NoActionHrefDirective,
    ScrollTopDirective,
    DisableControlDirective,
    RemoveClassOnClickDirective,
    PhoneMaskDirective,

    SafeUrlPipe,
    LimitToPipe,
    FilterPipe,

    BaseComponent,
    GridLoadingComponent,
    GridPaginationComponent,
    LoginLayoutComponent,
    ImageUploadComponent,
    ForbiddenComponent,

    MessageUserBlockedComponent,
    MessageWarningComponent,

    InputAutocompleteComponent,
    ModalBlockUserComponent,
  ],
  imports: [
    GoogleMapsModule,
    DragDropModule,
    CKEditorModule,
    NgxMaskModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule,
    NgxChameleonModule,
    NgxCurrencyModule,
    NgxSpinnerModule,
    ToastrModule.forRoot({
      enableHtml: true,
      progressBar: true,
      timeOut: 2500,
      positionClass: "toast-top-center",
    }),
    NgxPagedListModule,
    ImageCropperModule,
    NgSelectModule,
    NgxQRCodeModule,
    GooglePlaceModule,
  ],
  exports: [
    GoogleMapsModule,
    CKEditorModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule,
    NgxChameleonModule,
    NgxCurrencyModule,
    NgxSpinnerModule,
    ToastrModule,
    NgxPagedListModule,
    NgxQRCodeModule,
    GooglePlaceModule,
    DateValueAccessor,
    NoActionHrefDirective,
    ScrollTopDirective,
    DisableControlDirective,
    RemoveClassOnClickDirective,
    PhoneMaskDirective,

    SafeUrlPipe,
    LimitToPipe,
    FilterPipe,

    BaseComponent,
    GridLoadingComponent,
    GridPaginationComponent,
    LoginLayoutComponent,
    ImageUploadComponent,
    ForbiddenComponent,
    NgSelectModule,
    NgxMaskModule,
    MessageUserBlockedComponent,
    MessageWarningComponent,

    InputAutocompleteComponent,
    ModalBlockUserComponent,
  ],
})
export class SharedModule {
  static forRoot(): ModuleWithProviders<SharedModule> {
    return {
      ngModule: SharedModule,
      providers: [
        DatePipe,
        NgModel,
        {
          provide: LOCALE_ID,
          useValue: "pt",
        },
      ],
    };
  }
}
