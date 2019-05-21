import { Component } from '@angular/core';
import { AuthService } from 'src/app/core/auth/authService';
import { DataSharingService } from 'src/app/services/dataSharing';
import { Person } from 'src/app/models/Person';
import { Router } from '@angular/router';
import { OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  constructor(private router: Router, private authService: AuthService, private dataSharingService: DataSharingService) { }

  currentUser: Person;


  ngOnInit() {
    this.dataSharingService.currentUser.subscribe(user => {
      this.currentUser = user;
    });
  }
}
