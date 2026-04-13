import {Component, OnInit} from '@angular/core';
import {AlertService, TopBarBreadcrumbService} from "ngx-chameleon";
import {AppService} from "../../../../shared/services/app.service";
import {BackofficeService} from "../../../../shared/services/backoffice.service";
import {AuthService} from "../../../../shared/services/auth.service";
import {BaseComponent} from "../../../../shared/components/base/base.component";
import {ActivatedRoute, Router} from '@angular/router';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {
  BackofficeViagensService,
  Passageiro,
  ViagemPagamento
} from 'src/app/modules/shared/services/backoffice.viagens.service';
import {finalize, takeWhile} from "rxjs/operators";

@Component({
  selector: 'main-viagem-upsert',
  templateUrl: './viagem-upsert.component.html'
})
export class ViagemUpsertComponent extends BaseComponent implements OnInit {
  id: string;
  editing: boolean = false;
  readonly: boolean = false;
  form: FormGroup;
  submitted: boolean = false;
  passageiros: Passageiro[] = [];
  viagemPagamentos: ViagemPagamento[] = [];
  valorTotalFaturado: number;
  now = new Date();

  constructor(
    private topBarBreadcrumbService: TopBarBreadcrumbService,
    private appService: AppService,
    private backofficeService: BackofficeService,
    private backofficeViagensService: BackofficeViagensService,
    private authService: AuthService,
    private alertService: AlertService,
    private actRoute: ActivatedRoute,
    private router: Router,
  ) {
    super();
  }

  ngOnInit(): void {
    this.id = this.actRoute.snapshot.params.id;
    this.readonly = this.router.url.includes('detalhes');
    this.editing = !!this.id;
    this.createForm();
    this.topBarBreadcrumbService.emmiter.next({
      icon: "fas fa-users fa-icon",
      title: this.editing ? "Editar viagem" : "Nova viagem",
      path: ["Lista de viagens", this.editing && !this.readonly ? "Editar viagem" : this.readonly ? "Detalhes da viagem" : "Nova viagem"],
    });
  }

  createForm() {
    this.form = new FormGroup({
      origem: new FormControl(null, Validators.required),
      destino: new FormControl(null, Validators.required),
      dataPartida: new FormControl(null, Validators.required),
      preco: new FormControl(null, Validators.required),
      qtdVagas: new FormControl(null, Validators.required),
      observacoes: new FormControl(null),
      assentosDisponiveis: new FormControl(null),
      assentosOcupados: new FormControl(null),
    });
    this.obterPorId();
  }

  save() {
    if (this.form.invalid) {
      this.appService.toastr?.error("Verifique se preencheu todos dados corretamente.")
      return
    }

    if (!this.editing) {
      this.backofficeViagensService.criar(this.form.value)
        .pipe(
          takeWhile(() => this.isAlive),
          finalize(() => this.appService.spinner.hide())
        )
        .subscribe((resp) => {
          this.appService.toastr.success(resp.data.message, "Atenção");
          this.router.navigate(["/backoffice/viagens"]);
        });
      return;
    }
    this.backofficeViagensService.editar(this.id, this.form.value)
      .pipe(
        takeWhile(() => this.isAlive),
        finalize(() => this.appService.spinner.hide())
      )
      .subscribe((resp) => {
        this.appService.toastr.success(resp.data.message, "Atenção");
        this.router.navigate(["/backoffice/viagens"]);
      });
  }

  obterPorId() {
    if (this.editing)
      this.backofficeViagensService.obterPorId(this.id)
        .pipe(
          takeWhile(() => this.isAlive),
          finalize(() => this.appService.spinner.hide())
        )
        .subscribe((resp) => {
          this.valorTotalFaturado = resp.data.valorTotalFaturado;
          this.form.patchValue({
            ...resp.data,
            dataPartida: this.appService.dateUtils.toLocaleJsonDate(resp.data.dataPartida.toString().replace("Z",""))
          });
          this.passageiros = resp.data?.passageiros
          this.readonly && this.form.disable();
        });
  }

  estorno(passageiroId: string, estornado: boolean) {
     if(estornado) {
       this.appService.toastr.warning("Passageiro já estornado!");
        return;
     }
     this.alertService.confirm({
       headerTitle: "Atenção",
       msg: "Deseja realmente estornar o pagamento desse passageiro?",
       cancelText: "Cancelar",
       cancelBtnColor: "gray",
       confirmText: "Confirmar",
       confirmBtnColor: "red",
       confirmFn: () => {
         this.appService.spinner.show();

         this.backofficeViagensService.estornar(passageiroId)
           .pipe(
             takeWhile(() => this.isAlive),
             finalize(() => this.appService.spinner.hide())
           )
           .subscribe((resp) => {
             this.appService.toastr.success("Estornado com sucesso!");
             this.obterPorId()
           });}
     });
  }
}
