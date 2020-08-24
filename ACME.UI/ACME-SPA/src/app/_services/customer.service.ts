import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';

import { Customer } from 'src/app/_models/customer.model';
import { AuthService } from 'src/app/_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private apiBaseUrl = '';

  // Http Options
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(
        private http: HttpClient,
        private authService: AuthService
    ) {
    this.apiBaseUrl = environment.apiBaseUrl + 'customers/';
  }

  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.apiBaseUrl).pipe(
      map(result => {
        // console.log(result);
        let customerList: Customer[] = [];
        for(let cust of result) {
          customerList.push(cust);
        }
        return customerList;
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

  getCustomer(id: number): Observable<Customer>  {
    return this.http.get<Customer>(this.apiBaseUrl + id).pipe(
      map(result => {
        // console.log(result);
        let cust = new Customer();
        cust = result;
        return cust;
      }),
      catchError(this.handleError)
    );
  }

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
