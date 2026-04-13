
import { Injectable, ElementRef, ViewChild, Input } from "@angular/core";
import { Observable } from "rxjs";


@Injectable({
    providedIn: "root",
})

export class ImagesExtensionsService {

    constructor() { }

    @ViewChild('file', { static: true }) file: ElementRef;
    @Input('groupName') groupName: string;
    @Input('label') label: string;
    @Input('imageUrl') imageUrl: string;
    @Input('isImg') isImg = false;


    selectFile = () => (this.file.nativeElement as HTMLInputElement).click();

    onDocumentFileChange($event: Event, formGroup?: any) {
        this.toBase64($event.target as HTMLInputElement)
            .pipe()
            .subscribe((resp) => {
                if (formGroup) {
                    formGroup.get("name").setValue(resp.fileName);
                    formGroup.get("buffer").setValue(resp.contentFile);
                }
            });
    }


    private toBase64(file: HTMLInputElement): Observable<any> {
        const reader = new FileReader();
        const fileName = file.files[0].name;
        reader.readAsBinaryString(file.files[0]);
        return Observable.create((observer) => {
            reader.onload = (e) => {
                observer.next({
                    fileName,
                    contentFile: btoa(reader.result as string),
                });
            };
            reader.onerror = (error) => observer.error(error);
        });
    }

   
}