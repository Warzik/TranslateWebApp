
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Injectable, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';

import { LoginViewModel, RegisterViewModel } from '../_models';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<LoginViewModel>;
    public currentUser: Observable<LoginViewModel>;

  constructor(private http: HttpClient, @Inject(DOCUMENT) private document: Document) {
        this.currentUserSubject = new BehaviorSubject<LoginViewModel>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): LoginViewModel {
        return this.currentUserSubject.value;
    }

    login(email: string, password: string, rememberMe: boolean ) {
      return this.http.post<any>('/account/login', { email, password, rememberMe }); 
    }

  googleLogin() {
    this.document.location.href = 'http://localhost:5000/account/signInWithGoogle';
  }
  facebookLogin() {
    this.document.location.href = 'http://localhost:5000/account/signInWithFacebook';
  }

    
    register(user: RegisterViewModel) {
      return this.http.post(`/account/register`, user);
    }

     logout() {
        // remove user from local storage to log user out
      // localStorage.removeItem('currentUser');
      return this.http.post(`/account/logout`,"");
  }
}
