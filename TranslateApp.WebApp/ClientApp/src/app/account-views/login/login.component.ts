import { Component, OnInit, AfterViewInit} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/_services';
import { trigger, style, state, transition, animate, stagger, query } from '@angular/animations';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  animations: [
    trigger('OnLoad', [
      transition('*=>*', [
        query('div', style({ opacity: '0.0'})),
        query(':self', style({transform: 'translateY(-30%)'})),
        query(':self', animate('250ms', style({transform: 'translateY(0)'}))),
        query('div', stagger('150ms', animate('150ms', style({ opacity: '1'}))), )
      ])
    ]),
  ]
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  errors: string[];
  rememberMe = false;


  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) { }




  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.pattern('[a-zA-Z0-9.-_]{1,}@[a-zA-Z.-]{2,}[.]{1}[a-zA-Z]{2,}')]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });

    // reset login status
    this.authenticationService.logout().subscribe();

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';

    this.errors = [];
  }


  rememberMeFunc() {
    this.rememberMe = !this.rememberMe;
  }


  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }



  googleLogin() {
    this.authenticationService.googleLogin();
  }

  facebookLogin() {
    this.authenticationService.facebookLogin();
  }

  onSubmit() {
    this.submitted = true;
    this.errors = [];

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.authenticationService.login(this.f.username.value, this.f.password.value, this.rememberMe)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate([this.returnUrl]);
        },
      error => {
        this.loading = false;
        this.errors = error[''];
      });
  }
}
