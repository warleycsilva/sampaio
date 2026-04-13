import {ChangeDetectorRef, Directive, Host, OnInit, Optional, SimpleChange} from '@angular/core';
import {MaskDirective} from "ngx-mask";

@Directive({
  selector: '[phoneMask]'
})

export class PhoneMaskDirective implements OnInit  {

  mask11 = "(00) 0 0000-0000";
  mask10 = "(00) 0000-00009";
  currentMask = this.mask10;
  currentValue = null;

  constructor(private cdr: ChangeDetectorRef, @Host() @Optional() private mask: MaskDirective) {
  }

  ngOnInit(): void {
    this.mask.maskExpression = this.currentMask;
    this.mask.ngOnChanges({"maskExpression": new SimpleChange("", this.currentMask, true)});
    this.mask.registerOnChange((telefone) => {
      if (telefone != null && this.currentValue !== telefone) {
        this.currentValue = telefone;
        this.processInputChange(telefone);
      }
    });
    this.cdr.detectChanges();
  }

  private processInputChange(telefone: string) {
    if (telefone.length <= 10) {
      if (this.currentMask != this.mask10) {
        this.currentMask = this.mask10;
        setTimeout(() => {
          this.mask.ngOnChanges({"maskExpression": new SimpleChange(null, this.mask10, false)});
        }, 50);
      }
    } else {
      this.currentMask = this.mask11;
      setTimeout(() => {
        this.mask.ngOnChanges({"maskExpression": new SimpleChange(null, this.mask11, false)});
      }, 50);
    }
  }
}
