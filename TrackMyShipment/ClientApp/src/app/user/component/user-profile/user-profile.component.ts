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

  myAddress: Address;
  currentUser: Person;

  
  constructor(private userService:UserService,private dataSharingService:DataSharingService) { }

  ngOnInit() {
    this.userService.getMyActiveAddress().subscribe((response) => {
      this.myAddress = response.data as Address;
      console.log(response.data);
      console.log(this.myAddress);
      this.dataSharingService.currentUser.subscribe(user => {
        this.currentUser = user;

      });
    });
  }

  public editAddress(form: NgForm) {
    let address = form.value as Address;
    address.id = this.myAddress.id;
    this.userService.addAddress(address).subscribe((response) => {
      console.log(response.data);
    });
  }

}
