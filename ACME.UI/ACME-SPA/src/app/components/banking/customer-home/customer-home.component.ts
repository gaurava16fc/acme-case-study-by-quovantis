import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, of, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Customer } from 'src/app/_models/customer.model';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { CustomerService } from 'src/app/_services/customer.service';

@Component({
  selector: 'app-customer-home',
  templateUrl: './customer-home.component.html',
  styleUrls: ['./customer-home.component.css']
})
export class CustomerHomeComponent implements OnInit , OnDestroy {
  customers: Customer[];
  searchByCustomerName: string;
  routeDataSub: Subscription;

  constructor(
        private route: ActivatedRoute,
        private alertify: AlertifyService,
        private customerService: CustomerService,
        private router: Router
    ) {}

  ngOnInit() {
    // console.log('Inside ngOnInit()....');
    this.searchByCustomerName = '';
    this.routeDataSub = this.route.data.subscribe(data => {
        this.customers = data['customers'];
      });
  }

  ngOnDestroy() {
    if (this.routeDataSub) {
      this.routeDataSub.unsubscribe();
    }

    // if (this.deleteCustomerSub) {
    //   this.deleteCustomerSub.unsubscribe();
    // }
  }

  viewCustomerAccounts(id: number) {
    // this.router.navigateByUrl('customers/bankaccounts/' + id, {relativeTo: this.route});
    this.router.navigate(['bankaccounts/' + id], {relativeTo: this.route});
  }

  onDeleteEmployee(id: number) {
    // this.alertify.confirm('Are you sure you want to delete an employee with Id # ' + id + '?', () => {
    //   this.deleteCustomerSub = this.customerService.deleteCustomer(id).subscribe((data) => {
    //       this.alertify.success('Customer Id# {' + id + '} has been deleted successfully!');
    //       this.refreshData();
    //     },
    //     error => {
    //       this.alertify.error(error);
    //     });
    // });
  }

  // refreshData() {
  //   this.getCustomerLists().subscribe((data: Customer[]) => {
  //     this.customers = data;
  //     // this.noDataFound = false;
  //   });
  // }

  // getCustomerLists(): Observable<Customer[]> {
  //   return this.customerService.getCustomers().pipe(
  //     catchError(error => {
  //         this.alertify.error('Problem retrieving data at Customer List');
  //         return of(null);
  //     })
  //   );
  // }

  // onLockAccount(id: number, accountNumber: string): void {
  //   if (this.customerService.isLoggedUserWithEmployeeRole()) {
  //     accountNumber = 'ACMEIN9111000003';
  //     this.customerService.lockAccount(id, accountNumber).subscribe(() => {
  //       this.alertify.success('Account is locked successfully!');
  //     }, error => {
  //         this.alertify.error(error);
  //       });
  //   }
  //   else {
  //     this.alertify.error('Oops, you do not have enouogh permission!');
  //   }
  // }

  // onUnlockAccount(id: number, accountNumber: string): void {
  //   if (this.customerService.isLoggedUserWithEmployeeRole()) {
  //     accountNumber = 'ACMEIN9111000003';
  //     this.customerService.unlockAccount(id, accountNumber).subscribe(() => {
  //       this.alertify.success('Account is unlocked successfully!');
  //     }, error => {
  //         this.alertify.error(error);
  //       });
  //   }
  //   else {
  //     this.alertify.error('Oops, you do not have enouogh permission!');
  //   }
  // }
}
