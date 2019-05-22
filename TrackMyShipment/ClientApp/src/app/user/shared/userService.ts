import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../core/auth/authService';
import { IRequestResult  } from '../../models/IRequestResult';
import { Carrier } from '../../models/Carrier';
import { Person } from 'src/app/models/Person';
import { Address } from "../../models/Address";
import { Objective } from "../../models/Objective";

@Injectable()
export class UserService {
  private _url = 'https://localhost:44395/api/user/';
  constructor(private http: HttpClient, private authService: AuthService) { }


  public getUsersOfCarrier(carrierId: number) {
    let headers = this.authService.initAuthHeaders();
    return this.http.get<IRequestResult>(this._url + 'usersOfCarrier/' + carrierId, { headers });
  }

  public getAllCarrier() {
    let headers = this.authService.initAuthHeaders();
    return this.http.get<IRequestResult>(this._url + 'GetUsersCarrier', { headers });
  }

  public subscribe(carrier: Carrier) {
    let headers = this.authService.initAuthHeaders();
    return this.http.post<IRequestResult>(this._url + 'Subscribe/', carrier.id, { headers });
  }

  public addUserCarrier(userCarrier:Person,carrierId:Number) {
    let headers = this.authService.initAuthHeaders();
    return this.http.post<IRequestResult>(this._url + 'AddUserCarrier/' + carrierId ,userCarrier, { headers });
  }

  public getMyActiveAddress() {
    let headers = this.authService.initAuthHeaders();
    return this.http.get<IRequestResult>('https://localhost:44395/api/' + 'MyActiveAddress', { headers });
  }
  public getMyAllAddress() {
    let headers = this.authService.initAuthHeaders();
    return this.http.get<IRequestResult>('https://localhost:44395/api/' + 'MyAddress', { headers });
  }

  public addAddress(address:Address) {
    let headers = this.authService.initAuthHeaders();
    return this.http.post<IRequestResult>('https://localhost:44395/api/' + 'PutAddress',address, { headers });
  }

  public getMyTask() {
    let headers = this.authService.initAuthHeaders();
    return this.http.get<IRequestResult>('https://localhost:44395/api/objective/' + 'GetMyTask', { headers });
  }

  public takeTask(task:Objective) {
    let headers = this.authService.initAuthHeaders();
    return this.http.post<IRequestResult>('https://localhost:44395/api/objective/' + 'TakeTask',task.id, { headers });
  }

  public getWorkUser() {
    let headers = this.authService.initAuthHeaders();
    return this.http.get<IRequestResult>(this._url + 'GetWorkUsers', { headers });
  }

  public changeStatusTask(idTask: number) {
    let headers = this.authService.initAuthHeaders();
    return this.http.post<IRequestResult>('https://localhost:44395/api/objective/' + 'ChangeStatusTask', idTask, { headers });
  }

  public closeTask(idTask:number) {
    let headers = this.authService.initAuthHeaders();
    return this.http.post<IRequestResult>('https://localhost:44395/api/objective/' + 'CloseTask',idTask, { headers });
  }


  
}
