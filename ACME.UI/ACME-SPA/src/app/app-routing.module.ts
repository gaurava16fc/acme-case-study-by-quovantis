import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SignInComponent } from 'src/app/components/sign-in/sign-in.component';
import { HomeComponent } from 'src/app/components/banking/home/home.component';
import { CustomerHomeComponent } from 'src/app/components/banking/customer-home/customer-home.component';
import { EmployeeHomeComponent } from 'src/app/components/banking/employee-home/employee-home.component';
import { PageNotFoundComponent } from 'src/app/components/shared/page-not-found/page-not-found.component';
import { CustomerSavingAccountsComponent } from './components/banking/customer-saving-accounts/customer-saving-accounts.component';
import { CustomerListResolver } from 'src/app/_resolvers/customer-list.resolver';
import { CustomerSavingAccountsResolver } from 'src/app/_resolvers/customer-saving-accounts.resolver';


const routes: Routes = [
  { path: '', component: SignInComponent },
  { path: 'signin', component: SignInComponent },
  {
      path: '',
      runGuardsAndResolvers: 'always',
      // canActivate: [AuthGuard],
      children: [
          { path: 'home', component: HomeComponent },
          { path: 'customers/bankaccounts/:id', component: CustomerSavingAccountsComponent,
              resolve: {savingAccounts: CustomerSavingAccountsResolver},
              runGuardsAndResolvers: 'always'
          },
          { path: 'customers', component: CustomerHomeComponent, resolve: {customers: CustomerListResolver}  },
          { path: 'employees', component: EmployeeHomeComponent},
      ]
  },
  { path: '**', component: PageNotFoundComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
