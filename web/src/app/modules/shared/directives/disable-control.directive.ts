import { Directive, Input } from '@angular/core';
import { NgControl } from '@angular/forms';

@Directive({
  selector: '[disable-control]'
})
export class DisableControlDirective {

  @Input('disable-control') set disableControl(condition: boolean) {
    setTimeout(() => {
      if (condition) {
        this.ngControl.control.disable();
        return;
      }
      this.ngControl.control.enable()
    }, 10);
  }

  constructor(private ngControl: NgControl) {
  }

}
