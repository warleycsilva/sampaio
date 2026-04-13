import { Component, OnInit, SkipSelf, Input, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { BaseComponent } from '../base/base.component';
import { ControlContainer, FormGroup } from '@angular/forms';
import { takeWhile } from 'rxjs/operators';
import { AppService } from '../../services/app.service';

@Component({
  selector: 'image-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.css']
})
export class ImageUploadComponent extends BaseComponent implements AfterViewInit {

  constructor(@SkipSelf() public group: ControlContainer,
    private elementRef: ElementRef,
    private appService: AppService) {
    super();
  }

  ngOnInit(): void {
    this.form = this.group.control.get(this.groupName) as FormGroup;
  }

  ngAfterViewInit(): void {
    const nodeToBeRemoved = this.elementRef.nativeElement;

    while (nodeToBeRemoved.firstChild) {
        nodeToBeRemoved.parentNode.insertBefore(
          nodeToBeRemoved.firstChild,
          nodeToBeRemoved
        );
    }

    nodeToBeRemoved.parentNode.removeChild(nodeToBeRemoved);
  }

  @ViewChild('file', { static: true }) file: ElementRef;
  @Input('groupName') groupName: string;
  @Input('label') label: string;
  @Input('imageUrl') imageUrl: string;
  @Input('isImg') isImg = false;

  form: FormGroup;

  selectFile = () => (this.file.nativeElement as HTMLInputElement).click();

  onFileChange($event) {
    this.appService.fileToBase64($event.target as HTMLInputElement).pipe(
      takeWhile(() => this.isAlive)
    ).subscribe(resp => {
      this.form.get('name').setValue(resp.name);
      this.form.get('buffer').setValue(resp.buffer);
      this.imageUrl = `data:${resp.type};base64, ${resp.buffer}`;
    });
  }

  
}
