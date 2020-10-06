import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { trigger, style, state, transition, animate, stagger, query } from '@angular/animations';
import {AuthenticationService } from '../../_services';
import { PasswordValidation } from '../../_helpers';
import { RegisterViewModel } from '../../_models';

@Component({
  templateUrl: 'register.component.html',
  styleUrls: ['./register.component.scss'],
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
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  loading = false;
  submitted = false;
  errors: string[];


  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.pattern('[a-zA-Z0-9.-_]{1,}@[a-zA-Z.-]{2,}[.]{1}[a-zA-Z]{2,}')]],
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

    const user: RegisterViewModel = new RegisterViewModel();

    user.confirmPassword = this.f.confirmPassword.value;
    user.email = this.f.username.value;
    user.password = this.f.password.value;

    this.loading = true;
    this.authenticationService.register(user)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate(['/account/confirm-email']);
        },
        error => {
          this.loading = false;
          this.errors = error[''];
        });
  }
}
