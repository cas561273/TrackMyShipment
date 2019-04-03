import { Component } from '@angular/core';
import { AuthService } from './services/authService';
import { Person } from './models/Person';
import { Router } from '@angular/router';
import { DataSharingService } from './services/dataSharing';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  currentUser: Person;
  isUserLoggedIn: boolean;

  constructor(private authService: AuthService, private router: Router, private dataSharingService: DataSharingService) {
 
  }

  ngOnInit() {
    console.log(this.dataSharingService.isUserLoggedIn)
      this.dataSharingService.isUserLoggedIn.subscribe( value => {
        this.isUserLoggedIn = value;
    });
    console.log(this.dataSharingService.currentUser)
    this.dataSharingService.currentUser.subscribe(value => {
      this.currentUser = value;
    })
  }
}
