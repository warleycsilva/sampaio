import { Component, EventEmitter, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ModalComponent } from 'ngx-chameleon';
import { finalize, takeWhile } from 'rxjs';
import { AppService } from '../../services/app.service';
import { BackofficeService } from '../../services/backoffice.service';
import { BaseComponent } from '../base/base.component';

@Component({
  selector: 'modal-block-user',
  templateUrl: './modal-block-user.component.html'
})
export class ModalBlockUserComponent extends BaseComponent {
  
  @Output("on-close-modal") onCloseModal = new EventEmitter<any>();
  
  @ViewChild("modalBlockUser", { static: true })
  
  modalBlockUser: ModalComponent;
  formBlock = new FormGroup({
    blockReason: new FormControl(null, Validators.required),
  });

  idPartnerUserBlock: string;
  submittedBlock = false;

  constructor(
    private backofficeService: BackofficeService,
    private appService: AppService,
  ) {
    super();
  }

  closeModal = () => this.onCloseModal.emit({});

  blockPartnerUser() {
    this.submittedBlock = true
    if (this.formBlock.invalid) return;

    this.appService.spinner.show();
    this.backofficeService
      .blockPartnerUser(this.idPartnerUserBlock, {
        blockReason: this.formBlock.value.blockReason,
      })
      .pipe(
        takeWhile(() => this.isAlive),
        finalize(() => this.appService.spinner.hide())
      )
      .subscribe((resp) => {
        this.formBlock.get("blockReason").setValue(null);
        this.idPartnerUserBlock = null;
        this.modalBlockUser.close();
        this.appService.toastr.success(resp.data.message, "Atenção");
        this.closeModal();
      });
  }

  openModalFormBlock(id: string, name: string) {
    this.idPartnerUserBlock = id;
    this.modalBlockUser.open({
      closable: true,
      isScrolling: true,
      title: `Deseja realmente bloquear usuário? ${name}`,
      showCloseBtn: true,
      size: "small",
    });
  }

}
