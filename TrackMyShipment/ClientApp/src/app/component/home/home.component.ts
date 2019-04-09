import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { CarrierService } from 'src/app/services/carrierService';
import { Carrier } from 'src/app/models/Carrier';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',

})
export class HomeComponent implements OnInit {

  carriers: Carrier[];

  constructor(private router: Router, private http: HttpClient, private carrierService: CarrierService) { }

  ngOnInit() {
    this.carrierService.requestCarrier().subscribe((data) => {
      let requestCarriers = data.data as Carrier[];
      this.carriers = requestCarriers;
      console.log(data);
      console.log(data.msg);
    });
  }
}
