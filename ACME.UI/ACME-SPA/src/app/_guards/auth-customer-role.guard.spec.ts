import { TestBed } from '@angular/core/testing';

import { AuthCustomerRoleGuard } from './auth-customer-role.guard';

describe('AuthCustomerRoleGuard', () => {
  let guard: AuthCustomerRoleGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(AuthCustomerRoleGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
