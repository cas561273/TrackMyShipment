import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/authService';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {

  constructor(private authService: AuthService, private router: Router ) { }

  ngOnInit() {
    if (this.authService.checkLogin()) {
      this.authService.currentUser();
    }
    else {
      this.router.navigate(["login"]);
    }
  }
}

