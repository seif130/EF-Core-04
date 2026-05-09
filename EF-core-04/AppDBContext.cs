using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_core_04
{
    public class AppDBContext : DbContext
    {
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<AccountCustomers> AccountCustomers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=EF-Core-04;Trusted_Connection=True; TrustServerCertificate=True;");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Branch>()
    .HasOne(b => b.Manager)
    .WithOne(m => m.Branch)
    .HasForeignKey<Branch>(b => b.ManagerId);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Branch)
                .WithMany(b => b.Accounts)
                .HasForeignKey(a => a.BranchCode);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountNumber);

            modelBuilder.Entity<AccountCustomers>()
     .HasKey(ac => new { ac.AccountNumber, ac.CustomerId });

            modelBuilder.Entity<AccountCustomers>()
                .Property(ac => ac.OwnershipDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<AccountCustomers>()
                .Property(ac => ac.OwnershipType)
                .HasMaxLength(50);

            modelBuilder.Entity<AccountCustomers>()
                .Property(ac => ac.AccountStatus)
                .HasMaxLength(200);

            modelBuilder.Entity<AccountCustomers>()
                .HasOne(ac => ac.Account)
                .WithMany(a => a.AccountCustomers)
                .HasForeignKey(ac => ac.AccountNumber);

            modelBuilder.Entity<AccountCustomers>()
                .HasOne(ac => ac.Customer)
                .WithMany(c => c.AccountCustomers)
                .HasForeignKey(ac => ac.CustomerId);

        }


                


        }
    }

