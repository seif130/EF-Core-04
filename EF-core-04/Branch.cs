using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_core_04
{
    public class Branch
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        [InverseProperty(nameof(Manager.Branch))]
        public Manager Manager { get; set; }
        [ForeignKey(nameof(Manager))]
        public int ManagerId { get; set; }

        public ICollection<Account> Accounts { get; set; }



    }
}
