import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanActivate} from '@angular/router';
import {Observable, of} from 'rxjs';
import {AppService} from '../services/app.service';

@Injectable({
  providedIn: 'root'
})
export class LoggedActiveGuard implements CanActivate {
  constructor(private router: Router,
              private appService: AppService) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

    if (!this.appService.sessionUser) {
      this.router.navigate(['/login']);
      return of(false);
    }

    return of(true);
  }
}
