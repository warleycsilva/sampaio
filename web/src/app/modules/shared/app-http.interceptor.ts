import { Injectable, NgZone } from "@angular/core";
import { Router } from "@angular/router";
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from "@angular/common/http";

import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { AppService } from "./services/app.service";

declare var toastr: any;

@Injectable({
  providedIn: "root",
})
export class AppHttpInterceptor implements HttpInterceptor {
  constructor(
    private zone: NgZone,
    private router: Router,
    private appService: AppService
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    let newRequest = request.headers.has("Authorization")
      ? request.clone({})
      : request.clone({
          setHeaders: {
            Authorization: `Bearer ${this.appService.accessToken}`,
          },
        });

    return next.handle(newRequest).pipe(
      tap(
        (event: HttpEvent<any>) => {},
        (error) => {
          this.zone.run(() => {
            if (error instanceof HttpErrorResponse) {
              let response = <HttpErrorResponse>error;

              if (response.status == 401) {
                if (
                  response.error &&
                  response.error.errors &&
                  response.error.errors.length > 0
                ) {
                  this.appService.removeUser();
                  this.appService.toastr.error("Sua sessão expirou, faça login novamente para continuar");
                  this.router.navigate(["backoffice"]);
                }
              } else if (response.status == 403) {
                this.appService.removeUser();
                this.appService.toastr.error("Sua sessão expirou, faça login novamente para continuar");
                this.router.navigate(["backoffice"]);
              }else if (response.status == 404) {
                this.resolveErrors({}, [
                  {
                    value: "Recurso não encontrado",
                  },
                ]);
              } else if (response.status == 422) {
                this.resolveErrors({}, [
                  {
                    value: "Entidade não processável",
                  },
                ]);
              } else {
                this.resolveErrors(response);
              }
            } else {
              this.resolveErrors(error);
            }
          });
        }
      )
    );
  }

  private resolveErrors(response: any, refErrors: any[] = []) {
    let errors: any[] = response.error
      ? response.error.errors || []
      : response.errors || [];
    errors = errors.concat(refErrors);
    if (errors.length === 0) {
      errors.push({
        value: "Ocorreu um erro desconhecido!",
      });
    }
    let msg = `<ul>${errors.map((e) => `<li>${e}</li>`).join("")}</ul>`;
    this.appService.toastr.error(msg);
  }
}
