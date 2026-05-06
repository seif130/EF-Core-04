using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_core_04
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string Note { get; set; }

        public string TransactionType { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate {  get; set; }

        public Account? Account { get; set; }
        public int AccountNumber { get; set; }


    }
}
