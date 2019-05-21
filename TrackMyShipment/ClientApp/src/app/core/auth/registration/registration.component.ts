import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from 'src/app/core/auth/authService';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(private router: Router, private authService: AuthService) { }


  ngOnInit() {
    if (this.authService.checkLogin()) {
      this.router.navigate(["main"]);
    }
  }

  registration(form: NgForm) {
    console.log(form);
    this.authService.registration(form);
  }

}
