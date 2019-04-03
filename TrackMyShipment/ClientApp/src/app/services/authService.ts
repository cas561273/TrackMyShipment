import { Component, OnInit, Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Person } from '../models/Person';
import { NgForm } from '@angular/forms';
import { IRequestResult } from '../models/Requst';
import { DataSharingService } from './dataSharing';


@Injectable()
export class AuthService implements CanActivate {
  private _url = 'https://localhost:44395/api/';
  invalidLogin: boolean;

  constructor(private http: HttpClient, private router: Router, private dataSharingService: DataSharingService) { }

  login(form: NgForm) {
    let credentials = JSON.stringify(form.value);
    this.http.post<IRequestResult>(this._url + "auth", credentials, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response => {
      let token = (<any>response).data.token;
      console.log(token);
      localStorage.setItem('jwt', token);
      this.invalidLogin = false;
      this.router.navigate(["/main"]);
    }, err => {
      this.invalidLogin = true;
    });
  }


  public canActivate() {
    if (this.checkLogin()) {
      return true;
    } else {
      this.router.navigate(['login']);
      return false;
    }
  }

  getLocalToken(): string {
    return localStorage.getItem('jwt');
  }

  initAuthHeaders(): HttpHeaders {
    let token = this.getLocalToken();
    if (token == null) throw "No token";

    let headers = new HttpHeaders()
      .set("Authorization", "Bearer " + token)
      .set("Content-Type", "application/json");
    return headers;
  }


  public checkLogin(): boolean {
    let token = localStorage.getItem('jwt');
    this.dataSharingService.isUserLoggedIn.next(token != null);
    return token != null;
  }


  currentUser() {
  let headers = this.initAuthHeaders();
  return this.http.get<Person>(this._url + 'shortInfo', { headers })
    .subscribe((data) => {
      let person: Person;
      person = data as Person;
      this.dataSharingService.currentUser.next(person);
      return person;
    });

  }

  public logout(): void {
    this.dataSharingService.currentUser.next(null);
    this.dataSharingService.isUserLoggedIn.next(false);

    localStorage.clear();
    this.router.navigate(["login"]);
  }
}
