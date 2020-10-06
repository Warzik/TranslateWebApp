import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientHeaderComponent } from './client-header/client-header.component';
import { ClientFooterComponent } from './client-footer/client-footer.component';
import { HomeBodyComponent } from './home-body/home-body.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ContactFormComponent } from './contact-form/contact-form.component';
import { AppRoutingModule } from '../app-routing.module';
import { RouterModule } from '@angular/router';
import { ProjectService } from '../_services';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslatorBodyComponent } from './translator-body/translator-body.component';

@NgModule({
  declarations: [ClientHeaderComponent, ClientFooterComponent,
    HomeBodyComponent, ContactFormComponent, TranslatorBodyComponent,
    ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    CommonModule,
    AppRoutingModule,RouterModule
  ],
  exports: [ClientHeaderComponent, ClientFooterComponent, HomeBodyComponent, ContactFormComponent],
  providers: [
    ProjectService
  ]
})
export class ClientViewsModule { }
