using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZidiWCFService;

namespace RegistrationFormSyncer.Services
{
    public class DataUploaderDAL
    {
        int CenterID = 77;
        int StaffID = 12;
        string Password = "dee0c7263a914a1dfb5e0b0eeaba6421";

        public ZidiIService SetGlobalWcfConnection()
        {
            ZidiIService WcfConnection;

            WCFConnect _WCFConnect = new WCFConnect();

            return WcfConnection = _WCFConnect.GetWcfConnection(_WCFConnect.HttpBinding());
        }

        public ReturnType SetDeposit(DepositType tDeposit)
        {
            ReturnType returnf = new ReturnType();

            try
            {
                returnf = SetGlobalWcfConnection().SetDepositAsync(tDeposit, StaffID, CenterID, Password).Result;
            }
            catch (Exception ee)
            {
                //_ErrorLogClass.LogError(ee, "SetDeposit");
                //_ErrorLogClass.ShowDialog(ee.Message, "Saving Deposit");
            }

            return returnf;
        }

        public ReturnType UpdateDepositInvoiceConfirmation(DepositType tDeposit)
        {
            ReturnType returnf = new ReturnType();

            try
            {
                returnf = SetGlobalWcfConnection().UpdateDepositInvoiceConfirmationAsync(tDeposit, StaffID, CenterID, Password).Result;
            }
            catch (Exception xa)
            {
                // _ErrorLogClass.LogError(xa, "UpdateDepositInvoiceConfirmation");
                //_ErrorLogClass.ShowDialog(xa.Message, "Quote Confirmation");
            }

            return returnf;
        }

        public ReturnType SetDepositTransaction(DepositTransactionType DepositTransItem)
        {
            ReturnType returnf = new ReturnType();

            try
            {
                returnf = SetGlobalWcfConnection().SetDepositTransactionAsync(DepositTransItem, StaffID, CenterID, Password).Result;
            }
            catch (Exception xa)
            {
                // _ErrorLogClass.LogError(xa, "SetDepositTransaction");

                // _ErrorLogClass.ShowDialog(xa.Message, "Deposit Transaction Saving");
            }
            return returnf;
        }
        public ReturnType SetVisitCharge(VisitChargeType ServerVisitCharge)
        {
            ReturnType returnf = new ReturnType();

            try
            {
                returnf = SetGlobalWcfConnection().SetVisitChargeAsync(ServerVisitCharge, StaffID, CenterID, Password).Result;
            }
            catch (Exception xa)
            {
                //_ErrorLogClass.LogError(xa, "SetVisitCharge");

                //_ErrorLogClass.ShowDialog(xa.Message, "Visit Charge Update");
            }

            return returnf;
        }
        public ReturnType SetVisitChargeItemsBulk(VisitChargeItemType[] tVisitChargeItemList, int InsertOrUpdate)
        {
            ReturnType returnf = new ReturnType();

            try
            {
                returnf = SetGlobalWcfConnection().SetVisitChargeItemsBulkAsync(tVisitChargeItemList, InsertOrUpdate, StaffID, CenterID, Password, false).Result;
            }
            catch (Exception ee)
            {
                //_ErrorLogClass.LogError(ee, "Visit Charge Bulk");
                //_ErrorLogClass.ShowDialog(ee.Message, "Visit Charge Bulk");
            }

            return returnf;
        }
        public ReturnType SendMessage(Contact Contacts, string message, int Type = 0)
        {

            ReturnType returnf = new ReturnType();

            try
            {
                returnf = SetGlobalWcfConnection().SendSmsNewAsync(Contacts, message, Type, StaffID, CenterID, Password).Result;

                if (returnf.return_code > 0)
                {
                    // _dalclass.SetCenters ();
                }
            }
            catch (Exception ex)
            {
                // ErrorLogging.LogException(ex, DateTime.Now);
            }

            return returnf;
        }
        public ReturnType SetPatientRegistrationNew(PatientType ServerPatient)
        {
            ReturnType returnf = new ReturnType();

            try
            {
                returnf = SetGlobalWcfConnection().SetPatientRegistrationNewAsync(ServerPatient, StaffID, CenterID, Password, false).Result;
            }
            catch (Exception xa)
            {
                //_ErrorLogClass.LogError(xa, "SetPatientRegistrationNew");

                //_ErrorLogClass.ShowDialog(xa.InnerException.ToString(), "Registration");
            }

            return returnf;
        }
        public ReturnType SetVisitCycle(VisitCycleType tVisitcycle)
        {

            ReturnType returnf = new ReturnType();

            try
            {
                returnf = SetGlobalWcfConnection().SetVisitCycleAsync(tVisitcycle, StaffID, CenterID, Password).Result;
            }
            catch (Exception ee)
            {
                //_ErrorLogClass.LogError(ee, "SetVisitCycle");
                //_ErrorLogClass.ShowDialog(ee.Message, "Saving Visit Cycle");
            }


            return returnf;
        }
        public ReturnType UpdateVisitCyclePayerID(VisitCycleType tVisitcycle)
        {

            ReturnType returnf = new ReturnType();

            try
            {
                returnf = SetGlobalWcfConnection().UpdateVisitCyclePayerIDAsync(tVisitcycle, StaffID, CenterID, Password).Result;
            }
            catch (Exception ee)
            {
                //_ErrorLogClass.LogError(ee, "SetVisitCycle");
                //_ErrorLogClass.ShowDialog(ee.Message, "Saving Visit Cycle");
            }


            return returnf;
        }
        public ReturnType SetPatientPaymentAccount(PaymentAccountType tPaymentAccountItem)
        {

            ReturnType returnf = new ReturnType();

            try
            {
                returnf = SetGlobalWcfConnection().SetPatientPaymentAccountAsync(tPaymentAccountItem, StaffID, CenterID, Password).Result;
            }
            catch (Exception ee)
            {
                //_ErrorLogClass.LogError(ee, "SetVisitCycle");
                //_ErrorLogClass.ShowDialog(ee.Message, "Saving Visit Cycle");
            }


            return returnf;
        }
        public string SageGenerateCode(string Scheme)
        {
            string Code = string.Empty;

            try
            {
                Code = SetGlobalWcfConnection().SageGenerateCodeAsync("patient_payment_account_types", "scheme_code", 3, 2, Scheme).Result;


            }
            catch (Exception ee)
            {
                //_ErrorLogClass.LogError(ee, "SetSchemeType");

                //_ErrorLogClass.ShowDialog(ee.Message, "Saving Schemes");
            }

            return Code;
        }
        public ReturnType SetSchemeType(PaymentAccountType PaymentAccount)
        {
            ReturnType returf = new ReturnType();

            try
            {
                returf = SetGlobalWcfConnection().SetSchemeTypeAsync(PaymentAccount, StaffID, CenterID, Password).Result;

            }
            catch (Exception ee)
            {
                //_ErrorLogClass.LogError(ee, "SetSchemeType");

                //_ErrorLogClass.ShowDialog(ee.Message, "Saving Schemes");
            }

            return returf;
        }
        public ReturnType SetAccountLimitTrans(PaymentAccountType tPaymentAccountItem)
        {

            ReturnType returnf = new ReturnType();

            try
            {
                returnf = SetGlobalWcfConnection().SetAccountLimitTransAsync(tPaymentAccountItem, StaffID, CenterID, Password).Result;
            }
            catch (Exception ee)
            {
                //_ErrorLogClass.LogError(ee, "SetVisitCycle");
                //_ErrorLogClass.ShowDialog(ee.Message, "Saving Visit Cycle");
            }


            return returnf;
        }
        public int SetPatientReminders(Guid PatientID, DateTime ReminderDate, string Test)
        {
            int code = 0;

            PatientType tPatientReminder = new PatientType();
            tPatientReminder.patient_reminder_id = Guid.Empty;
            tPatientReminder.patient_reminder_type_id = 8;
            tPatientReminder.patient_reminder_date = ReminderDate.Date;
            tPatientReminder.patient_reminder_time = ReminderDate.TimeOfDay;
            tPatientReminder.assigned_doctor = 0;
            tPatientReminder.patient_reminder_comment = Test;
            tPatientReminder.patient_id = PatientID;
            tPatientReminder.center_id = CenterID;
            tPatientReminder.facility_id = 1;
            tPatientReminder.created_by = StaffID;
            tPatientReminder.created_date = DateTime.Now;
            tPatientReminder.updated_by = StaffID;
            tPatientReminder.update_date = DateTime.Now;
            tPatientReminder.active = 1;
            tPatientReminder.chronic = 0;
            tPatientReminder.reminder_external = 1;

            code = SavePatientReminders(tPatientReminder).return_code;

            if (code > 0)
            {
                //_ErrorLogClass.DisplayToastNotification("Patient Reminder saved succesfully");
            }

            return code;
        }
        public ReturnType SavePatientReminders(PatientType tPatientReminders)
        {
            ReturnType returnf = new ReturnType();

            try
            {
                returnf = SetGlobalWcfConnection().SetPatientRemindersAsync(tPatientReminders, StaffID, CenterID, Password).Result;
            }
            catch (Exception ee)
            {
                //_ErrorLogClass.LogError(ee, "SetPatientReminders");
                //_ErrorLogClass.ShowDialog(ee.Message, "Saving Patient Reminders");
            }

            return returnf;
        }
        public PaymentAccountType GetInvoicesGeneratedByCycleID(Guid CycleID)
        {
            PaymentAccountType InvoiceGenerated = new PaymentAccountType();

            try
            {
                InvoiceGenerated = SetGlobalWcfConnection().GetInvoicesGeneratedByCycleIDAsync(CycleID, StaffID, CenterID, Password).Result;
            }
            catch (Exception ee)
            {
                //_ErrorLogClass.LogError(ee, "GetInvoicesGeneratedByCycleID");
                // _ErrorLogClass.ShowDialog(ee.Message, "Getting Invoice");
            }
            return InvoiceGenerated;
        }
        public ReturnType SetInvoices(PaymentAccountType paymentaccount)
        {
            ReturnType returnf = new ReturnType();

            try
            {
                returnf = SetGlobalWcfConnection().SetInvoicesAsync(paymentaccount, StaffID, CenterID, Password).Result;
            }
            catch (Exception ee)
            {
                // _ErrorLogClass.LogError(ee, "SetInvoices");
                // _ErrorLogClass.ShowDialog(ee.Message, "Generating Invoice");
            }

            return returnf;
        }


        public ReturnType SetPatientTests(LabTests tLabTests)
        {
            ReturnType returnf = new ReturnType();
            try
            {
                returnf = SetGlobalWcfConnection().SetPatientTestsAsync(tLabTests, StaffID, CenterID, Password).Result;
            }
            catch (Exception ee)
            {
                //  _ErrorLogClass.LogError(ee, "SetPatientTests");
                // _ErrorLogClass.ShowDialog(ee.Message, "Set Lab Tests");
            }

            return returnf;
        }
        public DepositType GetDepositByCycle(Guid CycleID)
        {
            DepositType tDeposit = new DepositType();

            try
            {
                if (CycleID != Guid.Empty)
                {
                    tDeposit = SetGlobalWcfConnection().GetDepositByCycleAsync(CycleID, StaffID, CenterID, Password).Result;
                }
            }
            catch (Exception ee)
            {
                //_ErrorLogClass.LogError(ee, "GetDepositByCycle");
                //_ErrorLogClass.ShowDialog(ee.Message, "Get Deposit");
            }

            return tDeposit;
        }
        public List<PublicVisitChargeItemType> GetPatientsWithPendingItemsNew(Guid CycleId, int Paid)
        {
            List<PublicVisitChargeItemType> tVisitChargeItemList = new List<PublicVisitChargeItemType>();

            try
            {
                tVisitChargeItemList = SetGlobalWcfConnection().GetPatientsWithPendingItemsNewAsync(CycleId, Paid, 1, StaffID, CenterID, Password).Result.ToList();
            }
            catch (Exception ee)
            {
                //_ErrorLogClass.LogError(ee, "UpdateDepositWaiver");
                //_ErrorLogClass.ShowDialog(ee.Message, "Saving Waiver");
            }

            return tVisitChargeItemList;
        }
        public ReturnType SetUpdateTests(LabTests tLabTest)
        {
            ReturnType returnf = new ReturnType();

            try
            {
                returnf = SetGlobalWcfConnection().SetUpdateTestsAsync(tLabTest, true, StaffID, CenterID, Password).Result;
            }
            catch (Exception ee)
            {
                //_ErrorLogClass.LogError(ee, "Visit Charge Bulk");
                //_ErrorLogClass.ShowDialog(ee.Message, "Visit Charge Bulk");
            }

            return returnf;
        }
    }
}
