import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../_services';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-client-header',
  templateUrl: './client-header.component.html',
  styleUrls: ['./client-header.component.scss']
})
export class ClientHeaderComponent implements OnInit {

  statusUser: any;
  isExpanded = false;
  isAdmin = false;
  openSidebar = true;
  isNavbarCollapsed = true;

  constructor(private http: HttpClient, private authenticationService: AuthenticationService) {
    //this.http.post<boolean>('/Account/UserIsAdmin', '').subscribe(
    //  data => {
    //    this.isAdmin = data;
    //  }
    //);
  }


  ngOnInit() {
    this.statusUser =this.authenticationService.currentUserValue;

  }


  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }


  openOrClose() {

    this.openSidebar = !this.openSidebar;

    if (this.openSidebar) {
      document.getElementById('sidebarCollapse').classList.add('active');
      document.getElementById('content').classList.add('active');
      document.getElementById('sidebar').classList.add('active');
    } else {
      document.getElementById('sidebarCollapse').classList.remove('active');
      document.getElementById('content').classList.remove('active');
      document.getElementById('sidebar').classList.remove('active');
    }
  }


  openOrCloseMobile() {

    this.isNavbarCollapsed = !this.isNavbarCollapsed;

    if (this.isNavbarCollapsed) {
      document.getElementById('mobile-menu').classList.add('active');
    } else {
      document.getElementById('mobile-menu').classList.remove('active');
    }
  }

 
}
