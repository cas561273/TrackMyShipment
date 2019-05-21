import { Component } from '@angular/core';
import { UserService } from "../../user/shared/userService";
import { Address } from "../../models/Address";


@Component({
  selector: 'app-address',
  styleUrls: ['address.component.css'],
  templateUrl: 'address.component.html',
})
export class AddressComponent {
  myAddress: Address[];

  displayedColumns: string[] = ['position', 'city', 'streetLine1', 'streetLine2', 'state', 'zipCode'];
  dataSource;

  constructor(public userService: UserService) {}

  ngOnInit() {
    this.userService.getMyAllAddress().subscribe((response) => {
      this.dataSource = response.data as Address[];
    });
  }
}
