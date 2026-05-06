using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_core_04
{
    public class Account
    {
        [Key] 
        public int AccountNumber { get; set; }
        public string AccountType { get; set; }
        public DateTime OpeningDate { get; set; }

        public decimal Balance { get; set; }

        public Branch Branch { get; set; }

        public string BranchCode { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public ICollection<AccountCustomers> AccountCustomers { get; set; } = new List<AccountCustomers>();


    }
}
