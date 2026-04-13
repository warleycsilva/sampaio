import { AbstractControl, ValidatorFn } from '@angular/forms';

export class CustomValidators{

  static match(target: string) : ValidatorFn {

    return (control: AbstractControl):  {[key: string]: any} | null  => {

      if(!control || !control.parent || !control.parent.get(target)) return null;

      const value = control.parent.get(target).value;

      return !target ? { 'match': true } : ( control.value === value ? null : { 'match': true } );
    };
  }

  static atLeastOneTrue() {
    return (c: AbstractControl): {[key: string]: any} => {
        if (!c.value || c.value.filter(x => x).length > 0)
            return null;

        return { 'atLeastOneTrue': { valid: false }};
    }
}
}
