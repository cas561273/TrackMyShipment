import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../core/auth/authService';
import { IRequestResult } from '../../models/IRequestResult';
import { Carrier } from '../../models/Carrier';

@Injectable()
export class CarrierService {

  private _url = 'https://localhost:44395/api/carrier/';

  constructor(private http: HttpClient, private authService: AuthService) { }

  public requestCarriers() {
    let headers = this.authService.initAuthHeaders();
    return this.http.get<IRequestResult>(this._url + "GetCarriers", { headers });
  }

  public deleteCarrier(carrier:Carrier):Promise<IRequestResult> {
    let headers = this.authService.initAuthHeaders();
    return this.http.post<IRequestResult>(this._url + "DeleteCarrier/", carrier.id, { headers }).toPromise();
  }

  public changeStatus(carrier: Carrier) {
    let headers = this.authService.initAuthHeaders();
    return this.http.post<IRequestResult>(this._url + "ChangeStatusCarrier/", carrier.id, { headers });
  }


  public getCarrierById(carrierId: number) {
    let headers = this.authService.initAuthHeaders();
    return this.http.get<IRequestResult>(this._url + "Carrier/" + carrierId, { headers });
  }

  public putCarrier(carrier:Carrier) {
    let headers = this.authService.initAuthHeaders();
    return this.http.put<IRequestResult>(this._url + "putcarrier/",carrier, { headers });
  }

  public getMyCarriers() {
    let headers = this.authService.initAuthHeaders();
    return this.http.get<IRequestResult>(this._url + "MyCarriers", { headers });
  }

  public addTask(task) {
    let headers = this.authService.initAuthHeaders();
    return this.http.put<IRequestResult>('https://localhost:44395/api/objective/' + "Add-Task", task,{ headers });
  }
}
