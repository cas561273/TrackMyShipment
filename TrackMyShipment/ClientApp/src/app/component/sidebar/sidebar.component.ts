import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/authService';
import { DataSharingService } from 'src/app/services/dataSharing';
import { Person } from 'src/app/models/Person';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent {
  constructor(private router: Router, private authService: AuthService, private dataSharingService: DataSharingService) { }

  currentUser: Person;
  firstName: string;
  public clearStorage() {
    this.authService.logout();
  }

  ngOnInit() {
    this.dataSharingService.currentUser.subscribe(value => {
      this.currentUser = value;
    });
  }
}
