import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../_services';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private http: HttpClient) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    //  if (localStorage.getItem('currentUser')) {
    //      // logged in so return true
    //      return true;
    // }

    return this.checkLogin('/Account/CheckAuthentication', state);

    // not logged in so redirect to login page with the return url
    // this.router.navigate(['/account/login'], { queryParams: { returnUrl: state.url }});

  }

  checkLogin(url: string, state: RouterStateSnapshot): Observable<boolean> {
    return this.http.post(url, '').pipe(map(res => {
      if (res === true) {
        return true;
      } else {
        this.router.navigate(['/account/login'], { queryParams: { returnUrl: state.url } });
        return false;
      }
    }));
  }
}
