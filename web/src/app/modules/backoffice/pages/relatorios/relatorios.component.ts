import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup} from '@angular/forms';
import {PagedListService} from 'ngx-paged-list';
import {TopBarBreadcrumbService} from 'ngx-chameleon';
import {environment} from '../../../../../environments/environment';
import {BaseComponent} from '../../../shared/components/base/base.component';
import {AppService} from '../../../shared/services/app.service';
import {BackofficeViagensService} from '../../../shared/services/backoffice.viagens.service';
import {Router} from "@angular/router";

@Component({
    selector: 'main-relatorios',
    templateUrl: './relatorios.component.html',
    styleUrls: ['./relatorios.component.css']
})
export class RelatoriosComponent extends BaseComponent implements OnInit {
    grid: PagedListService;
    filterForm: FormGroup;

    constructor(
        private router: Router,
        private topBarBreadcrumbService: TopBarBreadcrumbService,
        private appService: AppService,
        private backofficeViagensService: BackofficeViagensService
    ) {
        super();

        this.grid = new PagedListService({
            isAlive: this.isAlive,
            onLoadStarts: () => this.appService.spinner.show(),
            onLoadFinished: () => this.appService.spinner.hide(),
            pageSize: 20,
            sortField: 'DataPartida',
            sortType: 'desc',
            url: `${environment.urls.api.v1}/backoffice/viagens`
        });
    }

    ngOnInit(): void {
        this.filterForm = new FormGroup({
            dataInicio: new FormControl(null),
            dataFim: new FormControl(null),
            origem: new FormControl(''),
            destino: new FormControl('')
        });

        this.topBarBreadcrumbService.emmiter.next({
            icon: 'fas fa-chart-bar fa-icon',
            title: 'Relatórios',
            path: ['Relatórios']
        });

        this.grid.load();
    }

    search() {
        this.grid.load(this.filterForm.value, true);
    }

    clear() {
        this.filterForm.reset();
        this.grid.load({}, true, true);
    }

    ocupacao(item: any) {
        if (!item?.qtdVagas) {
            return 0;
        }
        return (item.vagasVendidas / item.qtdVagas) * 100;
    }

    receita(item: any) {
        const preco = Number(item.preco ?? 0);
        const vendidas = Number(item.vagasVendidas ?? 0);
        return preco * vendidas;
    }

    openTravel(id) {
        this.router.navigate(["/backoffice/viagens", id, "detalhes"]);
    }
}
