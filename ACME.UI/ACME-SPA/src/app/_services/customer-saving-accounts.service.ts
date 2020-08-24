import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';

import { SavingAccount } from 'src/app/_models/saving-account.model';
import { AuthService } from 'src/app/_services/auth.service';
import { ActivatedRoute } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class CustomerSavingAccountsService {
  private apiBaseUrl = '';

  // Http Options
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(
        private http: HttpClient,
        private authService: AuthService,
        private route: ActivatedRoute
    ) {
      const customerId = +this.route.snapshot.params['id'];
      // if (customerId > 0) {
      //   this.customerId = customerId;
      // }
      this.apiBaseUrl = environment.apiBaseUrl + 'customers/';
      console.log('API URL: ' + this.apiBaseUrl);
  }

  getCustomerAccounts(custId: number): Observable<SavingAccount[]> {
    this.apiBaseUrl = environment.apiBaseUrl + 'customers/' + custId + '/SavingAccount/getActiveAccounts';
    return this.http.get<SavingAccount[]>(this.apiBaseUrl).pipe(
      map(result => {
        let savingAccountList: SavingAccount[] = [];
        for(let acct of result) {
          savingAccountList.push(acct);
        }
        return savingAccountList;
      }),
      catchError(this.handleError)
    );
  }

  isLoggedUserWithCustomerRole() {
    return this.authService.isCustomerRole();
  }

  isLoggedUserWithEmployeeRole() {
    return this.authService.isEmployeeRole();
  }

  // getCustomerAccount(id: number): Observable<SavingAccount>  {
  //   return this.http.get<Customer>(this.apiBaseUrl + id).pipe(
  //     map(result => {
  //       // console.log(result);
  //       let cust = new Customer();
  //       cust = result;
  //       return cust;
  //     }),
  //     catchError(this.handleError)
  //   );
  // }

  lockAccount(customerId: number, accountNumber: string) {
    const lockSavingAccountForCustomerUrl = this.apiBaseUrl + customerId + '/savingaccount/' + accountNumber + '/lock';
    return this.http.put(lockSavingAccountForCustomerUrl, {} );
  }

  unlockAccount(customerId: number, accountNumber: string) {
    const lockSavingAccountForCustomerUrl = this.apiBaseUrl + customerId + '/savingaccount/' + accountNumber + '/unlock';
    return this.http.put(lockSavingAccountForCustomerUrl, {} );
  }


  // Error handling...
  handleError(error: any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
 }
}
