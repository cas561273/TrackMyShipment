import { Component, OnInit } from '@angular/core';
import { Carrier } from "../../../models/Carrier";
import { CarrierService } from '../../shared/carrierService';
import { UserService } from 'src/app/user/shared/userService';

@Component({
  selector: 'app-my-carriers',
  templateUrl: './my-carriers.component.html',
  styleUrls: ['./my-carriers.component.css']
})
export class MyCarriersComponent implements OnInit {
  carriers: Carrier[];

  constructor(private carrierService:CarrierService,private userService:UserService) { }

  ngOnInit() {
    this.carrierService.getMyCarriers().subscribe((data) => {
      this.carriers = data.data as Carrier[];
    });
  }

  public subscribe(carrier) {
    this.userService.subscribe(carrier).subscribe((response) => {
      console.log(response.state);
      if (response.state === -1) {
        console.log("Unsubscribe");
        this.carriers = this.carriers.filter(c => c.id !== carrier.id);
      }

    });
  }
  }

