import { Component } from '@angular/core';
import { AuthService } from 'src/app/core/auth/authService';
import { DataSharingService } from 'src/app/services/dataSharing';
import { Person } from 'src/app/models/Person';
import { Router } from '@angular/router';
import { OnInit } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit{
  constructor(private router: Router, private authService: AuthService, private dataSharingService: DataSharingService) { }

  currentUser: Person;
  isUserLoggedIn: Boolean;

  public clearStorage() {
    this.authService.logout();
  }
  

  ngOnInit() {
    this.dataSharingService.currentUser.subscribe(user => {
      this.currentUser = user;
    });
    this.dataSharingService.isUserLoggedIn.subscribe(isValid => {
      this.isUserLoggedIn = isValid;
    });
  }
}
