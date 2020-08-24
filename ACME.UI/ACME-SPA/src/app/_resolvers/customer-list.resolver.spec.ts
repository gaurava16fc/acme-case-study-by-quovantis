import { TestBed } from '@angular/core/testing';
import { CustomerListResolver } from './customer-list.resolver';

// describe('CustomerListResolver', () => {
//   it('should create an instance', () => {
//     expect(new CustomerListResolver()).toBeTruthy();
//   });
// });


describe('CustomerService', () => {
  let customerListResolver: CustomerListResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    customerListResolver = TestBed.inject(CustomerListResolver);
  });

  it('should be created', () => {
    expect(customerListResolver).toBeTruthy();
  });
});