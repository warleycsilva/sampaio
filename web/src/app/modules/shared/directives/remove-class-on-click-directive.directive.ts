import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[remove-class-on-click]'
})
export class RemoveClassOnClickDirective {

  constructor(private elementRef: ElementRef) {
    this.element = this.elementRef.nativeElement as HTMLElement;
  }

  private element: HTMLElement;

  @Input('remove-class-on-click')value: string;

  @HostListener('click', ['$event']) onClick($event) {
    this.element.classList.remove(this.value);
  }
}
