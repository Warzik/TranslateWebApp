import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfirmedEmailComponent } from './confirmed-email/confirmed-email.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ForgotPasswordConfirmationComponent } from './forgot-password-confirmation/forgot-password-confirmation.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ResetPasswordConfirmationComponent } from './reset-password-confirmation/reset-password-confirmation.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ExternalLoginHandleExceptionComponent } from './external-login-handle-exception/external-login-handle-exception.component';
@NgModule({
  declarations: [
    ConfirmedEmailComponent,
    ConfirmEmailComponent,
    ForgotPasswordComponent,
    ForgotPasswordConfirmationComponent,
    LoginComponent,
    RegisterComponent,
    ResetPasswordComponent,
    ResetPasswordConfirmationComponent,
    ExternalLoginHandleExceptionComponent
  ],
  imports: [
    BrowserAnimationsModule,
    RouterModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule
  ],
  exports: [
ExternalLoginHandleExceptionComponent,
    ConfirmedEmailComponent,
    ConfirmEmailComponent,
    ForgotPasswordComponent,
    ForgotPasswordConfirmationComponent,
    LoginComponent,
    RegisterComponent,
    ResetPasswordComponent,
    ResetPasswordConfirmationComponent
  ]
})
export class AccountViewsModule { }
