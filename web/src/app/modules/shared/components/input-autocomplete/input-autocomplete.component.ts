import { Component, ElementRef, EventEmitter, Input, OnInit, Output, Renderer2, ViewChild } from '@angular/core';

type IOption = {
  key: string;
  value: string;
}

@Component({
  selector: 'main-input-autocomplete',
  templateUrl: './input-autocomplete.component.html',
  styleUrls: ['./input-autocomplete.component.css']
})
export class InputAutocompleteComponent implements OnInit {

  constructor(private renderer: Renderer2) {
    this.onClickOutside();
  }

  ngOnInit(): void {}

  @ViewChild('dropdown') dropdown: ElementRef = new ElementRef('');
  @ViewChild('input') input: ElementRef = new ElementRef('');
  @ViewChild('chevron') chevron: ElementRef = new ElementRef('');
  @ViewChild('list') listElement: ElementRef = new ElementRef('');

  @Input() options: IOption[] = [];
  @Input() required: boolean = false;
  @Input() isDisabled: boolean = false;
  @Input() isLoading: boolean = false;
  @Input() shouldClearAfterSelect: boolean = false;

  @Output() onSelectEvent = new EventEmitter<IOption>();
  @Output() onSearchEvent = new EventEmitter<string>();
  
  isOpen: boolean = false;
  hasSearchedOnce: boolean = false;
  search: string = '';
  focusIndex: number = -1;
  typingTimeout: ReturnType<typeof setTimeout> = setTimeout(() => {}, 0);


  //ao clicar fora
  onClickOutside(): void {
    this.renderer.listen('window', 'click', (e: Event) => {
      if (e.target !== this.dropdown.nativeElement) {
        this.isOpen = false;
      }
    });
  }

  ngOnChanges(): void {}

  onSelect(option: IOption): void {
    this.search = this.shouldClearAfterSelect ? '' : option.value;
    this.onSelectEvent.emit(option);
  }

  fetchResults(): void {
    const searchString = this.search.trim().toLocaleLowerCase();
    if (searchString.length > 3) {
      this.typingTimeout = setTimeout(() => {
        this.hasSearchedOnce = true;
        this.onSearchEvent.emit(searchString);
      }, 500);
    } 
  }
  //funcionalidade de usar o teclado
  onKeyUp(event: KeyboardEvent, currentOption: IOption | null = null): void {
    switch (event.key) {
      case 'Enter':
        if (this.options.length > 0 && !!this.search && this.isOpen) {
          const selectedOption = currentOption || this.options[0];
          this.onSelect(selectedOption);
          this.isOpen = false;
        }
        break;
      case 'ArrowUp':
        if (this.focusIndex > 0) {
          this.focusIndex--;
          this.listElement.nativeElement.children[this.focusIndex].focus();
        }
        break;
      case 'ArrowDown':
        if (this.focusIndex < this.listElement.nativeElement.children.length - 1) {
          this.focusIndex++;
          this.listElement.nativeElement.children[this.focusIndex].focus();
        }
        break;
      default:
        clearTimeout(this.typingTimeout);
        this.fetchResults();
        break;
    }
  }
}

