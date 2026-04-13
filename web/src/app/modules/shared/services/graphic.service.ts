import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class GraphicService {

  private url = environment.urls;

  constructor(private http: HttpClient) { }

  getGraphicData = (): Observable<any> =>
  this.http.get<any>(`${this.url.api2}/renter/v1/renters/home/graphic`).pipe(map(resp => resp));
}
