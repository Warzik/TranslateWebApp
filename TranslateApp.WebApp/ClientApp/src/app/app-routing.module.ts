import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './_guards';
import { LoginComponent } from './account-views/login/login.component';
import { RegisterComponent } from './account-views/register/register.component';
import { ConfirmEmailComponent } from './account-views/confirm-email/confirm-email.component';
import { ForgotPasswordComponent } from './account-views/forgot-password/forgot-password.component';
import { ForgotPasswordConfirmationComponent } from './account-views/forgot-password-confirmation/forgot-password-confirmation.component';
import { ResetPasswordConfirmationComponent } from './account-views/reset-password-confirmation/reset-password-confirmation.component';
import { ResetPasswordComponent } from './account-views/reset-password/reset-password.component';
import { ConfirmedEmailComponent } from './account-views/confirmed-email/confirmed-email.component';
import { HomeBodyComponent } from './client-views/home-body/home-body.component';
import { ClientHeaderComponent } from './client-views/client-header/client-header.component';
import { ClientFooterComponent } from './client-views/client-footer/client-footer.component';
import { ContactFormComponent } from './client-views/contact-form/contact-form.component';
import { TranslatorBodyComponent } from './client-views/translator-body/translator-body.component';
import { ExternalLoginHandleExceptionComponent } from './account-views/external-login-handle-exception/external-login-handle-exception.component';


const routes: Routes = [
  {
    path: 'home', canActivate: [AuthGuard],  children:
    [
      { path: '', component: HomeBodyComponent },
        { path: 'contact-form', component: ContactFormComponent },
        { path: 'translator/:id', component: TranslatorBodyComponent },
      { path: '', component: ClientHeaderComponent ,  outlet: 'header' },
      { path: '', component: ClientFooterComponent ,  outlet: 'footer' },
    ]
    // canActivate: [AuthGuard],
  },
  {
    path: 'account', children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'external', component: ExternalLoginHandleExceptionComponent },
      { path: 'confirm-email', component: ConfirmEmailComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      { path: 'forgot-password-confirmation', component: ForgotPasswordConfirmationComponent},
      { path: 'reset-password-confirmation', component: ResetPasswordConfirmationComponent },
    ]
  },

  { path: 'Account/ResetPassword', component: ResetPasswordComponent },
  { path: 'Account/ConfirmEmail', component: ConfirmedEmailComponent },
  // otherwise redirect to home
  { path: '**', redirectTo: 'home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
