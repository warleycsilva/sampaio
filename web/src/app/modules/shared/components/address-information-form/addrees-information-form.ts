import { FormGroup, FormControl } from "@angular/forms";

export const addressInformationForm = (data: any = {}) => new FormGroup({
  address: new FormControl(data.address || ''),
  number: new FormControl(data.number || ''),
  complement: new FormControl(data.complement || ''),
  district: new FormControl(data.district || ''),
  zipCode: new FormControl(data.zipCode || ''),
  city: new FormControl(data.city || ''),
  state: new FormControl(data.state || ''),
});
