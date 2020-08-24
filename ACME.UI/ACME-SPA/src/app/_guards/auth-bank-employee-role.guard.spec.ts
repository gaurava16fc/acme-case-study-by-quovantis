import { TestBed } from '@angular/core/testing';

import { AuthBankEmployeeRoleGuard } from './auth-bank-employee-role.guard';

describe('AuthBankEmployeeRoleGuard', () => {
  let guard: AuthBankEmployeeRoleGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(AuthBankEmployeeRoleGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
