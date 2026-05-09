using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EF_core_04
{
    public static class Services
    {
   
        public static void AddCustomer(AppDBContext _context)
        {
            try
            {
                Console.Write("Full Name: ");
                var name = Console.ReadLine();

                Console.Write("Email: ");
                var email = Console.ReadLine();

                Console.Write("Phone Number: ");
                var phone = Console.ReadLine();

                Console.Write("Address: ");
                var address = Console.ReadLine();

                Console.Write("Customer Type: ");
                var type = Console.ReadLine();

                Console.Write("National ID: ");
                var nationalIdInput = Console.ReadLine();

                Console.Write("Date of Birth (yyyy-mm-dd): ");
                var dobInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name) ||
                    string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(phone) ||
                    string.IsNullOrWhiteSpace(address) ||
                    string.IsNullOrWhiteSpace(type))
                {
                    Console.WriteLine("All fields are required!");
                    return;
                }

                if (!int.TryParse(nationalIdInput, out int nationalId))
                {
                    Console.WriteLine("Invalid National ID!");
                    return;
                }

                if (!DateTime.TryParse(dobInput, out DateTime dob))
                {
                    Console.WriteLine("Invalid Date!");
                    return;
                }

                Customer customer = new Customer
                {
                    FullName = name,
                    Email = email,
                    PhoneNumber = phone,
                    Address = address,
                    CustomerType = type,
                    NationalId = nationalId,
                    DateOfBirth = dob
                };

                _context.Customers.Add(customer);
                _context.SaveChanges();

                Console.WriteLine("Customer added successfully!");
                Console.WriteLine("your Id: " + customer.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

 
        public static void OpenAccount(AppDBContext _context)
        {
            try
            {
                Console.Write("Customer Id: ");

                if (!int.TryParse(Console.ReadLine(), out int customerId))
                {
                    Console.WriteLine("Invalid customer id");
                    return;
                }

                var customer = _context.Customers.Find(customerId);

                if (customer == null)
                {
                    Console.WriteLine("Customer not found");
                    return;
                }

                Console.Write("Account Type: ");
                var accountType = Console.ReadLine();

                Console.Write("Branch Code: ");
                var branchCode = Console.ReadLine();

                var branch = _context.Branches
                    .FirstOrDefault(b => b.Code == branchCode);

                if (branch == null)
                {
                    Console.WriteLine("Branch not found");
                    return;
                }

                Account account = new Account
                {
                    AccountType = accountType,
                    OpeningDate = DateTime.Now,
                    Balance = 0,
                    BranchCode = branch.Code,
                    IsActive = true
                };

                _context.Accounts.Add(account);
                _context.SaveChanges();

                AccountCustomers accountCustomer = new AccountCustomers
                {
                    AccountNumber = account.AccountNumber,
                    CustomerId = customerId,
                    OwnershipDate = DateTime.Now,
                    OwnershipType = "Primary",
                    AccountStatus = "Active"
                };

                _context.AccountCustomers.Add(accountCustomer);
                _context.SaveChanges();

                Console.WriteLine("Account opened successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public static void UpdateAccountStatus(AppDBContext _context)
        {
            try
            {
                Console.Write("Enter Account Number: ");

                if (!int.TryParse(Console.ReadLine(), out int accNumber))
                {
                    Console.WriteLine("Invalid account number");
                    return;
                }

                var account = _context.Accounts.Find(accNumber);

                if (account == null)
                {
                    Console.WriteLine("Account not found");
                    return;
                }

                Console.Write("Set Status (Active/Inactive): ");
                var status = Console.ReadLine();

                account.IsActive =
                    status?.ToLower() == "active";

                var accountCustomer = _context.AccountCustomers
                    .FirstOrDefault(ac => ac.AccountNumber == accNumber);

                if (accountCustomer != null)
                {
                    accountCustomer.AccountStatus =
                        account.IsActive ? "Active" : "Inactive";
                }

                _context.SaveChanges();

                Console.WriteLine("Account status updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public static void RemoveAccount(AppDBContext _context)
        {
            try
            {
                Console.Write("Enter Account Number: ");

                if (!int.TryParse(Console.ReadLine(), out int accNumber))
                {
                    Console.WriteLine("Invalid account number");
                    return;
                }

                var account = _context.Accounts
                    .Include(a => a.AccountCustomers)
                    .Include(a => a.Transactions)
                    .FirstOrDefault(a => a.AccountNumber == accNumber);

                if (account == null)
                {
                    Console.WriteLine("Account not found");
                    return;
                }

                _context.AccountCustomers.RemoveRange(account.AccountCustomers);

                _context.Transactions.RemoveRange(account.Transactions);

                _context.Accounts.Remove(account);

                _context.SaveChanges();

                Console.WriteLine("Account removed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ListCustomers(AppDBContext _context)
        {
            var customers = _context.Customers
                .Include(c => c.AccountCustomers)
                .ThenInclude(ac => ac.Account)
                .ThenInclude(a => a.Branch)
                .ToList();

            foreach (var customer in customers)
            {
                Console.WriteLine("================================");

                Console.WriteLine($"Customer Id: {customer.Id}");
                Console.WriteLine($"Name: {customer.FullName}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine($"Phone: {customer.PhoneNumber}");

                Console.WriteLine("Accounts:");

                foreach (var ac in customer.AccountCustomers)
                {
                    Console.WriteLine($"   Account Number : {ac.Account?.AccountNumber}");
                    Console.WriteLine($"   Type           : {ac.Account?.AccountType}");
                    Console.WriteLine($"   Balance        : {ac.Account?.Balance}");
                    Console.WriteLine($"   Branch         : {ac.Account?.Branch?.Name}");
                    Console.WriteLine($"   Status         : {ac.AccountStatus}");
                    Console.WriteLine("--------------------------------");
                }
            }
        }
    }
}


