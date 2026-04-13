import {AfterViewInit, Component, OnInit, ViewChild} from "@angular/core";
import {environment} from "src/environments/environment";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {finalize, takeWhile} from "rxjs/operators";
import {Router} from "@angular/router";
import {BaseComponent} from "src/app/modules/shared/components/base/base.component";
import {AppService} from "src/app/modules/shared/services/app.service";
import {AuthService} from "src/app/modules/shared/services/auth.service";
import {ModalComponent} from "ngx-chameleon";

declare const $: any;

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent
  extends BaseComponent
  implements OnInit, AfterViewInit {
  constructor(
    private router: Router,
    private appService: AppService,
    private authService: AuthService,
  ) {
    super();
  }

  @ViewChild("selectUserTypeModal", {static: true})
  selectUserTypeModal: ModalComponent;

  submited = false;

  forgotPass: boolean;

  signUp = false;

  userTypes: any = environment.userTypes;

  form = new FormGroup({
    email: new FormControl(
      "",
      Validators.compose([Validators.required, Validators.email])
    ),
    password: new FormControl("", Validators.required),
    passwordConfirmation: new FormControl("", Validators.required),
    firstName: new FormControl("", Validators.required),
    lastName: new FormControl(""),
    type: new FormControl("Renter", Validators.required),
  });

  ngAfterViewInit() {
    $(".ui.modal").modal();
  }

  ngOnInit() {
    const user = this.appService.sessionUser;
    if (user) {
      this.handleUserType(user);
    }
    this.form.get("passwordConfirmation").disable();
    this.form.get("firstName").disable();
    this.form.get("lastName").disable();
    this.form.get("type").disable();
  }

  handleUserType(user) {
    if (user.type == 'Backoffice') {
      this.router.navigate(["/backoffice"]);
    }
  }

  handleSignUp() {
    this.signUp = !this.signUp;
    if (!this.signUp) {
      this.form.get("passwordConfirmation").disable();
      this.form.get("firstName").disable();
      this.form.get("lastName").disable();
      this.form.get("type").disable();
    } else {
      this.form.get("passwordConfirmation").enable();
      this.form.get("firstName").enable();
      this.form.get("lastName").enable();
      this.form.get("type").enable();
    }
  }

  handleFunction() {
    this.forgotPass = !this.forgotPass;
    if (this.forgotPass) {
      this.form.get("password").disable();
    } else {
      this.form.get("password").enable();
    }
  }

  send() {
    this.submited = true;
    if (this.form.invalid) {
      return;
    }

    this.appService.spinner.show();

    if (this.signUp) {
      this.authService
        .signUp(this.form.value)
        .pipe(
          takeWhile(() => this.isAlive),
          finalize(() => this.appService.spinner.hide())
        )
        .subscribe((resp) => {
          if (resp.data.success) {
            this.appService.toastr.success(
              "Verifique seu e-mail para ativar a conta",
              resp.data.message
            );
            this.signUp = false;
            this.doLogin();
          } else {
            this.appService.toastr.error(resp.data.message);
          }
        });
    } else if (this.forgotPass) {
      this.authService
        .remember(this.form.value)
        .pipe(
          takeWhile(() => this.isAlive),
          finalize(() => this.appService.spinner.hide())
        )
        .subscribe((resp) => {
          if (resp.data.success) {
            this.appService.toastr.success(resp.data.message);
            return;
          } else {
            this.appService.toastr.error(resp.data.message);
          }
        });
    } else {
      this.doLogin();
    }
  }

  doLogin() {
    this.form.get("type").enable();
    this.form.get("type").setValue("Backoffice");

    this.authService
      .login(this.form.value)
      .pipe(
        takeWhile(() => this.isAlive),
        finalize(() => this.appService.spinner.hide())
      )
      .subscribe((resp) => {
        if (!resp.success) {
          this.appService.toastr.error(resp.error);
          return;
        }
        if (resp.user.type === "Lessee") {
          this.appService.toastr.error(
            "Este módulo não está disponível para motoristas. Por favor, baixe o aplicativo para continuar."
          );
        } else {
          this.appService.storeAcsessToken(resp.accessToken);
          this.appService.storeUser(resp.user);
          this.handleUserType(resp.user);
        }
      });
  }

  handleEstablishment() {
    this.router.navigate(["/establishment"]);
  }

  closeModal(redirect) {
    this.selectUserTypeModal.close();
    if (redirect) this.router.navigate([""]);
  }
}
