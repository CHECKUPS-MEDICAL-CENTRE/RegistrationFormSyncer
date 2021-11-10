using RegistrationFormSyncer.CheckDbContext;
using RegistrationFormSyncer.Models.ViewModels;
using RegistrationFormSyncer.RepositoryMixins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationFormSyncer.Services
{
    public class RegistrationDAL : Repository, IRegistrationDAL
    {
        public async Task<IEnumerable<PatientInfoVM>> GetUnsyncedPatients()
        {
            string query = @"select * from onlinepatients where IsSynced=0 and IsPayementRecorded=1";
            return await FindOptimisedAsync<PatientInfoVM>(query);

        }
        public async Task SyncPatient(object[] args)
        {
            string query = @"update onlinepatients set IsSynced=1, CycleId={1} where OnlinePatientId={0}";
            await UpdateAsync(query, args);
        }
        public async Task<PatientInfo> getPatient(object[] args)
        {
            string query = @"select patient_id Id, patient_first_name FirstName, dob ,
                            patient_tel Phone, email from patient_registration where patient_tel={0} or email={1}";
            return await FirstOrDefaultOptimisedAsync<PatientInfo>(query, args);
        }
        public async Task InsertIntoDocumentsLog(object[] args)
        {
            string query = @"insert into DocumentsLog(cycle_id,document_type,date_created,
is_sent,number_of_resends)values({0},{1},GETDATE(),1,0)";
            await UpdateAsync(query, args);
        }
        public async Task<TestType> GetTestTypesByInsurance(object[] args)
        {

            string query = @"select lt.lab_test_id TestId, lab_test_desc Test, clp.center_labtest_price_amount RetailPrice, clp.center_lab_corporate_price CoopPrice  from lab_test lt
inner join center_labtest_price clp on clp.lab_test_id= lt.lab_test_id
where clp.IsWebVisible=1 and clp.Scheme_ID='00000000-0000-0000-0000-000000000000' AND clp.active=1 and clp.lab_test_id={0}";



            return await FirstOrDefaultOptimisedAsync<TestType>(query,args);
        }
        public async Task<TestType> GetTestTypesByInsurance1(object[] args)
        {
            args[0] = Guid.Parse(args[0].ToString());
            string query = @"select lt.lab_test_id TestId, lab_test_desc Test, clp.center_labtest_price_amount RetailPrice, clp.center_lab_corporate_price CoopPrice  from lab_test lt
inner join center_labtest_price clp on clp.lab_test_id= lt.lab_test_id
where clp.IsWebVisible=1 and clp.Scheme_ID={0} AND clp.active=1 and clp.lab_test_id={1}
UNION
select lt.lab_test_id TestId, lab_test_desc Test, clp.center_labtest_price_amount RetailPrice, clp.center_lab_corporate_price CoopPrice  from lab_test lt
inner join center_labtest_price clp on clp.lab_test_id= lt.lab_test_id
where clp.IsWebVisible=1 and clp.Scheme_ID='00000000-0000-0000-0000-000000000000' AND clp.active=1 and clp.lab_test_id={1}";


            return await FirstOrDefaultOptimisedAsync<TestType>(query, args);
        }
        public async Task SavePaypalTransaction(object[] args)
        {
            string query = @"insert into PaypalTransactions (CycleID,OrderId,ExpectedAmount,AmountPaid,IsConfirmed,DateAdded)
                             values({0},{1},{2},{3},0,getdate())";
            await UpdateAsync(query, args);
        }
        public async Task UpdateFromQuoteToInvoice(object[] args)
        {
            string query = @"update deposit set invoice_confirmed=1 where cycle_id={0}";
            await UpdateAsync(query, args);
        }
        public async Task UpdatePatientCycleMapping(object[] args)
        {
            string query = @"update onlinepatients set cycle_id={1} where OnlinePatientId={0}";
            await UpdateAsync(query, args);
        }
        public RegistrationDAL(CheckupsDbContext context) : base(context)
        {

        }
    }
}
