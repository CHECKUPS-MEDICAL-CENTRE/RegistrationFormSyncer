using RegistrationFormSyncer.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RegistrationFormSyncer.Services
{
    public interface IRegistrationDAL
    {
        Task<IEnumerable<PatientInfoVM>> GetUnsyncedPatients();
        Task SyncPatient(object[] args);
        Task<PatientInfo> getPatient(object[] args);
        Task InsertIntoDocumentsLog(object[] args);
        Task<TestType> GetTestTypesByInsurance(object[] args);
        Task<TestType> GetTestTypesByInsurance1(object[] args);
        Task SavePaypalTransaction(object[] args);
        Task UpdateFromQuoteToInvoice(object[] args);
       Task UpdatePatientCycleMapping(object[] args);
    }
}