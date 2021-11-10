using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationFormSyncer.Models.ViewModels
{
    public class TestType
    {
        [Key]
        public int TestId { get; set; }
        public string Test { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal CoopPrice { get; set; }
    }
}
