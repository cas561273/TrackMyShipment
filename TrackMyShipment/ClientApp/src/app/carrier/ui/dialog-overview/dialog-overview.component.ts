import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Carrier } from "../../../models/Carrier";
import { NgForm } from '@angular/forms';
import { DataSharingService } from "../../../services/dataSharing";
import { CarrierService } from '../../shared/carrierService';


@Component({
  selector: 'app-dialog-overview',
  templateUrl: './dialog-overview.component.html',
  styleUrls: ['./dialog-overview.component.css']
})


export class DialogOverviewComponent {

  carrier: Carrier;

  constructor(
    public dialogRef: MatDialogRef<DialogOverviewComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Carrier,public carrierService:CarrierService,public dataSharing:DataSharingService) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  public addCarrier(form: NgForm) {
    this.carrierService.putCarrier(form.value).subscribe((response) => {
      console.log(response.msg);
      this.carrier = response.data as Carrier;
      this.dialogRef.close();
    });
  }

}
