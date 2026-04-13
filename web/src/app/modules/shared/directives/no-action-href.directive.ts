import { Directive, ElementRef } from '@angular/core';



@Directive({
  selector: '[noActionHref],[noAction]'
})
export class NoActionHrefDirective  {

 constructor(private elementRef: ElementRef){
   (this.elementRef.nativeElement as HTMLElement).setAttribute('href', '#');
   (this.elementRef.nativeElement as HTMLElement).setAttribute('onclick', 'return false;');
 }


}
