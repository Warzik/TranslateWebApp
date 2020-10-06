import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { trigger, style, state, transition, animate, stagger, query } from '@angular/animations';
@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss'],
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
export class ForgotPasswordComponent implements OnInit {

  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  errors: string[];

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.pattern('[a-zA-Z0-9.-_]{1,}@[a-zA-Z.-]{2,}[.]{1}[a-zA-Z]{2,}')]],
    });


    this.errors = [];
  }


  get f() { return this.loginForm.controls; }



  onSubmit() {
    this.submitted = true;
    this.errors = [];

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }

    const Email = this.f.username.value;

    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    this.loading = true;
    this.http.post(`/Account/ForgotPassword`, JSON.stringify({ Email }), {headers})
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate(['/account/forgot-password-confirmation']);
        },
        error => {
          this.loading = false;
          this.errors = error[''];
        });
  }
}
