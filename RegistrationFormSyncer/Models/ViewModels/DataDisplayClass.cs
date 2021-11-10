using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationFormSyncer.Models.ViewModels
{
    public class DataDisplayClass
    {
        public Guid CycleID { get; set; }
        public Guid PatientID { get; set; }
        public decimal ChargeAmount { get; set; }
        public string FacilityNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public string Names { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime ReminderDate { get; set; }
        public Guid DepositID { get; set; }
        public Guid DepositTransID { get; set; }
        public bool Is_KQ_crew { get; set; }
        public int Ponea { get; set; }
        public string PcrFacility { get; set; }
        public string PcrResults { get; set; }
        public string flight_number { get; set; }
        public string travelling_to { get; set; }
        public string airline { get; set; }
        public string telephone { get; set; }
        public Guid patient_payment_account_id { get; set; }
        public Guid test_id { get; set; }
        public int lab_test_id { get; set; }
        public string lab_test_desc { get; set; }
        public string lab_test_code { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal Savings { get; set; }
        public string member_number { get; set; }
        public string external_facility { get; set; }
        public string reason { get; set; }
        public int for_travel { get; set; }
        public string test_type { get; set; }
        public string time_slot { get; set; }
        public Guid patient_payment_account_type_id { get; set; }
        public string patient_payment_account_type_desc { get; set; }
        public string SampleCollectionLocation { get; set; }
        public DataDisplayClass()
        {
            CycleID = Guid.Empty;
            PatientID = Guid.Empty;
            DepositTransID = Guid.Empty;
            DepositID = Guid.Empty;
            patient_payment_account_id = Guid.Empty;
            test_id = Guid.Empty;
            lab_test_id = 0;
        }
    }
}
