import { Component, OnInit, SkipSelf, Input, ChangeDetectionStrategy } from '@angular/core';
import { FormGroup, ControlContainer, Validators, FormControl } from '@angular/forms';
import { BaseComponent } from '../base/base.component';
import { BrazilStates } from '../../models/brazil-states';

@Component({
  selector: 'address-information-form',
  templateUrl: './address-information-form.component.html',
  styleUrls: ['./address-information-form.component.css']
})
export class AddressInformationFormComponent extends BaseComponent {

  constructor(@SkipSelf() public group: ControlContainer) {
    super();
  }


  ngOnInit() {
    this.brStates = BrazilStates.states;
    this.form = this.group.control.get('addressInformation') as FormGroup;
    this.loaded = true;

    if(this.required){
      this.form.get('address').setValidators(Validators.required);
      this.form.get('number').setValidators(Validators.required);
      this.form.get('district').setValidators(Validators.required);
      this.form.get('zipCode').setValidators(Validators.required);
      this.form.get('city').setValidators(Validators.required);
      this.form.get('state').setValidators(Validators.required);
    }
  }

  form: FormGroup;

  loaded = false;

  @Input('use-brazil-states')useBrStates: boolean;

  @Input('submitted')submitted: boolean;

  @Input('required')required = false;

  brStates: any[] = []
}
