using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationFormSyncer.Models.ViewModels
{
    public class PatientInfo
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public DateTime DOB { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
