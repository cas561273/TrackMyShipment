import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './authService';
import { IRequestResult } from '../models/Request';

@Injectable()
export class CarrierService {
  private _url = 'https://localhost:44395/api/carrier/';

  constructor(private http: HttpClient, private router: Router, private authService: AuthService) { }

  public requestCarrier() {
    let headers = this.authService.initAuthHeaders();
    return this.http.get<IRequestResult>(this._url + "GetCarriers", { headers });
  }

}
