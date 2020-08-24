import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { AppComponent } from 'src/app/app.component';
import { environment } from 'src/environments/environment';

import { JwtModule } from '@auth0/angular-jwt';

import { HeaderComponent } from 'src/app/components/shared/header/header.component';
import { FooterComponent } from 'src/app/components/shared/footer/footer.component';
import { NavigationComponent } from 'src/app/components/shared/navigation/navigation.component';
import { PageNotFoundComponent } from 'src/app/components/shared/page-not-found/page-not-found.component';
import { CustomerHomeComponent } from 'src/app/components/banking/customer-home/customer-home.component';
import { EmployeeHomeComponent } from 'src/app/components/banking/employee-home/employee-home.component';
import { HomeComponent } from 'src/app/components/banking/home/home.component';
import { SignInComponent } from 'src/app/components/sign-in/sign-in.component';
import { StrictNumericDecimalDigtsDirective } from 'src/app/_directives/strict-numeric-decimal-digts.directive';
import { StrictNumericDigtsDirective } from 'src/app/_directives/strict-numeric-digts.directive';
import { CustomerFilterPipe } from 'src/app/_pipes/customer-filter.pipe';
import { ErrorInterceptorProvider } from 'src/app/_services/error.interceptor';

import { AlertifyService } from 'src/app/_services/alertify.service';
import { CustomerService } from 'src/app/_services/customer.service';
import { CustomerListResolver } from 'src/app/_resolvers/customer-list.resolver';
import { CustomerSavingAccountsComponent } from './components/banking/customer-saving-accounts/customer-saving-accounts.component';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    NavigationComponent,
    PageNotFoundComponent,
    CustomerHomeComponent,
    EmployeeHomeComponent,
    HomeComponent,
    SignInComponent,
    StrictNumericDecimalDigtsDirective,
    StrictNumericDigtsDirective,
    CustomerFilterPipe,
    CustomerSavingAccountsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: environment.whiteListedDomains,
        disallowedRoutes: environment.blackListedRoutes
      }
    })
  ],
  providers: [
    ErrorInterceptorProvider,
    CustomerService,
    AlertifyService,
    CustomerListResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
