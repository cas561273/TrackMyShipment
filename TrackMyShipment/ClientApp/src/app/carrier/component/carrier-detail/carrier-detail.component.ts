import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { Carrier } from "../../../models/Carrier";
import { Router } from '@angular/router';
import { CarrierService } from '../../shared/carrierService';

@Component({
  selector: 'app-carrier-detail',
  templateUrl: './carrier-detail.component.html',
  styleUrls: ['./carrier-detail.component.css']
})
export class CarrierDetailComponent implements OnInit {

  userId: number;
  carrier: Carrier;

  constructor(private carrierService:CarrierService,private activatedRoute:ActivatedRoute,private router:Router) { }

  ngOnInit() {
    this.userId = this.activatedRoute.snapshot.params['id'];

    this.carrierService.getCarrierById(this.userId).subscribe(response => {
      this.carrier = response.data as Carrier;
      console.log(response.data);
    });

  }

  public save() {
    this.carrierService.putCarrier(this.carrier).subscribe(response => {
      if (response.state === 1)
      this.router.navigate(["main"]);

    });
  }

}
