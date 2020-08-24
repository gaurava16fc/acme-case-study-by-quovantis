import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';


import { Customer } from 'src/app/_models/customer.model';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { CustomerService } from 'src/app/_services/customer.service';

@Injectable()
export class CustomerListResolver implements Resolve<Customer[]> {
    constructor(
        private customerService: CustomerService,
        private alertify: AlertifyService,
        private router: Router
    ) {}


    resolve(route: ActivatedRouteSnapshot): Observable<Customer[]> {
        return this.customerService.getCustomers().pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data at Customer List');
                return of(null);
            })
        );
    }
}
