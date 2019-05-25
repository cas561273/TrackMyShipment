import { Component, OnInit, Inject } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { CarrierService } from '../../../../../carrier/shared/carrierService';

@Component({
  selector: 'dialog.add-task',
  templateUrl: './dialog.add-task.component.html',
})
export class DialogTaskComponent  {
  
  constructor(public dialogRef: MatDialogRef<DialogTaskComponent>, @Inject(MAT_DIALOG_DATA) public data,
    private carrierService: CarrierService, public activatedRoute: ActivatedRoute) { }

 
   onNoClick(): void {
    this.dialogRef.close();
  }

  public addTask(form: NgForm) {
    console.log(form.value);
    this.carrierService.addTask(form.value).subscribe((response) => {
      this.dialogRef.close();
    });
  }

}
