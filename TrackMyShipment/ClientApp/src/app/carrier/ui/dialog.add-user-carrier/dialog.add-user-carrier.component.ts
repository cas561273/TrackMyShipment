import { Component, OnInit, Inject } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Person } from 'src/app/models/Person';
import { ActivatedRoute } from '@angular/router';
import { UsersCarrierComponent } from '../../component/users-carrier/users-carrier.component';
import { UserService } from 'src/app/user/shared/userService';

@Component({
  selector: 'app-dialog.add-user-carrier',
  templateUrl: './dialog.add-user-carrier.component.html',
  styleUrls: ['./dialog.add-user-carrier.component.css']
})
export class DialogAddUserCarrierComponent   {
  
   constructor(public dialogRef: MatDialogRef<DialogAddUserCarrierComponent>, @Inject(MAT_DIALOG_DATA) public data: Person,
   private userService:UserService,public activatedRoute:ActivatedRoute){}

 
   onNoClick(): void {
    this.dialogRef.close();
  }

  public addCarrier(form: NgForm) {
    this.userService.addUserCarrier(form.value,UsersCarrierComponent.userId ).subscribe((response) => {
      this.dialogRef.close();
    });
  }

}
