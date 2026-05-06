using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_core_04
{
    public class Manager
    {
    
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
    
         public string PhoneNumber { get; set; }
    
         public DateTime HireDate { get; set; }

        public Branch Branch { get; set; }

    }
}
