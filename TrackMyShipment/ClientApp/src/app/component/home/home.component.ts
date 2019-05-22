import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

import { MatDialog } from '@angular/material/dialog';
import { Carrier } from 'src/app/models/Carrier';
import { DataSharingService } from 'src/app/services/dataSharing';
import { CarrierService } from 'src/app/carrier/shared/carrierService';
import { Person } from 'src/app/models/Person';
import { DialogOverviewComponent } from 'src/app/carrier/ui/dialog-overview/dialog-overview.component';
import { UserService } from 'src/app/user/shared/userService';
import notify from "devextreme/ui/notify";



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',

})
export class HomeComponent implements OnInit {

  carriers: Carrier[];
  currentUser: Person;
  activeUsers: any[];

  constructor(private router: Router, private http: HttpClient, private carrierService: CarrierService,private userService:UserService,
    private dataSharingService: DataSharingService, public dialog: MatDialog) { }


  ngOnInit() {
    this.refresh();
    this.dataSharingService.currentUser.subscribe(user => {
      this.currentUser = user;
        this.userService.getWorkUser().subscribe(response => {
          this.activeUsers = response.data;
          console.log(this.activeUsers);
        });
    });

  }

  public refresh() {
    this.carrierService.requestCarriers().subscribe((data) => {
      let requestCarriers = data.data as Carrier[];
      this.carriers = requestCarriers;
    });
  }

  public delete(carrier) {
    this.carrierService.deleteCarrier(carrier).then(
      res => {
        if (res.state) {
          console.log("complete");
          this.carriers = this.carriers.filter(c => c.id !== carrier.id);
          Promise.resolve();
        }
      });
  }

  public usersOfCarrier(carrier) {
    console.log("redirect");
    this.router.navigate(["carrier/"+ carrier.id]);
  }


  public changeStatus(carrier) {
    this.carrierService.changeStatus(carrier).subscribe((response) => {
      console.log(response.state);
    if (response.state === 1) {
      let indexOf = this.carriers.indexOf(carrier);
      this.carriers[indexOf].status = !this.carriers[indexOf].status;
    }
  });
  }
  public closeTask(idTask: number,id) {
    this.userService.closeTask(idTask).subscribe((response) => {
      if (response.state === 1) {
        this.activeUsers[id].status = "done";
      }
    });
  }
  public subscribe(carrier) {
    this.userService.subscribe(carrier).subscribe((response) => {
      console.log(response);
    });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(DialogOverviewComponent, {
      width: '340px',
      height:'450',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result.value);
    });
  }

  identify(index) {
    return index;
  }
}
