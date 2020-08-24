import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  titleApp: string = 'Admin Portal';

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    if (this.authService.isCustomerRole()) {
      this.titleApp = 'Customer Portal';
    } else if (this.authService.isEmployeeRole()) {
      this.titleApp = 'Employee Portal';
    }
  }

  loggedIn() {
    return this.authService.loggedIn();
  }
}
