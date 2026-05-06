using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_core_04
{
    public class AccountCustomers
    {
        public int AccountNumber { get; set; }

        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public Account? Account { get; set; }


        public DateTime OwnershipDate { get; set; }
        public string OwnershipType { get; set; }
        public string AccountStatus { get; set; }


    }
}
