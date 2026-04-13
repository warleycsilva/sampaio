import { Component, OnInit } from '@angular/core';
import {AlertService, TopBarBreadcrumbService} from "ngx-chameleon";
import {AppService} from "../../../shared/services/app.service";
import {BackofficeService} from "../../../shared/services/backoffice.service";
import {AuthService} from "../../../shared/services/auth.service";
import {PagedListService} from "ngx-paged-list";
import {environment} from "../../../../../environments/environment";
import {BaseComponent} from "../../../shared/components/base/base.component";
import {Router} from "@angular/router";
import {BackofficeViagensService} from "../../../shared/services/backoffice.viagens.service";
import {finalize, takeWhile} from "rxjs/operators";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'main-viagens',
  templateUrl: './viagens.component.html'
})
export class ViagensComponent extends BaseComponent implements OnInit {
  grid: PagedListService;
  filterForm: FormGroup;

  constructor(
    private topBarBreadcrumbService: TopBarBreadcrumbService,
    private appService: AppService,
    private router: Router,
    private backofficeViagensService: BackofficeViagensService,
  ) {
    super();

    this.grid = new PagedListService({
      isAlive: this.isAlive,
      onLoadStarts: () => this.appService.spinner.show(),
      onLoadFinished: () => this.appService.spinner.hide(),
      pageSize: 20,
      sortField: "CreatedAt",
      sortType: "desc",
      url: `${environment.urls.api.v1}/backoffice/viagens`,
    });
  }

  ngOnInit(): void {
    this.filterForm = new FormGroup({
      origem: new FormControl(''),
      destino: new FormControl(''),
      dataPartida: new FormControl(null),
      email: new FormControl(''),
      cpf: new FormControl(''),
      isActive: new FormControl(null)
    });
    this.topBarBreadcrumbService.emmiter.next({
      icon: "fas fa-users fa-icon",
      title: "Lista de viagens",
      path: ["Lista de viagens"],
    });
    this.grid.load();
  }

  novaViagem() {
    this.router.navigate(['backoffice/viagens/nova'])
  }

  toggleActivation(id) {
    this.backofficeViagensService.toggleActivation(id)
      .pipe(
        takeWhile(() => this.isAlive),
        finalize(() => this.appService.spinner.hide())
      )
      .subscribe((resp) => {
        this.grid.load()
        this.appService.toastr.success("Status da viagem atualizado.")
      });
    return;
  }

  search() {
    this.grid.load(this.filterForm.value, true);
  }

  clear() {
    this.filterForm.reset();
    this.grid.load({}, true, true);
  }
}
