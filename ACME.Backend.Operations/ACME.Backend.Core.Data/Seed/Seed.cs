using ACME.Backend.Core.Entities.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ACME.Backend.Core.Data.Seed
{
    public class Seed
    {
        public static void DumpSeedData(RepositoryDBContext context)
        {
            // User Roles Data...
            if (!context.UserRoles.Any())
            {
                var userRolesData = System.IO.File.ReadAllText(@"../ACME.Backend.Core.Data/Seed/JsonData/UserRoles.json");
                var userRoles = JsonConvert.DeserializeObject<List<UserRole>>(userRolesData);
                foreach (var roles in userRoles)
                {
                    context.UserRoles.Add(roles);
                }
                context.SaveChanges();
            }


            // User Data...
            if (!context.Users.Any())
            {
                var usersData = System.IO.File.ReadAllText(@"../ACME.Backend.Core.Data/Seed/JsonData/Users.json");
                var users = JsonConvert.DeserializeObject<List<User>>(usersData);
                foreach (var usr in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);
                    usr.PasswordHash = passwordHash;
                    usr.PasswordSalt = passwordSalt;
                    usr.UserName = usr.UserName.ToLower().Trim();
                    context.Users.Add(usr);
                }
                context.SaveChanges();
            }


            // Customers Data...
            if (!context.Customers.Any())
            {
                var customersData = System.IO.File.ReadAllText(@"../ACME.Backend.Core.Data/Seed/JsonData/Customers.json");
                var customers = JsonConvert.DeserializeObject<List<Customer>>(customersData);
                foreach (var customer in customers)
                {
                    context.Customers.Add(customer);
                }
                context.SaveChanges();
            }

            // Employees Data...
            if (!context.Employees.Any())
            {
                var empData = System.IO.File.ReadAllText(@"../ACME.Backend.Core.Data/Seed/JsonData/Employees.json");
                var employees = JsonConvert.DeserializeObject<List<Employee>>(empData);
                foreach (var emp in employees)
                {
                    context.Employees.Add(emp);
                }
                context.SaveChanges();
            }

            // Saving Account Data...
            if (!context.SavingAccounts.Any())
            {
                var savingAccount = System.IO.File.ReadAllText(@"../ACME.Backend.Core.Data/Seed/JsonData/SavingAccount.json");
                var savingAccounts = JsonConvert.DeserializeObject<List<SavingAccount>>(savingAccount);
                foreach (var svngAcc in savingAccounts)
                {
                    context.SavingAccounts.Add(svngAcc);
                }
                context.SaveChanges();
            }

        }


        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
