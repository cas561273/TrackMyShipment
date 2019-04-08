import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { AuthService } from './authService';
import { Person } from '../models/Person';
import { Carrier } from '../models/Carrier';
import { IRequestResult } from '../models/Requst';

@Injectable()
export class CarrierService {
  private _url = 'https://localhost:44395/api/carriers/';

  constructor(private http: HttpClient, private router: Router, private authService: AuthService) { }

  RequestCarrier() {
    let headers = this.authService.initAuthHeaders();
    return this.http.get<IRequestResult>(this._url + "GetCarriers", { headers });
  }

}
