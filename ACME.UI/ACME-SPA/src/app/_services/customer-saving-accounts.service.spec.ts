import { TestBed } from '@angular/core/testing';

import { CustomerSavingAccountsService } from './customer-saving-accounts.service';

describe('CustomerSavingAccountsService', () => {
  let service: CustomerSavingAccountsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CustomerSavingAccountsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
