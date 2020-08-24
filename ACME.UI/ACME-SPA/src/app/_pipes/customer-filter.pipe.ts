import { Pipe, PipeTransform } from '@angular/core';
import { Customer } from 'src/app/_models/customer.model';

@Pipe({
  name: 'customerFilter',
  pure: false
})
export class CustomerFilterPipe implements PipeTransform {

  transform(customers: Customer[], searchValue: string): Customer[] {
    if (searchValue) {
      searchValue = searchValue.toLowerCase().trim();
      return customers.filter(el => el.name.toLowerCase().indexOf(searchValue) !== -1);
    }
    return customers;
  }
}
