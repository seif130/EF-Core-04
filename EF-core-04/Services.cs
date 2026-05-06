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


                if (string.IsNullOrWhiteSpace(name) ||  string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone) ||
                    string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(type))
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
                    Console.WriteLine("Invalid Date of Birth!");
                    return;
                }

                Customer customer = new Customer
                {
                    FullName = name,
                    Email = email,
                    PhoneNumber = phone,
                    Address = address,
                    CustomerType = type,
                    NationalId = int.Parse(nationalIdInput),
                    DateOfBirth = DateTime.Parse(dobInput)
                };

                _context.Set<Customer>().Add(customer);
                _context.SaveChanges();

                Console.WriteLine("Customer added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        public static void OpenAccount(AppDBContext _context)
        {
            try
            {
                Console.Write("Account Type: ");
                var type = Console.ReadLine();

                Console.Write("Branch Code: ");
                var branchCode = Console.ReadLine();

                var branch = _context.Set<Branch>().FirstOrDefault(b => b.Code == branchCode);

                if (branch == null)
                {
                    Console.WriteLine("Branch not found");
                    return;
                }

                var account = new Account
                {
                    AccountType = type,
                    OpeningDate = DateTime.Now,
                    Balance = 0,
                    BranchCode = branch.Code
                };

                _context.Set<Account>().Add(account);
                _context.SaveChanges();

                Console.WriteLine("Account created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        public static void ListCustomers(AppDBContext _context)
        {
            var customers = _context.Set<Customer>()
                .Include(c => c.AccountCustomers)
                .ThenInclude(ca => ca.Account)
                .ToList();

            foreach (var c in customers)
            {
                Console.WriteLine($"Customer: {c.FullName}");

                foreach (var ca in c.AccountCustomers)
                {
                    Console.WriteLine($"  Account: {ca.Account?.AccountNumber}");
                }
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

                var account = _context.Set<Account>().Find(accNumber);

                if (account == null)
                {
                    Console.WriteLine("Account not found");
                    return;
                }

                _context.Set<Account>().Remove(account);
                _context.SaveChanges();

                Console.WriteLine("Account removed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        public static void UpdateAccountStatus(AppDBContext _context)
        {
            try
            {
                Console.Write("Enter Account Number:");
                if (!int.TryParse(Console.ReadLine(), out int accNumber))
                {
                    Console.WriteLine("Invalid account number");
                    return;
                }

                var account = _context.Set<Account>().Find(accNumber);

                if (account == null)
                {
                    Console.WriteLine("Account not found");
                    return;
                }

                Console.Write("Set Status (Active/Inactive): ");
                var status = Console.ReadLine();

                account.IsActive = status?.ToLower() == "active";

                _context.SaveChanges();

                Console.WriteLine("Account status updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
