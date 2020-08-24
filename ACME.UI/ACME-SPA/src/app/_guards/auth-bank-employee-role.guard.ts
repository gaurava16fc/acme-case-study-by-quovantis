import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private alertify: AlertifyService) {}
 canActivate(
  next: ActivatedRouteSnapshot,
  state: RouterStateSnapshot): boolean {
    if (this.authService.loggedIn()) {
      return true;
    }
    this.alertify.error('You are required to login first!');
    this.router.navigate(['/signin']);
  }
}

