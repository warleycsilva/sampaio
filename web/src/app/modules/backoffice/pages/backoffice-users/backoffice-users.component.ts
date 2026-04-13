import { Component, OnInit, ViewChild } from "@angular/core";
import { BaseComponent } from "../../../shared/components/base/base.component";
import {
  ModalComponent,
  TopBarBreadcrumbService,
  AlertService,
} from "ngx-chameleon";
import { AppService } from "../../../shared/services/app.service";
import { BackofficeService } from "../../../shared/services/backoffice.service";
import { AuthService } from "../../../shared/services/auth.service";
import { PagedListService } from "ngx-paged-list";
import { environment } from "../../../../../environments/environment";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { finalize, takeWhile } from "rxjs/operators";

@Component({
  selector: "main-backoffice-users",
  templateUrl: "./backoffice-users.component.html",
  styleUrls: ["./backoffice-users.component.css"],
})
export class BackofficeUsersComponent extends BaseComponent implements OnInit {
  @ViewChild("modalNewUser", { static: true }) modalNewUser: ModalComponent;
  @ViewChild("modalBlockUser", { static: true })
  modalBlockUser: ModalComponent;
  @ViewChild("modalBlockedReason", { static: true })
  modalBlockedReason: ModalComponent;

  blockedReasonSelected: string | null = null;

  grid: PagedListService;
  form = new FormGroup({
    email: new FormControl(
      "",
      Validators.compose([Validators.required, Validators.email])
    ),
    password: new FormControl("", Validators.required),
    passwordConfirmation: new FormControl("", Validators.required),
    firstName: new FormControl("", Validators.required),
    lastName: new FormControl(""),
  });
  submitted = false;

  idBackofficeUserBlock = null;
  submittedBlock = false;
  formBlock = new FormGroup({
    blockReason: new FormControl(null, Validators.required),
  });

  constructor(
    private topBarBreadcrumbService: TopBarBreadcrumbService,
    private appService: AppService,
    private backofficeService: BackofficeService,
    private authService: AuthService,
    private alertService: AlertService
  ) {
    super();

    this.grid = new PagedListService({
      isAlive: this.isAlive,
      onLoadStarts: () => this.appService.spinner.show(),
      onLoadFinished: () => this.appService.spinner.hide(),
      pageSize: 20,
      sortField: "CreatedAt",
      sortType: "desc",
      url: `${environment.urls.api.v1}/backoffice/backoffice-users`,
    });
  }

  ngOnInit(): void {
    this.topBarBreadcrumbService.emmiter.next({
      icon: "fas fa-users fa-icon",
      title: "Lista de usuários Backoffice",
      path: ["Lista de usuários Backoffice"],
    });
    this.grid.load();
  }

  send() {
    this.backofficeService
      .createBackofficeUser(this.form.value)
      .pipe(
        takeWhile(() => this.isAlive),
        finalize(() => this.appService.spinner.hide())
      )
      .subscribe((resp) => {
        this.appService.toastr.success(resp.data.message, "Atenção");
        this.grid.load();
        this.modalNewUser.close();
      });
  }

  openCreateUserModal() {
    this.modalNewUser.open({
      closable: true,
      isScrolling: true,
      title: "Adicionar novo usuário",
      showCloseBtn: true,
      size: "medium",
    });
  }

  openModalFormBlock(id: string, name: string) {
    this.idBackofficeUserBlock = id;
    this.modalBlockUser.open({
      closable: true,
      isScrolling: true,
      title: `Deseja realmente bloquear usuário? ${name}`,
      showCloseBtn: true,
      size: "small",
    });
  }

  blockBackofficeUser() {
    this.submittedBlock = true;

    if (this.formBlock.invalid) return;

    this.appService.spinner.show();
    this.backofficeService
      .blockBackofficeUser(this.idBackofficeUserBlock, {
        blockReason: this.formBlock.value.blockReason,
      })
      .pipe(
        takeWhile(() => this.isAlive),
        finalize(() => this.appService.spinner.hide())
      )
      .subscribe((resp) => {
        this.formBlock.get("blockReason").setValue(null);
        this.idBackofficeUserBlock = null;
        this.modalBlockUser.close();
        this.appService.toastr.success(resp.data.message, "Atenção");
        this.grid.load();
      });
  }

  unBlockBackofficeUser(id: string) {
    this.alertService.confirm({
      headerTitle: "Atenção",
      msg: "Deseja realmente desbloquear este usuário?",
      cancelText: "Cancelar",
      cancelBtnColor: "red",
      confirmText: "Confirmar",
      confirmBtnColor: "green",
      confirmFn: () => {
        this.appService.spinner.show();
        this.backofficeService
          .unblockBackofficeUser(id)
          .pipe(
            takeWhile(() => this.isAlive),
            finalize(() => this.appService.spinner.hide())
          )
          .subscribe((resp) => {
            this.appService.toastr.success(resp.data.message, "Atenção");
            this.grid.load();
          });
      },
    });
  }

  openModalBlockedReason(nameUser: string, reason: string) {
    this.blockedReasonSelected = reason;
    this.modalBlockedReason.open({
      closable: true,
      isScrolling: true,
      title: `Motivo do usuário '${nameUser}' ter sido bloqueado`,
      showCloseBtn: true,
      size: "medium",
    });
  }

  recoveryPassordUser(email: string) {
    this.alertService.confirm({
      headerTitle: "Atenção",
      msg: "Deseja realmente enviar um e-mail de recuperação de senha para este usuário?",
      cancelText: "Cancelar",
      cancelBtnColor: "red",
      confirmText: "Confirmar",
      confirmBtnColor: "green",
      confirmFn: () => {
        this.appService.spinner.show();
        this.authService
          .remember({ email })
          .pipe(
            takeWhile(() => this.isAlive),
            finalize(() => this.appService.spinner.hide())
          )
          .subscribe((resp) => {
            this.appService.toastr.success(resp.data.message, "Atenção");
          });
      },
    });
  }
}
