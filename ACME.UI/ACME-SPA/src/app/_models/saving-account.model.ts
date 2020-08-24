export class SavingAccount {
    id?: number;
    accountNumber: string;
    isAccountLocked: boolean;
    customerId: number;
    minimumBalance: number;
    totalBalance: number;
    isActive: boolean;
    createdOn: Date;
    modifiedOn?: Date;
}
