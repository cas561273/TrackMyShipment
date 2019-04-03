import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient} from '@angular/common/http'
import { Router } from '@angular/router';

import { AuthService } from 'src/app/services/authService';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],

})
export class LoginComponent implements OnInit{

  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit() {
    if (this.authService.checkLogin()) {
      this.router.navigate(["main"]);
    }
  }

  login(form: NgForm) {
    this.authService.login(form);
  }

}
