import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/authService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {

  constructor(private authService: AuthService, private router: Router, ) { }

  ngOnInit() {
    if (this.authService.checkLogin()) {
      this.authService.currentUser();
    }
    else {
      this.router.navigate(["login"]);
    }
  }
}

