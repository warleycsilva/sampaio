import { Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[scrollTop]'
})
export class ScrollTopDirective {

  constructor(private element: ElementRef) {
    (this.element.nativeElement as HTMLElement).onclick = (e) => {
      e.preventDefault();
      window.scrollTo(0, 0);
    };
  }

}
