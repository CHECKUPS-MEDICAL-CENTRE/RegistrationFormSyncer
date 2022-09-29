using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationFormSyncer.Models.ViewModels
{
    public  class DispatchVM
    {
        public string name { get; set; }
        public string order_no { get; set; }
        public string s_contact { get; set; }
        public string p_contact { get; set; }
        public string address { get; set; }
        public string note { get; set; }
        public int priority { get; set; }
        public string time { get; set; }
        public string date { get; set; }
        public string method { get; set; }


    }
}
