using Microsoft.EntityFrameworkCore;

namespace EF_core_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using AppDBContext db = new AppDBContext();

            #region part02

            db.Database.Migrate();

            //if (!db.Set<Manager>().Any())
            //{

            //    db.Set<Manager>().AddRange(
            //       new Manager
            //       { FullName = "Ahmed Hassan", PhoneNumber = "1234567890", Email = "ahmed@gmail.com", HireDate = DateTime.Now    },
            //       new Manager { FullName = "Sara Ali",  PhoneNumber = "0987654321", Email = "sara@gmail.com", HireDate = DateTime.Now   }
            //    );

            //    db.SaveChanges();
            //    Console.WriteLine("Managers added successfully");

            //}

            //if (!db.Set<Branch>().Any()) {

            //    db.Set<Branch>().AddRange( 
            //        new Branch { Code = "BR001", Name = "Cairo Branch", Address = "Nasr City" ,PhoneNumber = "111111111" , ManagerId = 4},
            //        new Branch { Code = "BR002", Name = "Alex Branch", Address = "Smouha", PhoneNumber = "2222222222", ManagerId = 5});

            //    db.SaveChanges();

            //    Console.WriteLine("Branches added successfully");


            //}


            #endregion



            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Open Account");
                Console.WriteLine("3. Update Account Status");
                Console.WriteLine("4. Remove Account from Customer");
                Console.WriteLine("5. List Customers");
                Console.WriteLine("0. Exit");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                       Services.AddCustomer(db);
                        break;
                    case "2":
                        Services.OpenAccount(db);
                        break;
                    case "3":
                        Services.UpdateAccountStatus(db);
                        break;
                    case "4":
                        Services.RemoveAccount(db);
                        break;
                    case "5":
                        Services.ListCustomers(db);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }

                Console.WriteLine("Press any key to return...");
                Console.ReadKey();



              
            }







            }
        }
    }

