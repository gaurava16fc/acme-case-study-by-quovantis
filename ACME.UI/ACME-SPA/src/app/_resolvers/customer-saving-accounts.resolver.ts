// export class CustomerSavingAccountsResolver {
// }


import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';

import { SavingAccount } from 'src/app/_models/saving-account.model';
import { CustomerSavingAccountsService } from '../_services/customer-saving-accounts.service';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class CustomerSavingAccountsResolver implements Resolve<SavingAccount[]> {
    constructor(
        private customerSavingAccountsService: CustomerSavingAccountsService,
        private alertify: AlertifyService,
        private router: Router
    ) {}

    resolve(route: ActivatedRouteSnapshot): Observable<SavingAccount[]> {
        return this.customerSavingAccountsService.getCustomerAccounts(route.params['id']).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data at Customer Saving Accounts List');
                return of(null);
            })
        );
    }
}