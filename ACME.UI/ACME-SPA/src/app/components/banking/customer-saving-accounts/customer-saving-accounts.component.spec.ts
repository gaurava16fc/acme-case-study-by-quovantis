import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerSavingAccountsComponent } from './customer-saving-accounts.component';

describe('CustomerSavingAccountsComponent', () => {
  let component: CustomerSavingAccountsComponent;
  let fixture: ComponentFixture<CustomerSavingAccountsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomerSavingAccountsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerSavingAccountsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
