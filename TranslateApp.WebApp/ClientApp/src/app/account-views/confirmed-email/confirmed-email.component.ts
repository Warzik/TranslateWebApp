import { Component, OnInit } from '@angular/core';
import { trigger, style, state, transition, animate, stagger, query } from '@angular/animations';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-confirmed-email',
  templateUrl: './confirmed-email.component.html',
  styleUrls: ['./confirmed-email.component.scss'],
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
export class ConfirmedEmailComponent implements OnInit {
  loading = true;
  constructor(private router: Router, private http: HttpClient) {
    this.http.post(this.router.url, '').subscribe(data => {
      this.loading = false;
    });
  }

  ngOnInit() {
  }

}
