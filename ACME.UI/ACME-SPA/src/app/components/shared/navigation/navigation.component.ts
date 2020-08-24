import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  model: any = {};
  userType = 'Super Admin';

  constructor(
      public authService: AuthService,
      private alertify: AlertifyService,
      private router: Router
    ) {}

  ngOnInit(): void {
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.alertify.message('Logged-out successfully!');
    this.router.navigate(['/signin']);
  }

}
