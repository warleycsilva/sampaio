import { Directive, ElementRef, HostListener, forwardRef, Renderer2 } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NgModel } from '@angular/forms';

export const DATE_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => DateValueAccessor),
  multi: true
};

/**
 * The accessor for writing a value and listening to changes on a date input element
 *
 *  ### Example
 *  `<input type="date" name="myBirthday" ngModel useValueAsDate>`
 */
@Directive({
  selector: '[useValueAsDate]',
  providers: [DATE_VALUE_ACCESSOR]
})
export class DateValueAccessor implements ControlValueAccessor {

  @HostListener('input', ['$event.target.valueAsDate']) onChange = (_: any) => { };
  @HostListener('blur', []) onTouched = () => { };

  constructor(private _renderer: Renderer2, private _elementRef: ElementRef, private model: NgModel) { }

  writeValue(value: any): void {
    let dateValue: Date = this.toDate(value);
    this._renderer.setProperty(this._elementRef.nativeElement, 'valueAsDate', dateValue);
  }

  registerOnChange(fn: (_: any) => void): void { this.onChange = fn; }
  registerOnTouched(fn: () => void): void { this.onTouched = fn; }

  setDisabledState(isDisabled: boolean): void {
    this._renderer.setProperty(this._elementRef.nativeElement, 'disabled', isDisabled);
  }

  private toDate(value?: any) {
    if (!value) return null;
    let dateValue: Date;
    if (!(value instanceof Date)) {
      dateValue = new Date(value);
    }
    return new Date(this.toUTCJsonData(dateValue));
    //return new Date(dateValue.toLocaleString());
  }

  private toUTCJsonData(date?: Date) {
    date = date || new Date();
    date.setHours(0,0,0,0);
    let utc = new Date(date.getTime() + date.getTimezoneOffset() * 60000);
    return utc.toJSON();
  }
}