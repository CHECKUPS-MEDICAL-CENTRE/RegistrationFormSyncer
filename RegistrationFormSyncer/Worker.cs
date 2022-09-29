using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RegistrationFormSyncer.Models.ViewModels;
using RegistrationFormSyncer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZidiWCFService;

namespace RegistrationFormSyncer
{
    public class Worker : BackgroundService
    {
        ZidiIServiceClient zidisvc = new ZidiIServiceClient();
        public DataDisplayClass tDataDisplayClass = new DataDisplayClass();
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceProvider;
        public Guid Visit_cycle_id;
        public int visitType=14;
        public string InvoiceNumber = "";

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var ct = _serviceProvider.CreateScope();
            //order detail
            var rd = ct.ServiceProvider.GetRequiredService<IRegistrationDAL>();
            
            while (!stoppingToken.IsCancellationRequested)
            {
                var patients = await rd.GetUnsyncedPatients();

                foreach (var patient in patients)
                {
                    //fetch price based on payment method
                    //var payable_amount=string.IsNullOrWhiteSpace(patient.SchemeId) ? await _setupService.GetPayableCashAmount(new object[] { patient.TypeOfTest }) : await _setupService.GetPayableInsuranceAmount(new object[] { Guid.Parse(patient.InsuranceId),patient.TypeOfTest });
                    //check currency and convert
                    //decimal convertedAmount;
                    //if (patient.Currency == "USD")
                    //{
                    //    patient.Amount = patient.Amount * 110;
                    //}
                    
                        
                    ReturnType returnf = new ReturnType();
                    DateTime InitialDate = patient.VisitDate;
                    DateTime InitialTime = DateTime.Now;
                    DateTime ReminderDate = DateTime.Now;
                    
                    DateTime VisitDate =  patient.VisitDate;
                    //DateTime DateNow = (string.IsNullOrWhiteSpace(row["Timestamp"].ToString())) ? DateTime.Now : DateTime.Parse((row["Timestamp"].ToString()), new CultureInfo("en-US"));
                    string travelling_to = string.IsNullOrWhiteSpace(patient.CountryTo) ? string.Empty : patient.CountryTo;
                    string flight_number = string.IsNullOrWhiteSpace(patient.FlightNumber) ? string.Empty : patient.FlightNumber;
                    string airline = string.IsNullOrWhiteSpace(patient.Airline) ? string.Empty : patient.Airline;
                    //string servicegroup = (string.IsNullOrWhiteSpace(row["service_group"].ToString())) ? string.Empty : row["service_group"].ToString();
                    string SampleCollectionLocation = string.IsNullOrWhiteSpace(patient.CollectionLocation) ? string.Empty : patient.CollectionLocation;

                    PatientType ServerPatient = new PatientType();

                    ServerPatient.patient_id = Guid.NewGuid();
                    ServerPatient.patient_first_name = string.IsNullOrWhiteSpace(patient.FirstName) ? string.Empty : patient.FirstName.ToUpper().Trim();
                    ServerPatient.patient_middle_name = string.IsNullOrWhiteSpace(patient.OtherNames) ? string.Empty: patient.OtherNames.ToUpper().Trim();
                    ServerPatient.patient_last_name = string.IsNullOrWhiteSpace(patient.Surname) ? string.Empty : patient.Surname.ToUpper().Trim();
                    ServerPatient.gender = string.IsNullOrWhiteSpace(patient.Gender) ? string.Empty : patient.Gender;
                    ServerPatient.dob = string.IsNullOrWhiteSpace(patient.DOB.ToString()) ? DateTime.Parse("01/01/1900") : DateTime.Parse(patient.DOB.ToString(), new CultureInfo("en-US"));
                    ServerPatient.patient_tel = string.IsNullOrWhiteSpace(patient.Telephone) ? string.Empty : patient.Telephone;
                    ServerPatient.nationality = string.IsNullOrWhiteSpace(patient.Nationality) ? string.Empty : patient.Nationality;
                    ServerPatient.email = string.IsNullOrWhiteSpace(patient.Email) ? string.Empty : patient.Email;
                    ServerPatient.id_no = string.IsNullOrWhiteSpace(patient.IdNumber) ? string.Empty : patient.IdNumber;
                    ServerPatient.other_id = string.IsNullOrWhiteSpace(patient.PassportId) ? string.Empty : patient.PassportId;
                    ServerPatient.occupation = string.IsNullOrWhiteSpace(patient.Occupation) ? string.Empty : patient.Occupation;
                    ServerPatient.citytown = string.IsNullOrWhiteSpace(patient.Town) ? string.Empty : patient.Town;
                    ServerPatient.county = string.IsNullOrWhiteSpace(patient.County) ? string.Empty : patient.County;
                    ServerPatient.referral_type = "corporate";
                    ServerPatient.referring_entity = airline;
                    ServerPatient.referral_location = string.Empty;
                    ServerPatient.facility_number_year = 0;
                    ServerPatient.channel = "CORPORATE";
                    ServerPatient.patient_address = string.Empty;
                    ServerPatient.patient_postal_code = string.Empty;
                    ServerPatient.patient_number = string.Empty;
                    ServerPatient.patient_fathers_name = string.Empty;
                    ServerPatient.patient_mothers_name = string.Empty;
                    ServerPatient.updated = "N";
                    ServerPatient.id_type = "PASSPORT";
                    ServerPatient.nhif_no = string.Empty;
                    ServerPatient.center_id = 77;
                    ServerPatient.staff_id = 12;
                    ServerPatient.date = VisitDate;
                    ServerPatient.package_code = "DS";
                    ServerPatient.package_id = Guid.Parse("976F4D24-8331-4683-9DD1-007163A791E5");
                    ServerPatient.patient_guardians_name = string.Empty;
                    ServerPatient.patient_next_of_kin = string.Empty;
                    ServerPatient.death_date = DateTime.Parse("01/01/1900");
                    ServerPatient.time_of_death = DateTime.Parse("01/01/1900");
                    ServerPatient.next_of_kin_relationship = string.Empty;
                    ServerPatient.religion = string.Empty;
                    ServerPatient.kin_telephone = string.Empty;
                    ServerPatient.kin_residence = string.Empty;
                    ServerPatient.birth_cert = string.Empty;
                    ServerPatient.image = new byte[0];
                    ServerPatient.patient_facility_number = string.Empty;
                    ServerPatient.alt_kin_telephone = string.Empty;
                    ServerPatient.emg_contact_name = string.Empty;
                    ServerPatient.emg_contact_relationship = string.Empty;
                    ServerPatient.emg_contact_residence = string.Empty;
                    ServerPatient.emg_contact_telephone = string.Empty;
                    ServerPatient.emg_contact_alt_telephone = string.Empty;
                    ServerPatient.mothers_patient_facility_id = string.Empty;
                    ServerPatient.unique_gov_no = string.Empty;
                    ServerPatient.alt_telephone = string.Empty;
                    ServerPatient.OPK = 0;
                    ServerPatient.image = new byte[0];
                    ServerPatient.kin_id_no = string.Empty;
                    ServerPatient.emg_contact_id_no = string.Empty;
                    ServerPatient.updated_by = 12;
                    ServerPatient.update_date = DateTime.Now;
                    ServerPatient.created_by = 12;
                    ServerPatient.created_date = DateTime.Now;
                    ServerPatient.outreach = 0;
                    ServerPatient.outreach_location = string.Empty;
                    ServerPatient.delivery_location = string.Empty;
                    ServerPatient.distance_travelled = string.Empty;
                    ServerPatient.has_smartphone = string.Empty;
                    ServerPatient.active = 1;
                    ServerPatient.marital_status = string.Empty;
                    // ServerPatient.visit_type_id = patient.IsHomeCollection == 1 ? 11 : 14;
                    if (patient.CollectionLocation.Contains("Walk-in"))
                    {
                        visitType = 14;
                    }
                    else if (patient.CollectionLocation.ToUpper().Contains("ROBERT"))
                    {
                        visitType = 2;
                    }
                    else if (patient.CollectionLocation.ToUpper().Contains("CITIBANK"))
                    {

                        visitType = 2;
                    }
                    else
                    {
                        visitType = 11;
                    }
                    ServerPatient.visit_type_id = visitType;
                    ServerPatient.facility_id = 2;
                    tDataDisplayClass.patient_payment_account_id = Guid.Empty;
                    tDataDisplayClass.FacilityNumber = ServerPatient.patient_facility_number;
                    tDataDisplayClass.Names = ServerPatient.patient_first_name;
                    tDataDisplayClass.VisitDate = VisitDate;
                    tDataDisplayClass.ReminderDate = ReminderDate;
                    tDataDisplayClass.flight_number = flight_number;
                    tDataDisplayClass.travelling_to = travelling_to;
                    tDataDisplayClass.airline = airline;
                    tDataDisplayClass.telephone = ServerPatient.patient_tel;
                    tDataDisplayClass.ReminderDate = VisitDate;
                    //tDataDisplayClass.Is_KQ_crew = ISKQCrew;
                    //tDataDisplayClass.Ponea = Ponea;
                    tDataDisplayClass.reason = string.IsNullOrWhiteSpace(patient.TestingReason) ? string.Empty : patient.TestingReason;
                    tDataDisplayClass.test_type = string.IsNullOrWhiteSpace(patient.TypeOfTestDescription) ? string.Empty : patient.TypeOfTestDescription;
                    tDataDisplayClass.member_number = string.IsNullOrWhiteSpace(patient.MemberNumber) ? string.Empty : patient.MemberNumber;
                    tDataDisplayClass.SampleCollectionLocation = SampleCollectionLocation;

                    tDataDisplayClass.ChargeAmount = patient.Amount;
                    tDataDisplayClass.RetailPrice = patient.Amount + 1500;
                    tDataDisplayClass.Savings = 1500;
                    tDataDisplayClass.lab_test_id = patient.TypeOfTest;
                    tDataDisplayClass.lab_test_code = "COV002";
                    tDataDisplayClass.time_slot = string.IsNullOrWhiteSpace(patient.CollectionSlot) ? string.Empty : patient.CollectionSlot;
                    var patExists = await rd.getPatient(new object[] { patient.Telephone, patient.Email });
                    if (patExists != null)
                    {
                        //patient with phone number or email exists, further check with first name

                        if (patExists.FirstName.ToUpper().Contains(patient.FirstName.ToUpper()))
                        {
                            //exact patient exists, start another visit
                            tDataDisplayClass.PatientID = patExists.Id;
                        }
                        else
                        {
                            //exact patient does not exist, create and start visit
                            //create and start visit 
                            returnf = new DataUploaderDAL().SetPatientRegistrationNew(ServerPatient);
                            if (returnf.return_code > 0)
                            {
                                Guid PatientID = Guid.Parse(returnf.return_identity_id);
                                tDataDisplayClass.PatientID = PatientID;

                            }
                        }
                    }
                    else
                    {
                        //create and start visit 
                        returnf = new DataUploaderDAL().SetPatientRegistrationNew(ServerPatient);
                        if (returnf.return_code > 0)
                        {
                            Guid PatientID = Guid.Parse(returnf.return_identity_id);
                            tDataDisplayClass.PatientID = PatientID;
                        }
                    }
                    //if (patient.PaymentMethod.Contains("Insurance"))
                    //{

                    //    //check whether patient exists in scheme
                    //    var patScheme = await _registrationService.PatientScheme(new object[] { tDataDisplayClass.PatientID, patient.SchemeId });
                    //    if (patScheme == null)
                    //    {
                    //        //add to scheme
                    //        CreateInsurancekQ(tDataDisplayClass);
                    //    }

                    //}

                    if (string.IsNullOrEmpty(patient.SchemeId))
                    {
                        StartVisit(tDataDisplayClass);
                    }
                    else
                    {
                        AddPatientToInsurance(tDataDisplayClass, patient.InsuranceId, patient.Insurance, patient.SchemeId, patient.Scheme);
                        await rd.UpdateFromQuoteToInvoice(new object[] { tDataDisplayClass.CycleID });
                    }
                    if (patient.PaymentMethod.Contains("VISA"))
                    {
                          await rd.SavePaypalTransaction(new object[] { tDataDisplayClass.CycleID, patient.TransactionGroup.ToString(), tDataDisplayClass.ChargeAmount, 0 });
                    }
                    //insert into documents log

                    await rd.InsertIntoDocumentsLog(new object[] { Visit_cycle_id, "consent_form" });
                    await rd.InsertIntoDocumentsLog(new object[] { Visit_cycle_id, "invoice" });

                    await rd.SyncPatient(new object[] { patient.OnlinePatientId, Visit_cycle_id,InvoiceNumber });
                    //if (patient.IsHomeCollection == 1)
                    //{
                    //    //send to dispatch app
                    //    DispatchVM dispatch = new DispatchVM();
                    //    dispatch.name = patient.FirstName +" "+ patient.OtherNames+" " + patient.Surname;
                    //    dispatch.order_no = InvoiceNumber;
                    //    dispatch.p_contact = patient.Telephone;
                    //    dispatch.address = patient.Town;
                    //    dispatch.note = "Covid Booking";
                    //    dispatch.priority = 1;
                    //    dispatch.time = patient.VisitDate.ToString("hh:mm");
                    //    dispatch.date = patient.VisitDate.ToString("dd-MM-yyyy");
                    //    dispatch.method = patient.PaymentMethod;

                    //    var postDispatchData= new { name=dispatch.name,}
                    //}
                    try
                    {
                        // create email message
                        EmailService emailService = new EmailService();
//                        string body = @"Thank you for registering for choosing us to do your covid test. 
//                            This service is currently unavailable as we are trying to improve our service
//                            We can however link you with one of our patners to have your test. Kindly call 0111050290
//";
                        string body = PopulateBody(patient.FirstName + " "+patient.OtherNames+ " "+patient.Surname, patient.IdNumber, patient.DOB.ToString("dd-MM-yyyy"), patient.CollectionLocation, patient.VisitDate.ToString("dd-MM-yyyy"),patient.Telephone,patient.TypeOfTestDescription,patient.PaymentMethod,patient.Amount,patient.CollectionSlot,patient.Currency);
                        emailService.SendWelcomeMessage(patient.Email,
                            "Covid 19 Test Registration",body
                            );
                        if (patient.IsHomeCollection == 1)
                        {
                            string body2= PopulateCCBody(patient.FirstName + " " + patient.OtherNames + " " + patient.Surname, patient.IdNumber, patient.DOB.ToString("dd-MM-yyyy"), patient.CollectionLocation, patient.VisitDate.ToString("dd-MM-yyyy"), patient.Telephone, patient.TypeOfTestDescription, patient.PaymentMethod, patient.Amount, patient.CollectionSlot,patient.Currency);
                            emailService.SendWelcomeMessage("pcrtest@checkupsmed.com",
                            "PCR Home sample collection",
                            body2);
                        }
                    }
                    catch(Exception Ex)
                    {
                        
                    }
                    
                }

                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        int AddPatientToInsurance(DataDisplayClass tDataDisplayClassx,string InsuranceId,string Insurance,string SchemeId, string Scheme)
        {
            int code = 0;

            int code2 = 0;

            PaymentAccountType tPaymentAccountItem = new PaymentAccountType();
            tPaymentAccountItem.patient_payment_account_id = Guid.NewGuid();
            tPaymentAccountItem.patient_id = tDataDisplayClassx.PatientID;
            tPaymentAccountItem.names = tDataDisplayClassx.Names;

            tPaymentAccountItem.payment_account_id = Guid.Parse(InsuranceId);
            tPaymentAccountItem.payment_account_desc = Insurance;
            tPaymentAccountItem.patient_payment_account_type_id = Guid.Parse(SchemeId);
            tPaymentAccountItem.patient_payment_account_type_desc = Scheme;


            tPaymentAccountItem.account_no = string.IsNullOrWhiteSpace(tDataDisplayClassx.member_number) ? "123" : tDataDisplayClassx.member_number;
            tPaymentAccountItem.account_valid_from = DateTime.Now;
            tPaymentAccountItem.account_valid_to = DateTime.Now.AddYears(1);
            tPaymentAccountItem.payment_account_limit_type_id = Guid.Parse("BAD694B0-0F0A-416C-9A0A-3C7F09B78A51");
            tPaymentAccountItem.payment_account_limit_type_desc = "Outpatient";
            tPaymentAccountItem.insurance_type_name = "Normal cover";
            tPaymentAccountItem.insurance_type_id = 1;
            tPaymentAccountItem.limit_amount = 100000;
            tPaymentAccountItem.account_upper_limit = 100000;
            tPaymentAccountItem.account_active = true;

            tPaymentAccountItem.created_by = 12;
            tPaymentAccountItem.created_on = DateTime.Now;
            tPaymentAccountItem.edited_by = 0;
            tPaymentAccountItem.edited_on = DateTime.Parse("01/01/1900");
            tPaymentAccountItem.statutory_amount = 100000;
            tPaymentAccountItem.update = false;
            tPaymentAccountItem.scheme_insurance_type_id = Guid.Empty;
            tPaymentAccountItem.isprimarymember = 1;
            tPaymentAccountItem.dependantrelationship = string.Empty;

            code = new DataUploaderDAL().SetPatientPaymentAccount(tPaymentAccountItem).return_code;

            if (code > 0)
            {
                code2 = new DataUploaderDAL().SetAccountLimitTrans(tPaymentAccountItem).return_code;

                if (code2 > 0)
                {
                    tDataDisplayClass.patient_payment_account_id = tPaymentAccountItem.patient_payment_account_id;

                    code = StartVisit(tDataDisplayClass);
                }
            }

            return code2;
        }
        int StartVisit(DataDisplayClass tDataDisplayClassx)
        {
            int returncode = 0;

            VisitCycleType ServerCycle = new VisitCycleType();
            ServerCycle.patient_id = tDataDisplayClassx.PatientID;
            ServerCycle.visit_type = visitType;
            ServerCycle.sub_que_details_id = 0;
            ServerCycle.case_type = Guid.Parse("FBE18B15-B8F4-4F25-BA8E-428D4C32F7A8");
            ServerCycle.case_type_id = Guid.Parse("FBE18B15-B8F4-4F25-BA8E-428D4C32F7A8");
            ServerCycle.cycle_id = Guid.NewGuid();
            Visit_cycle_id = ServerCycle.cycle_id;
            ServerCycle.center_id = 77;
            ServerCycle.cycle_completed = true;
            ServerCycle.over_five = "Y";
            ServerCycle.re_visit = "N";
            ServerCycle.telephone = tDataDisplayClassx.telephone;
            ServerCycle.patient_facility_number = tDataDisplayClassx.FacilityNumber;
            ServerCycle.flight_number = tDataDisplayClassx.flight_number;
            ServerCycle.travelling_to = tDataDisplayClassx.travelling_to;
            ServerCycle.airline = tDataDisplayClassx.airline;
            ServerCycle.chw_name = string.Empty;
            ServerCycle.chw_cell_no = string.Empty;
            ServerCycle.staff_id = 12;
            ServerCycle.visit_number = 1;
            ServerCycle.pending_page_title = string.Empty;
            ServerCycle.cycle_time_created = tDataDisplayClassx.VisitDate;
            ServerCycle.cycle_closed_time = DateTime.Parse("01/01/1900");
            ServerCycle.line_of_service_id = 0;
            ServerCycle.updated = "N";
            ServerCycle.patient_support_number = string.Empty;
            ServerCycle.marital_status = string.Empty;
            ServerCycle.occupation = string.Empty;
            ServerCycle.proffession = string.Empty;
            ServerCycle.education_level = string.Empty;
            ServerCycle.location = string.Empty;
            ServerCycle.sub_location = string.Empty;
            ServerCycle.village = string.Empty;
            ServerCycle.distance_travelled = string.Empty;
            ServerCycle.center_id_transferred_from = 0;
            ServerCycle.case_sent_to_staff_id = 12;
            ServerCycle.case_under_review = false;
            ServerCycle.ambulance_comment = string.Empty;
            ServerCycle.ambulance_no = string.Empty;
            ServerCycle.admission_date = DateTime.Parse("01/01/1900");
            ServerCycle.discharge_date = DateTime.Parse("01/01/1900");
            ServerCycle.stay_duration = 0;
            ServerCycle.inpatient = false;
            ServerCycle.discharged = false;
            ServerCycle.benefit_package_id = Guid.Empty;
            ServerCycle.active = 1;
            ServerCycle.concierge = visitType;
            ServerCycle.internal_nurse_id = 0;
            ServerCycle.facility_id = 2;
            ServerCycle.payer_id = (tDataDisplayClassx.patient_payment_account_id == Guid.Empty) ? Guid.Empty : tDataDisplayClassx.patient_payment_account_id;
            ServerCycle.insurance1 = (tDataDisplayClassx.patient_payment_account_id != Guid.Empty) ? 1 : 0;
            ServerCycle.reason = tDataDisplayClassx.reason;
            ServerCycle.test_type = tDataDisplayClassx.test_type;

            returncode = new DataUploaderDAL().SetVisitCycle(ServerCycle).return_code;


            if (returncode > 0)
            {
                tDataDisplayClass.CycleID = ServerCycle.cycle_id;

                if (tDataDisplayClassx.patient_payment_account_id != Guid.Empty)
                {
                    ActivateInsurance(tDataDisplayClass);
                }

                returncode = SaveDeposit(tDataDisplayClass);

                if (returncode > 0)
                {
                    int code = new DataUploaderDAL().SetPatientReminders(ServerCycle.patient_id, tDataDisplayClass.ReminderDate, tDataDisplayClass.lab_test_desc);

                    if (code > 0)
                    {
                        SendBookingSms(tDataDisplayClass);
                    }
                }
            }

            return returncode;
        }
        int ActivateInsurance(DataDisplayClass tDataDisplayClassx)
        {
            int returncode = 0;

            VisitCycleType CycleToUpdate = new VisitCycleType();
            CycleToUpdate.cycle_id = tDataDisplayClassx.CycleID;
            CycleToUpdate.payer_id = tDataDisplayClassx.patient_payment_account_id;
            CycleToUpdate.insurance1 = 1;
            CycleToUpdate.copay = 0;
            CycleToUpdate.payer_id2 = Guid.Empty;
            CycleToUpdate.insurance2 = 0;

            returncode = new DataUploaderDAL().UpdateVisitCyclePayerID(CycleToUpdate).return_code;

            return returncode;
        }
        void SendBookingSms(DataDisplayClass tDataDisplayClassx)
        {
            string Message = string.Empty;

            if (string.IsNullOrWhiteSpace(tDataDisplayClassx.time_slot))
            {
                Message = "Dear " + tDataDisplayClassx.Names + " Your Covid test appointment has been booked for " + tDataDisplayClassx.ReminderDate + " Call 0711 082 911 for any inquiries";
            }
            else
            {
                Message = "Dear " + tDataDisplayClassx.Names + " Your Covid test appointment has been booked for " + tDataDisplayClassx.ReminderDate.Date.ToString("dd-MM-yyyy") + "  " + tDataDisplayClassx.time_slot + " Call 0711 082 911 for any inquiries";

            }


            Contact tContact = new Contact();

            tContact.telephone = tDataDisplayClassx.telephone.Replace("+254", "0");
            tContact.contact_id = tDataDisplayClassx.PatientID;
            tContact.cycle_id = tDataDisplayClassx.CycleID;
            tContact.other_names = tDataDisplayClassx.Names;

            new DataUploaderDAL().SendMessage(tContact, Message);
        }
        int SaveDeposit(DataDisplayClass tDataDisplayClassx)
        {
            int returncode = 0;

            if (tDataDisplayClassx.CycleID != Guid.Empty)
            {
                DepositType tDeposit = new DepositType();
                tDeposit.deposit_id = Guid.NewGuid();
                tDeposit.amount = 0;
                tDeposit.approved = 0;
                tDeposit.approved_by = 0;
                tDeposit.cancelled = 0;
                tDeposit.cancelled_by = 0;
                tDeposit.center_id = 77;
                tDeposit.facility_id = 2;
                tDeposit.comment = string.Empty;
                tDeposit.cycle_id = tDataDisplayClassx.CycleID;
                tDeposit.date_approved = DateTime.Parse("01/01/1900");
                tDeposit.date_cancelled = DateTime.Parse("01/01/1900");
                tDeposit.date_created = tDataDisplayClassx.VisitDate;
                tDeposit.generated_pin = string.Empty;
                tDeposit.staff_id = 12;
                tDeposit.total_charge = tDataDisplayClassx.ChargeAmount;
                tDeposit.amount_paid = 0;
                tDeposit.two_step_invoice = 1;
                tDeposit.new_package = 1;
                tDeposit.charged_at_end = 0;
                tDeposit.refund = 0;
                tDeposit.running_balance = tDeposit.amount_paid - (tDeposit.total_charge + tDeposit.refund);
                tDeposit.insurance_rebate = 0;
                tDeposit.invoice_number = GenerateInvoice(tDataDisplayClassx.CycleID, tDataDisplayClassx.VisitDate);
                InvoiceNumber = tDeposit.invoice_number;
                tDeposit.waived = 0;
                tDeposit.waiver_used = 0;
                tDeposit.waiver_used_by = 0;
                tDeposit.waiver_used_date = DateTime.Parse("01/01/1900");
                tDeposit.preauth_requested = 0;
                tDeposit.preauth_request_date = DateTime.Parse("01/01/1900");
                tDeposit.preauth_request_comments = string.Empty;
                tDeposit.preauth_requested_by = 0;

                returncode = new DataUploaderDAL().SetDeposit(tDeposit).return_code;

                if (returncode > 0)
                {
                    tDataDisplayClass.InvoiceNumber = tDeposit.invoice_number;
                    tDataDisplayClass.DepositID = tDeposit.deposit_id;

                    if (tDataDisplayClassx.patient_payment_account_id != Guid.Empty)
                    {
                        int CODE = CloseInvoice(tDataDisplayClass.DepositID);
                    }

                    SaveLabTestData(tDataDisplayClass);
                }
            }

            return returncode;

        }
        int CloseInvoice(Guid DepositID)
        {


            int code = 0;

            try
            {
                DepositType tDeposit = new DepositType();

                tDeposit.deposit_id = DepositID;

                if (tDeposit != null && tDeposit.deposit_id != Guid.Empty)
                {
                    tDeposit.invoice_closed = 1;
                    tDeposit.invoice_closed_date = DateTime.Now;
                    tDeposit.invoice_closed_amount = 0;
                    tDeposit.invoice_closed_by = 12;
                    tDeposit.invoice_confirmed = 1;

                    code = new DataUploaderDAL().UpdateDepositInvoiceConfirmation(tDeposit).return_code;

                    if (code > 0)
                    {
                    }
                    //await _
                }
            }
            catch (Exception xa)
            {
                //ShowDialog(xa.Message, "Deposit New");
            }

            return code;
        }
        string GenerateInvoice(Guid CycleID, DateTime VisitDate)
        {
            string Message = string.Empty;

            var Invoice = new DataUploaderDAL().GetInvoicesGeneratedByCycleID(CycleID);

            if (Invoice.invoice_id == Guid.Empty)
            {
                PaymentAccountType paymentaccount = new PaymentAccountType();
                paymentaccount.created_by = 12;
                paymentaccount.created_on = VisitDate;
                paymentaccount.center_id = 77;
                paymentaccount.cycle_id = CycleID;
                Message = new DataUploaderDAL().SetInvoices(paymentaccount).return_message;
            }
            else if (Invoice.invoice_id != Guid.Empty)
            {
                return Invoice.invoice_number;
            }
            else
            {
                return string.Empty;
            }

            return Message;
        }
        int SaveLabTestData(DataDisplayClass tDataDisplayClassx)
        {
            int code = 0;

            LabTests serverlabtests = new LabTests();
            serverlabtests.test_id = Guid.NewGuid();
            serverlabtests.cycle_id = tDataDisplayClassx.CycleID;
            serverlabtests.patient_id = tDataDisplayClassx.PatientID;
            serverlabtests.patient_facility_number = tDataDisplayClassx.FacilityNumber;
            serverlabtests.invoice_number = tDataDisplayClassx.InvoiceNumber;
            serverlabtests.names = tDataDisplayClassx.Names;
            serverlabtests.lab_test_id = tDataDisplayClassx.lab_test_id;
            serverlabtests.lab_test_desc = tDataDisplayClassx.lab_test_desc;
            serverlabtests.sub_lab_test_id = 0;
            serverlabtests.staff_id = 12;
            serverlabtests.requestdate = tDataDisplayClassx.VisitDate;
            serverlabtests.comment = string.Empty;
            serverlabtests.sample = "SWAB";
            serverlabtests.paid = (tDataDisplayClassx.patient_payment_account_id != Guid.Empty) ? 1 : 0;
            serverlabtests.date = DateTime.Parse("01/01/1900");
            serverlabtests.time = DateTime.Parse("01/01/1900");
            serverlabtests.created_date = DateTime.Parse("01/01/1900");
            serverlabtests.updated_date = DateTime.Parse("01/01/1900");
            serverlabtests.value = string.Empty;
            serverlabtests.done = 0;
            serverlabtests.done_by = 0;
            serverlabtests.samplecollectiontime = DateTime.Parse("01/01/1900");
            serverlabtests.samplecollectedby = 0;
            serverlabtests.sample_quantity = string.Empty;
            serverlabtests.samplecondition = string.Empty;
            serverlabtests.samplelocation = string.Empty;
            serverlabtests.samplecomment = string.Empty;
            serverlabtests.labcomment = string.Empty;
            serverlabtests.consultation_id = Guid.Empty;
            serverlabtests.service_type_id = 0;
            serverlabtests.external_test = string.IsNullOrWhiteSpace(tDataDisplayClassx.external_facility) ? 0 : 1;
            serverlabtests.external_facility = string.IsNullOrWhiteSpace(tDataDisplayClassx.external_facility) ? string.Empty : tDataDisplayClassx.external_facility;
            serverlabtests.entered_by = 0;
            serverlabtests.fromexternal_test = 0;
            serverlabtests.fromexternal_facility = string.Empty;
            serverlabtests.supplier_id = Guid.Empty;
            serverlabtests.quantity = 1;
            serverlabtests.lab_test_price = tDataDisplayClassx.ChargeAmount;
            serverlabtests.unit_price = tDataDisplayClassx.ChargeAmount;
            serverlabtests.savings = tDataDisplayClassx.Savings;
            serverlabtests.regular_retail_price = tDataDisplayClassx.RetailPrice;
            serverlabtests.vat_amount = 0;
            serverlabtests.lab_test_code = "SAR003";
            serverlabtests.facility_id = 2;
            serverlabtests.center_id = 77;
            serverlabtests.reffered_externally = 0;
            serverlabtests.cost_price = 0;
            serverlabtests.for_travel = (tDataDisplayClassx.reason.ToLower().Contains("travel")) ? 1 : 0;
            serverlabtests.testing_reason = tDataDisplayClassx.reason;


            code = new DataUploaderDAL().SetPatientTests(serverlabtests).return_code;

            if (code > 0)
            {
                tDataDisplayClass.test_id = serverlabtests.test_id;
                tDataDisplayClass.lab_test_id = serverlabtests.lab_test_id;
                tDataDisplayClass.lab_test_desc = serverlabtests.lab_test_desc;
                tDataDisplayClass.lab_test_code = serverlabtests.lab_test_code;
                tDataDisplayClass.RetailPrice = serverlabtests.regular_retail_price;
                tDataDisplayClass.Savings = serverlabtests.savings;

                SaveDepositTransaction(tDataDisplayClass);
            }

            return code;

        }
        private string PopulateBody(string fullnames, string IdNumber, string dob, string collectionLocation, string collectionDate,string phone,string typeoftest,string paymentmethod, decimal amount, string collectiontime,string currency)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(@"C:\EmailFiles\BookingConfirmationEmail.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{fullnames}", fullnames);
            body = body.Replace("{IdNumber}", IdNumber);
            body = body.Replace("{dob}", dob);
            body = body.Replace("{collectionLocation}", collectionLocation);
            body = body.Replace("{collectiondate}", collectionDate);
            body = body.Replace("{phonenumber}", phone);
            body = body.Replace("{typeoftest}", typeoftest);
            body = body.Replace("{amount}", amount.ToString());
            body = body.Replace("{paymentmethod}", paymentmethod.Contains("0")? paymentmethod.Remove(0, 3): paymentmethod);
            body = body.Replace("{collectiontime}", collectiontime);
            body = body.Replace("{currency}", currency);
            //body = body.Replace("{paymentmethod}", paymentmethod);
            return body;
        }

        private string PopulateCCBody(string fullnames, string IdNumber, string dob, string collectionLocation, string collectionDate, string phone, string typeoftest, string paymentmethod, decimal amount, string collectiontime,string currency)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(@"C:\EmailFiles\CCHomeSampleCollection.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{fullnames}", fullnames);
            body = body.Replace("{IdNumber}", IdNumber);
            body = body.Replace("{dob}", dob);
            body = body.Replace("{collectionLocation}", collectionLocation);
            body = body.Replace("{collectiondate}", collectionDate);
            body = body.Replace("{phonenumber}", phone);
            body = body.Replace("{typeoftest}", typeoftest);
            body = body.Replace("{amount}", amount.ToString());
            body = body.Replace("{paymentmethod}", paymentmethod.Contains("0") ? paymentmethod.Remove(0, 3) : paymentmethod);
            body = body.Replace("{collectiontime}", collectiontime);
            body = body.Replace("{currency}", currency);
            //body = body.Replace("{paymentmethod}", paymentmethod);
            return body;
        }
        int SaveDepositTransaction(DataDisplayClass tDataDisplayClassx)
        {
            int code = 0;
                        DepositTransactionType tDepositTransaction = new DepositTransactionType();
            tDepositTransaction.deposit_transaction_id = Guid.NewGuid();
            tDepositTransaction.deposit_id = tDataDisplayClassx.DepositID;
            tDepositTransaction.deposit_transaction_amount = tDataDisplayClassx.ChargeAmount;
            tDepositTransaction.deposit_transaction_category_id = Guid.Parse("9771FEF0-CD96-4D7B-9621-D12A24F1B48C");
            tDepositTransaction.initial_transaction_amount = tDataDisplayClassx.ChargeAmount;
            tDepositTransaction.created_amount = tDataDisplayClassx.ChargeAmount;
            tDepositTransaction.payment_amount1 = 0;
            tDepositTransaction.payment_amount2 = 0;
            tDepositTransaction.payment_method_id = Guid.Empty;
            tDepositTransaction.payment_method2_id = Guid.Empty;
            tDepositTransaction.cancelled = 0;
            tDepositTransaction.cancelled_by = 0;
            tDepositTransaction.comment = string.Empty;
            tDepositTransaction.cycle_id = tDataDisplayClassx.CycleID;
            tDepositTransaction.date_cancelled = DateTime.Parse("01/01/1900");
            tDepositTransaction.date_created = tDataDisplayClassx.VisitDate;
            tDepositTransaction.staff_id = 12;
            tDepositTransaction.approved = 1;
            tDepositTransaction.approved_by = 12;
            tDepositTransaction.date_approved = DateTime.Parse("01/01/1900");
            tDepositTransaction.conversion_factor = 0;
            tDepositTransaction.payment_made_by_id = (tDataDisplayClassx.patient_payment_account_id != Guid.Empty) ? tDataDisplayClassx.patient_payment_account_id.ToString() : tDataDisplayClassx.PatientID.ToString();
            tDepositTransaction.is_cash_customer = false;
            tDepositTransaction.copay_type = "";
            tDepositTransaction.names = tDataDisplayClassx.Names;
            tDepositTransaction.invoice_number = tDataDisplayClass.InvoiceNumber;
            tDepositTransaction.patient_facility_number = tDataDisplayClassx.FacilityNumber;

            code = new DataUploaderDAL().SetDepositTransaction(tDepositTransaction).return_code;

            if (code > 0)
            {
                tDataDisplayClass.DepositTransID = tDepositTransaction.deposit_transaction_id;

                code = SaveVisitChargeItems(tDataDisplayClass);
            }

            return code;
        }
        int SaveVisitChargeItems(DataDisplayClass tDataDisplayClassx)
        {
            int code = 0;

            ObservableCollection<VisitChargeItemType> FinalList = new ObservableCollection<VisitChargeItemType>();

            VisitChargeItemType VisitChargeItem = new VisitChargeItemType();
            VisitChargeItem.charge_item_id = Guid.NewGuid();
            VisitChargeItem.cancelled = 0;
            VisitChargeItem.cancelled_by = 0;
            VisitChargeItem.cancelled_on = DateTime.Parse("01/01/1900");
            VisitChargeItem.charge_amount = tDataDisplayClassx.ChargeAmount;
            VisitChargeItem.unit_price = tDataDisplayClassx.ChargeAmount;
            VisitChargeItem.unit_cost = tDataDisplayClassx.ChargeAmount;
            VisitChargeItem.charge_id = Guid.NewGuid();
            VisitChargeItem.checked_allowed = 0;
            VisitChargeItem.cycle_id = tDataDisplayClassx.CycleID;
            VisitChargeItem.full_item_description = tDataDisplayClassx.lab_test_desc;
            VisitChargeItem.item_description = "Lab";
            VisitChargeItem.item_id = tDataDisplayClassx.lab_test_id.ToString();
            VisitChargeItem.items_trans_id = tDataDisplayClassx.test_id;
            VisitChargeItem.payment_date = DateTime.Parse("01/01/1900");
            VisitChargeItem.charge_date = tDataDisplayClassx.VisitDate;
            VisitChargeItem.quantity = 1;
            VisitChargeItem.staff_id = 12;
            VisitChargeItem.paid_amount = 0;
            VisitChargeItem.initial_charge_amount = tDataDisplayClassx.ChargeAmount;
            VisitChargeItem.deposit_id = tDataDisplayClassx.DepositID;
            VisitChargeItem.deposit_transaction_id = tDataDisplayClassx.DepositTransID;
            VisitChargeItem.payment_deposit_transaction_id = Guid.Empty;
            VisitChargeItem.item_name = VisitChargeItem.item_description;
            VisitChargeItem.patient_facility_number = tDataDisplayClassx.FacilityNumber;
            VisitChargeItem.patient_names = tDataDisplayClassx.Names;
            VisitChargeItem.patient_id = tDataDisplayClassx.PatientID;
            VisitChargeItem.is_splited_transaction = false;
            VisitChargeItem.inpatient = 0;
            VisitChargeItem.comment = string.Empty;
            VisitChargeItem.cancellation_comment = string.Empty;
            VisitChargeItem.supplier_id = Guid.Empty;
            VisitChargeItem.invoice_number = tDataDisplayClassx.InvoiceNumber;
            VisitChargeItem.payment_method_id = Guid.Empty;
            VisitChargeItem.is_copay = false;
            VisitChargeItem.isinpatient = 0;
            VisitChargeItem.isinsurance = (tDataDisplayClassx.patient_payment_account_id != Guid.Empty) ? 1 : 0;
            VisitChargeItem.regular_retail_price = tDataDisplayClassx.RetailPrice;
            VisitChargeItem.savings = tDataDisplayClassx.Savings;
            VisitChargeItem.vat_amount = 0;
            VisitChargeItem.item_code = tDataDisplayClassx.lab_test_code;
            VisitChargeItem.center_id = 77;
            VisitChargeItem.facility_id = 2;
            VisitChargeItem.discount = 0;
            VisitChargeItem.supplier_charge_amount = 0;
            VisitChargeItem.updated_on = DateTime.Parse("01/01/1900");
            VisitChargeItem.updated_by = 12;

            FinalList.Add(VisitChargeItem);

            code = new DataUploaderDAL().SetVisitChargeItemsBulk(FinalList.ToArray(), 1).return_code;

            if (code > 0)
            {
                //SaveVisiCharges(serverlabtests, VisitChargeItem.charge_id);
            }

            return code;
        }



    }
}
