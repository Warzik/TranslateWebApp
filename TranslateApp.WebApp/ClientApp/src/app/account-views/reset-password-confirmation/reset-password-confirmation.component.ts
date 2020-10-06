import { Component, OnInit } from '@angular/core';
import { trigger, style, state, transition, animate, stagger, query } from '@angular/animations';
@Component({
  selector: 'app-reset-password-confirmation',
  templateUrl: './reset-password-confirmation.component.html',
  styleUrls: ['./reset-password-confirmation.component.scss'],
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
export class ResetPasswordConfirmationComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
