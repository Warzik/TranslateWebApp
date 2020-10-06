import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { trigger, style, state, transition, animate, stagger, query } from '@angular/animations';
import { PasswordValidation } from '../../_helpers';
import { RegisterViewModel } from '../../_models';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss'],
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
export class ResetPasswordComponent implements OnInit {

  registerForm: FormGroup;
  loading = false;
  submitted = false;
  errors: string[];

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    private router: Router) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      password: ['', [Validators.required, Validators.pattern('^(?=.*?[A-Z])(?=.*?[a-z]).{6,}$')]],
      confirmPassword: ['']
    },
      { validator: PasswordValidation.MatchPassword });

    this.errors = [];
  }


  // convenience getter for easy access to form fields
  get f() { return this.registerForm.controls; }

  onSubmit() {
    this.submitted = true;

    this.errors = [];

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }

    const resetUrl = this.router.url + '&password=' + this.f.password.value + '&confirmPassword=' + this.f.confirmPassword.value;

    this.loading = true;
    this.http.post(resetUrl, '')
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate(['/account/reset-password-confirmation']);
        },
        error => {
          this.loading = false;
          this.errors = error[''];
        });
  }

}
