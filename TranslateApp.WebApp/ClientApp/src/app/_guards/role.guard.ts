import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable()
export class RoleGuard implements CanActivate {

  constructor(private router: Router, private http: HttpClient) { }

  canActivate(route: ActivatedRouteSnapshot): Observable<boolean> {

    return this.checkRole('/Account/UserIsAdmin');

  }

  checkRole(url: string): Observable<boolean> {
    return this.http.post(url, '').pipe(map(res => {
      if (res === true) {
        return true;
      } else {
        this.router.navigate(['/account/login']);
        return false;
      }
    }));
  }

}
