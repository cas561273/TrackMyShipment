import { Component, Inject } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { Person } from "../../../models/Person";
import { DialogAddUserCarrierComponent } from '../../ui/dialog.add-user-carrier/dialog.add-user-carrier.component';
import { MatDialog } from '@angular/material';
import { UserService } from 'src/app/user/shared/userService';


@Component({
  selector: 'app-users-carrier',
  templateUrl: '/users-carrier.component.html',
  styleUrls: ['/users-carrier.component.css']
})
export class UsersCarrierComponent {
  users: Person[];
  public static userId: number;

  constructor(private activatedRoute: ActivatedRoute, private userService: UserService,public dialog: MatDialog) {
  }

  openModal(): void {
    const dialogRef = this.dialog.open(DialogAddUserCarrierComponent, {
      width: '340px',
      height:'390',
    });

    //delete this
    dialogRef.afterClosed().subscribe(result => {

      this.userService.getUsersOfCarrier(UsersCarrierComponent.userId).subscribe(response => {
        this.users = response.data as Person[];
      });

    });
  }
  

  ngOnInit() {
    UsersCarrierComponent.userId = this.activatedRoute.snapshot.params['id'];
    this.userService.getUsersOfCarrier(UsersCarrierComponent.userId).subscribe(response => {
      this.users = response.data as Person[];
    });
     

    
  }
}



