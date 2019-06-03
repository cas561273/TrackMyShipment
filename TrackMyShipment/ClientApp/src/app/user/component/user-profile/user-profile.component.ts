import { Component, OnInit } from '@angular/core';
import { UserService } from "../../shared/userService";
import { Address } from "../../../models/Address";
import { Person } from "../../../models/Person";
import { DataSharingService } from "../../../services/dataSharing";
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  currentUser: Person;
  stats: any[];
  constructor(private userService: UserService, private dataSharingService: DataSharingService) { }

  ngOnInit() {
    this.userService.myProfileStats().subscribe(stats => {
      this.stats = stats.data;
      console.log(this.stats);
    });
    this.dataSharingService.currentUser.subscribe(user => {
      this.currentUser = user;
      console.log(this.currentUser);
    });
  }
}
