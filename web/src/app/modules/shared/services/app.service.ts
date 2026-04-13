import { Injectable, EventEmitter } from "@angular/core";
import { environment } from "src/environments/environment";
import { SessionUser } from "../models/session-user";
import { ToastrService } from "ngx-toastr";
import { NgxSpinnerService } from "ngx-spinner";
import {FormArray, FormGroup, AbstractControl, FormControl} from "@angular/forms";
import { Observable } from "rxjs";
import { TopBarBreadcrumbService } from "ngx-chameleon";
import { PagedListService } from "ngx-paged-list";
import {HttpParams} from "@angular/common/http";

@Injectable({
  providedIn: "root",
})
export class AppService {
  constructor(
    private topBarBreadcrumbService: TopBarBreadcrumbService,
    private toastrService: ToastrService,
    private spinnerService: NgxSpinnerService
  ) {
    this.forms = new CustomFormsService();
    this.toastr = new CustomToastrService(this.toastrService);
    this.spinner = new CustomSpinnerService(this.spinnerService);
    this.dateUtils = new DateUtils();
  }

  forms: CustomFormsService;
  toastr: CustomToastrService;
  spinner: CustomSpinnerService;
  dateUtils: DateUtils;

  private _sessionUser: SessionUser;

  public get sessionUser(): SessionUser {
    if (!this._sessionUser) {
      const jsonUserData = localStorage.getItem(environment.localStore.user);
      this._sessionUser = jsonUserData ? JSON.parse(jsonUserData) : null;
    }
    return this._sessionUser;
  }

  sessionUserEvent = new EventEmitter<SessionUser>();

  storeUser(sessionUser: SessionUser) {
    this._sessionUser = sessionUser;
    this.sessionUserEvent.next(sessionUser);
    localStorage.setItem(
      environment.localStore.user,
      JSON.stringify(sessionUser)
    );
  }
  removeUser() {
    localStorage.removeItem(environment.localStore.user);
    this._sessionUser = null;
    this.sessionUserEvent.next(null);
  }

  storeFilterGrid(key: string,
    grid: PagedListService,
    data?: any) {
    const filter = {
      ...data || {},
      pageIndex: grid.pageIndex,
      sortField: grid.sortField,
      sortType: grid.sortType
    };
    localStorage.setItem(key, JSON.stringify(filter));
  }

  get accessToken(): string {
    return localStorage.getItem(environment.localStore.token);
  }

  storeAcsessToken = (token: string) =>
    localStorage.setItem(environment.localStore.token, token);

  removeAccessToken = () =>
    localStorage.removeItem(environment.localStore.token);

  fileToBase64(file: HTMLInputElement): Observable<any> {
    let reader = new FileReader();

    let name = file.files[0].name;

    let type = file.files[0].type;

    reader.readAsBinaryString(file.files[0]);

    reader.onload = (e) => console.log(btoa(reader.result as string));

    return Observable.create((observer) => {
      reader.onload = (e) => {
        observer.next({
          name,
          buffer: btoa(reader.result as string),
          type,
        });
      };
      reader.onerror = (error) => observer.error(error);
    });
  }

  base64toBlob(base64Data: string, contentType: string) {
    contentType = contentType || "";
    let sliceSize = 1024;
    let byteCharacters = atob(base64Data);
    let bytesLength = byteCharacters.length;
    let slicesCount = Math.ceil(bytesLength / sliceSize);
    let byteArrays = new Array(slicesCount);
    for (let sliceIndex = 0; sliceIndex < slicesCount; ++sliceIndex) {
      let begin = sliceIndex * sliceSize;
      let end = Math.min(begin + sliceSize, bytesLength);

      let bytes = new Array(end - begin);
      for (var offset = begin, i = 0; offset < end; ++i, ++offset) {
        bytes[i] = byteCharacters[offset].charCodeAt(0);
      }
      byteArrays[sliceIndex] = new Uint8Array(bytes);
    }
    return new Blob(byteArrays, { type: contentType });
  }

  order(array: any[], prop: string) {
    let aux = [...array];
    return array.sort(function (a, b) {
      let x = a[prop];
      let y = b[prop];

      if (typeof x == "string") {
        x = ("" + x).toLowerCase();
      }
      if (typeof y == "string") {
        y = ("" + y).toLowerCase();
      }

      return x < y ? -1 : x > y ? 1 : 0;
    });
  }

  copyToClipboard(value: string) {
    let selBox = document.createElement("textarea");
    selBox.style.position = "fixed";
    selBox.style.left = "0";
    selBox.style.top = "0";
    selBox.style.opacity = "0";
    selBox.value = value;
    document.body.appendChild(selBox);
    selBox.focus();
    selBox.select();
    document.execCommand("copy");
    document.body.removeChild(selBox);
  }

  #region //http-utils
  formatParams(filter: any){
    let params = new HttpParams();
    Object.keys(filter).forEach(
      key => filter[key] && (params = params.append(key, filter[key]))
    );
    return params
  }

  validURL(str) {
    var pattern = new RegExp("^(http[s]?:\/\/){1,1}(www\.){0,1}[a-zA-Z0-9\.\-]+\.[a-zA-Z]{2,5}[\.]{0,1}");
    return !!pattern.test(str);

  }
  #endregion

}

class CustomToastrService {
  constructor(private toastrService: ToastrService) { }

  private readonly defaultTitle: string = "Atenção";

  success = (message: string, title?: string) =>
    setTimeout(() =>
      this.toastrService.success(message, title || this.defaultTitle)
    );

  error = (message: string, title?: string) =>
    setTimeout(() =>
      this.toastrService.error(message, title || this.defaultTitle)
    );

  info = (message: string, title?: string) =>
    setTimeout(() =>
      this.toastrService.info(message, title || this.defaultTitle)
    );

  warning = (message: string, title?: string) =>
    setTimeout(() =>
      this.toastrService.warning(message, title || this.defaultTitle)
    );
}

class CustomSpinnerService {
  constructor(private spinnerService: NgxSpinnerService) { }

  show = () => this.spinnerService.show();

  hide = () => this.spinnerService.hide();

  start = () => this.show();

  stop = () => this.hide();
}

class DateUtils {
  toLocaleJsonDate(value: any) {
    const date: Date = value instanceof Date ? value : new Date(value);

    return `${date.getFullYear()}-${(date.getMonth() + 1)
      .toString()
      .padStart(2, "0")}-${date.getDate().toString().padStart(2, "0")}T${date
        .getHours()
        .toString()
        .padStart(2, "0")}:${date.getMinutes().toString().padStart(2, "0")}`;
  }

  toDate(value?: any) {
    if (!value) {
      return null;
    }
    let dateValue: Date;
    if (!(value instanceof Date)) {
      dateValue = new Date(value);
    }
    return dateValue;
  }

  getUTCJsonData(value?: any) {
    if (!value || value == null) {
      return null;
    }
    let date: Date = value instanceof Date ? value : this.toDate(value);
    let utc = new Date(date.getTime() + date.getTimezoneOffset() * 60000);
    return utc.toJSON();
  }

  getUTCJsonData2(value?: any) {
    if (!value || value == null) return null;
    let date: Date = value instanceof Date ? value : this.toDate(value);
    let utc = new Date(date.toUTCString());
    return utc.toJSON();
  }

  formatDate(date?: any) {
    var d = new Date(date),
      month = "" + (d.getMonth() + 1),
      day = "" + d.getDate(),
      year = d.getFullYear();

    if (month.length < 2) month = "0" + month;
    if (day.length < 2) day = "0" + day;

    return [year, month, day].join("-");
  }
}

class CustomFormsService {
  date: DateUtils = new DateUtils();

  clearFormArray(formArray: FormArray) {
    while (formArray.controls.length > 0) {
      formArray.removeAt(0);
    }
  }

  fill(form: FormGroup, data: any) {
    for (let key in form.controls) {
      if (form.controls[key] instanceof FormArray) {
        continue;
      }
      if (form.controls[key] instanceof FormGroup) {
        this.fill(form.controls[key] as FormGroup, data[key]);
      } else {
        form.controls[key].setValue(
          data == null ? null : data[key] == null ? null : data[key]
        );
      }
    }
  }

  clear(form: FormGroup) {
    for (let key in form.controls) {
      if (form.controls[key] instanceof FormArray) {
        this.clearFormArray(form.controls[key] as FormArray);
        continue;
      }
      if (form.controls[key] instanceof FormGroup) {
        this.clear(form.controls[key] as FormGroup);
      } else {
        form.controls[key].setValue(null);
      }
    }
  }

  toJsonDate(form: FormGroup, ...props: string[]) {
    for (let p in props) {
      this.controlValueToJsonDate(form.controls[props[p]]);
    }
  }

  controlValueToJsonDate(control: AbstractControl) {
    control.setValue(this.date.getUTCJsonData(control.value));
  }

  removeValidator(control: AbstractControl) {
    control.setValue("");
    control.clearValidators();
    control.setErrors(null);
    control.setValidators(null);
    control.updateValueAndValidity();
  }

  addValidators(control: AbstractControl, ...validators: any) {
    control.setValidators(validators);
    control.updateValueAndValidity();
  }

}
