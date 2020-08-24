import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

import { Customer } from 'src/app/_models/customer.model';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { CustomerService } from 'src/app/_services/customer.service';
import { SavingAccount } from 'src/app/_models/saving-account.model';


@Component({
  selector: 'app-customer-saving-accounts',
  templateUrl: './customer-saving-accounts.component.html',
  styleUrls: ['./customer-saving-accounts.component.css']
})
export class CustomerSavingAccountsComponent implements OnInit , OnDestroy {
  @Input() selectedCustomer: Customer;
  customer: Customer;
  savingAccounts: SavingAccount[];
  routeDataSub: Subscription;

  constructor(
        private route: ActivatedRoute,
        private alertify: AlertifyService,
        private customerService: CustomerService,
        private router: Router
    ) {}

  ngOnInit() {
    this.customer = this.selectedCustomer;
    // console.log('Inside ngOnInit()....');
    this.routeDataSub = this.route.data.subscribe((data: SavingAccount[]) => {
        // console.log('inside ngOnInit() method...');
        this.savingAccounts = data['savingAccounts'];
        // this.savingAccounts = data;
      });
  }

  ngOnDestroy() {
    if (this.routeDataSub) {
      this.routeDataSub.unsubscribe();
    }
  }

  onLockAccount(id: number, accountNumber: string): void {
    if (this.customerService.isLoggedUserWithEmployeeRole()) {
      // accountNumber = 'ACMEIN9111000003';
      this.customerService.lockAccount(id, accountNumber).subscribe(() => {
        this.alertify.success('Account is locked successfully!');
      }, error => {
          this.alertify.error(error);
        });
    }
    else {
      this.alertify.error('Oops, you do not have enouogh permission!');
    }
  }

  onUnlockAccount(id: number, accountNumber: string): void {
    if (this.customerService.isLoggedUserWithEmployeeRole()) {
      // accountNumber = 'ACMEIN9111000003';
      this.customerService.unlockAccount(id, accountNumber).subscribe(() => {
        this.alertify.success('Account is unlocked successfully!');
      }, error => {
          this.alertify.error(error);
        });
    }
    else {
      this.alertify.error('Oops, you do not have enouogh permission!');
    }
  }
}
