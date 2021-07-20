using Newtonsoft.Json;
using services.varian.com.AriaWebConnect.Link;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VMSType = services.varian.com.AriaWebConnect.Common;

namespace AriaAccessWebServiceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            // login at myvarian.com => My Account  => API Key Management => New API Request => Fill out all the information. Once submitted, "Display API Request => Click "API Key" under Request Status. From the downloaded key fiel, copy key value to below. Also, set API Key at varian Service Portal. 
            // Get your own gateway.cs file following the pp 6-7 in "Aria Access 1.4 reference Guide" and add to this project.
            // This UserName / Password may or may not work based on how Administrave gropu set your access right at Varian Service Portal, but usually physicists are authorized to do most of it.
            // If windows ID/pswd is not sync'd with Aria, Username and Password need to be added to each request as follows.

            /*
            // Do below.
            string userName = "your userName";
            string psWd = "your password";

            // Add the following order in each request
            ,\"UserName\":{\"Value\":\"" + userName + "\"}, ,\"Password\":{\"Value\":\"" + psWd + "\"}

            */



            string apiKey = "Get your own Aria Access Key"; //place your API key here (ARIA Access)

            // Get Doctors ID List

            string departmentName = "Radiation Oncology or Actual name";
            string hospitalName = "Your Hospital Name";
            
            string request_Doctors = "{\"__type\":\"GetDoctorsInfoRequest:http://services.varian.com/AriaWebConnect/Link\",\"Attributes\":[],\"HospitalID\":{\"Value\":\"" + hospitalName + "\"},\"DepartmentID\":{\"Value\":\"" + departmentName + "\"}}";
            string response_Doctors = SendData(request_Doctors, true, apiKey);

            //Console.WriteLine(response_Doctors);
            var response_Docs = JsonConvert.DeserializeObject<GetDoctorsInfoResponse>(response_Doctors);

            var DoctInfoList = new List<string> ();
            foreach (var Doctors in response_Docs.DoctorsInfo)
            {
                Console.WriteLine(Doctors.DoctorId.Value + "-" + Doctors.LastName.Value + ", " + Doctors.FirstName.Value + /*" " + Doctors.MiddleName.Value + */" " + Doctors.NameSuffix.Value/* + " E-mail: " + Doctors.EMailAddress.Value + " " + Doctors.Specialty.Value*/);
            }
            //Console.Write(response_Docs);
            Console.ReadLine();

            //// Get Machine Appointment List
            //string departmentName = "Radiation Oncology or Actual name";
            //string hospitalName = "Your Hospital Name";
            //string resourceType = "Machine";
            //string machineId = "Machine ID";
            ////var week_start = new DateTimeOffset(startdate, TimeZoneInfo.Local.GetUtcOffset(startdate));
            //var enddate = DateTime.Now.AddDays(1);// startdate.AddDays(5);// 
            //var startdate = enddate.AddDays(-8);//  new DateTime(2021, 7, 5);
            //GetMachineAppointmentsRequest getMachineAppointmentsRequest = new GetMachineAppointmentsRequest
            //{
            //    DepartmentName = new VMSType.String { Value = departmentName },
            //    HospitalName = new VMSType.String { Value = hospitalName },
            //    ResourceType = new VMSType.String { Value = resourceType },
            //    MachineId = new VMSType.String { Value = machineId },
            //    StartDateTime = new VMSType.String { Value = startdate.ToString("yyyy-MM-ddTHH:mm:sszzz") },
            //    EndDateTime = new VMSType.String { Value = enddate.ToString("yyyy-MM-ddTHH:mm:sszzz") }
            //};
            //string request_appointments = $"{{\"__type\":\"GetMachineAppointmentsRequest:http://services.varian.com/AriaWebConnect/Link\",{JsonConvert.SerializeObject(getMachineAppointmentsRequest).TrimStart('{')}}}";
            //string response_appointments = SendData(request_appointments, true, apiKey);
            //GetMachineAppointmentsResponse getMachineAppointmentsResponse = JsonConvert.DeserializeObject<GetMachineAppointmentsResponse>(response_appointments);
            ////Dictionary<string, int> appointments = new Dictionary<string, int>();
            //Console.WriteLine($"Appointments for the week of {startdate.ToString("MM-dd-yyyy")} on Machine {machineId}");
            //foreach (var appointment in getMachineAppointmentsResponse.MachineAppointments)//.Where(ma => ma.PatientId.Value.Length > 4))
            //{
            //    Console.WriteLine(appointment.MachineId.Value + ": " + appointment.PatientId.Value + " - Starts on: " + appointment.ScheduledStartTime.Value.ToString().Substring(0, 10) + " at " + appointment.ScheduledStartTime.Value.ToString().Substring(11, 5) + " & Ends on: " + appointment.ScheduledEndTime.Value.ToString().Substring(0, 10) + " at " + appointment.ScheduledEndTime.Value.ToString().Substring(11, 5));
            //    //if (appointments.Keys.Contains(appointment.PatientId.Value))
            //    //{
            //    //    appointments[appointment.PatientId.Value]++;
            //    //}
            //    //else
            //    //{
            //    //    appointments.Add(appointment.PatientId.Value, 1);
            //    //}
            //}

            ////Console.Write(getMachineAppointmentsResponse.MachineAppointments.ToString());
            ////foreach (var app in appointments)
            ////{
            ////    Console.WriteLine($"{app.Key}: {app.Value}");
            ////}
            ////Console.WriteLine(response_appointments);
            //Console.ReadLine();

            //// Get patient Diagnoses List

            //string patientId = "PatientId";
            //string request_Diagnosis = "{\"__type\":\"GetPatientDiagnosesRequest:http://services.varian.com/AriaWebConnect/Link\",\"Attributes\":[],\"PatientDiagnosisId\":[],\"PatientId\":{\"Value\":\"" + patientId + "\"}}";
            //string response_Diagnosis = SendData(request_Diagnosis, true, apiKey);

            //var response_DiagnosisDoc = JsonConvert.DeserializeObject<GetPatientDiagnosesResponse>(response_Diagnosis);
            //foreach (var Diag in response_DiagnosisDoc.PatientDiagnoses)
            //{
            //    Console.WriteLine(Diag.AreaName.Value + ": " + Diag.DiagnosisCode.Value + " - " + Diag.DiagnosisCodeDescription.Value);
            //}
            ////Console.Write(response_Diagnosis);
            //Console.ReadLine();


            //// Get Patient Info

            //string patientId = "PatientId";
            //string request_ptInfo = "{\"__type\":\"GetPatientsRequest:http://services.varian.com/AriaWebConnect/Link\",\"Attributes\":[],\"PatientId1\":{\"Value\":\"" + patientId + "\"}}";
            //string response_ptInfo = SendData(request_ptInfo, true, apiKey);

            //var ptInfo = JsonConvert.DeserializeObject<GetPatientsResponse>(response_ptInfo);

            //foreach (var ptinfo in ptInfo.Patients)
            //{
            //    Console.WriteLine(ptinfo.LastName.Value + ", " + ptinfo.FirstName.Value + " " + ptinfo.MiddleName.Value + "\n");
            //}
            //Console.ReadLine();

            // Get Assigned Doctor Id

            //string patientId = "PatientId";
            //string request_DrInfo = "{\"__type\":\"GetDoctorsAssignedToPatientRequest:http://services.varian.com/AriaWebConnect/Link\",\"Attributes\":[],\"IsOncologist\":{\"Value\":true},\"PatientId\":{\"Value\":\"" + patientId + "\"}}";
            //string response_DrInfo = SendData(request_DrInfo, true, apiKey);

            //var DrInfo = JsonConvert.DeserializeObject<GetDoctorsAssignedToPatientResponse>(response_DrInfo);

            //foreach (var Drinfo in DrInfo.AssignedDoctors)
            //{
            //    string Dname = "";
            //    for (int n = 0; n < DoctInfoList.Count(); n++)
            //    {
            //        string[] Dinfo = DoctInfoList[n].Split('-');
            //        if(Dinfo[0].Equals(Drinfo.DoctorId.Value))
            //        {
            //            Dname = Dinfo[1];
            //        }
            //    }
            //        Console.WriteLine(Drinfo.DoctorId.Value + " - " + Dname + "\n");
            //}
            ////Console.WriteLine(response_DrInfo);
            //Console.ReadLine();

            //// Get Machine List

            //string request_MachineList = "{\"__type\":\"GetMachineListRequest:http://services.varian.com/AriaWebConnect/Link\",\"Attributes\":[]}";
            //string response_MachineList = SendData(request_MachineList, true, apiKey);

            ////Console.WriteLine(response_Doctors);
            //var response_MachineListDocs = JsonConvert.DeserializeObject<GetMachineListResponse>(response_MachineList);

            //foreach (var machine in response_MachineListDocs.Machines)
            //{
            //    Console.WriteLine(machine.MachineId.Value + ": Z" + machine.MachineType.Value);
            //}

            //Console.ReadLine();

            //// Get Billing Info

            //string endDate = DateTime.Now.ToString("yyyy-MM-dd");
            //string startDate = DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd");

            //string request_BillingInfo = "{\"__type\":\"GetBillingInfoRequest:http://services.varian.com/AriaWebConnect/Link\",\"Attributes\":[],\"EndDate\":{\"Value\":\"2021-07-16\"},\"HospitalName\":{ \"Value\":\"Arnett Hospital\"},\"ReturnAllCharges\":{ \"Value\":true},\"SortMode\":{ \"Value\":2},\"StartDate\":{ \"Value\":\"2021-07-15\"}}";

            //string response_BillingInfo = SendData(request_BillingInfo, true, apiKey);

            //var response_BillingInfoDocs = JsonConvert.DeserializeObject<GetBillingInfoResponse>(response_BillingInfo);

            //foreach (var BInfo in response_BillingInfoDocs.BillingInfos)
            //{
            //    Console.WriteLine(BInfo.CPTCode.Value);
            //}
            ////Console.Write(response_BillingInfo);
            //Console.ReadLine();

            //// Get Patient Courses and PlanSetup Request

            //string patientId = "PatientId";
            //string request_C_PS = "{\"__type\":\"GetPatientCoursesAndPlanSetupsRequest:http://services.varian.com/AriaWebConnect/Link\",\"Attributes\":null,\"PatientId\":{ \"Value\":\"" + patientId + "\"},\"TreatmentType\":{ \"Value\":\"All\"}}";

            ////string request_C_PS = "{\"__type\":\"GetPatientCoursesAndPlanSetupsRequest:http://services.varian.com/AriaWebConnect/Link\",\"Attributes\":null,\"PatientId\":{ \"Value\":\"72915592\"},\"TreatmentType\":{ \"Value\":\"Linac\"}}";

            //string response_C_PSInfo = SendData(request_C_PS, true, apiKey);

            //var C_PSInfo = JsonConvert.DeserializeObject<GetPatientCoursesAndPlanSetupsResponse>(response_C_PSInfo);

            //foreach (var C_PSinfo in C_PSInfo.PatientCourses)
            //{
            //    Console.WriteLine(C_PSinfo.CourseId.Value + " - " + C_PSinfo.StartDateTime.Value);
            //    //foreach(var pS_info in C_PSinfo.PatientPlanSetups)
            //    //{
            //    //    Console.WriteLine(pS_info + " - " + pS_info.TreatmentType.Value);
            //    //}
            //}
            ////Console.WriteLine(response_C_PSInfo);
            //Console.ReadLine();

            //// Get Patient Plans Request

            //string patientId = "PatientId";
            //string courseId = "CourseId";
            //string request_Pl = "{\"__type\":\"GetPatientPlansRequest:http://services.varian.com/AriaWebConnect/Link\",\"Attributes\":null,\"PatientId\":{ \"Value\":\"" + patientId + "\"},\"CourseId\":{ \"Value\":\"" + courseId + "\"}}";

            //string response_PlInfo = SendData(request_Pl, true, apiKey);

            //var PlInfo = JsonConvert.DeserializeObject<GetPatientPlansResponse>(response_PlInfo);

            //foreach (var Plinfo in PlInfo.PatientPlans)
            //{
            //    Console.WriteLine(Plinfo.CourseId.Value + " - " + Plinfo.PlanSetupId.Value + ": " + Plinfo.NoOfFractions.Value + " fractions " + Plinfo.CreatedByUserName.Value + ":" + Plinfo.HistoryTaskName.Value + " done by " + Plinfo.HistoryUserName.Value + " \n");
            //}
            ////Console.WriteLine(response_PlInfo);
            //Console.ReadLine();

            //// Get Patient Plan Tx Fields Request - Something worng

            //string patientId = "PatientId";
            //string courseId = "CourseId";
            //string planId = "PlanId";
            //string request_PlF = "{\"__type\":\"GetPatientPlanTxFieldsRequest:http://services.varian.com/AriaWebConnect/Link\",\"Attributes\":null,\"PatientId\":{ \"Value\":\"" + patientId + "\"},\"CourseId\":{ \"Value\":\"" + courseId + "\"},\"PlanId\":{ \"Value\":\"" + planId + "\"}";

            //string response_PlFInfo = SendData(request_PlF, true, apiKey);

            ////var PlFInfo = JsonConvert.DeserializeObject<GetPatientPlanTxFieldsResponse>(response_PlFInfo);

            ////foreach (var PlFinfo in PlFInfo.FieldInfos)
            ////{
            ////    Console.WriteLine(PlFinfo.FieldId);
            ////}
            //Console.WriteLine(response_PlFInfo);
            //Console.ReadLine();

            //// Get Patient Fields Treated Info Request

            //string patientId = "PatientId";
            //string courseId = "CourseId";
            //string TxEndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            //string TxStartDate = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
            //string request_PTrInfo = "{\"__type\":\"GetPatientFieldsTreatedInfoRequest:http://services.varian.com/AriaWebConnect/Link\",\"Attributes\":null,\"PatientId\":{ \"Value\":\"" + patientId + "\"},\"CourseId\":{ \"Value\":\"" + courseId + "\"},\"TreatmentEndDate\":{ \"Value\":\"" + TxEndDate + "\"},\"TreatmentStartDate\":{ \"Value\":\"" + TxStartDate + "\"}}";

            //string response_PTrInfo = SendData(request_PTrInfo, true, apiKey);

            //var PTrInfo = JsonConvert.DeserializeObject<GetPatientFieldsTreatedInfoResponse>(response_PTrInfo);


            //Console.WriteLine(PTrInfo.TotalDoseDeliveredToDate.Value);

            //foreach (var PTrinfo in PTrInfo.TreatedFieldsInfo)
            //{
            //    Console.WriteLine(PTrinfo.FieldId.Value + ": " + PTrinfo.TreatmentDate.Value + " - " + PTrinfo.ActualDailyDose.Value);
            //}
            ////Console.WriteLine(PTrInfo.TotalDoseDeliveredToDate.Value);
            //Console.ReadLine();

            //// Get Patient Reference Points Request

            //string patientId = "PatientId";
            ////string courseId = "CourseId";
            ////string TxEndDate = DateTime.Now.ToString("yyyy-MM-dd");
            ////string TxStartDate = DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd");
            //string request_PtRefPt = "{\"__type\":\"GetPatientRefPointsRequest:http://services.varian.com/AriaWebConnect/Link\",\"Attributes\":null,\"PatientId\":{ \"Value\":\"" + patientId + "\"}}";

            //string response_PtRefPt = SendData(request_PtRefPt, true, apiKey);

            //var PtRefPt = JsonConvert.DeserializeObject<GetPatientRefPointsResponse>(response_PtRefPt);

            //foreach (var PtRefpt in PtRefPt.ReferencePoints)
            //{
            //    Console.WriteLine(PtRefpt.PatientVolumeId.Value + ": " + PtRefpt.ReferencePointId.Value + " - " + PtRefpt.ReferencePointType.Value + ", " + PtRefpt.TotalDoseLimit.Value);
            //}
            ////Console.WriteLine(PTrInfo.TotalDoseDeliveredToDate.Value);
            //Console.ReadLine();

            //// Get Patient Clinical Concepts Request

            //string patientId = "PatientId";
            //string courseId = "CourseId";
            ////string TxEndDate = DateTime.Now.ToString("yyyy-MM-dd");
            ////string TxStartDate = DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd");
            //string request_PtClCon = "{\"__type\":\"GetPatientClinicalConceptsRequest:http://services.varian.com/AriaWebConnect/Link\",\"Attributes\":null,\"PatientId\":{ \"Value\":\"" + patientId + "\"},\"CourseId\":{ \"Value\":\"" + courseId + "\"}}";

            //string response_PtClCon = SendData(request_PtClCon, true, apiKey);

            //var PtClCon = JsonConvert.DeserializeObject<GetPatientClinicalConceptsResponse>(response_PtClCon);

            //foreach (var PtClcon in PtClCon.PatientClinicalConcepts)
            //{
            //    Console.WriteLine(PtClcon.PrescriptionName.Value + ": " + PtClcon.PhaseType.Value + ", " + PtClcon.Status.Value + ", " + PtClcon.Technique.Value + ", " + PtClcon.Site.Value + ", " + PtClcon.CreationUserName.Value + ", " + PtClcon.TretmentIntentType.Value);
            //}
            ////Console.WriteLine(PTrInfo.TotalDoseDeliveredToDate.Value);
            //Console.ReadLine();
        }

        public static string SendData(string request, bool bIsJson, string apiKey)
        {
            var sMediaTYpe = bIsJson ? "application/json" :
           "application/xml";
            var sResponse = System.String.Empty;
            using (var c = new HttpClient(new
           HttpClientHandler()
            { UseDefaultCredentials = true }))
            {
                if (c.DefaultRequestHeaders.Contains("ApiKey"))
                {
                    c.DefaultRequestHeaders.Remove("ApiKey");
                }
                c.DefaultRequestHeaders.Add("ApiKey", apiKey);
                //in App.Config, change this to the Resource ID for your REST Service.
                var task =
               c.PostAsync(ConfigurationManager.AppSettings["GatewayRestUrl"],
                new StringContent(request, Encoding.UTF8,
               sMediaTYpe));
                Task.WaitAll(task);
                var responseTask =
               task.Result.Content.ReadAsStringAsync();
                Task.WaitAll(responseTask);
                sResponse = responseTask.Result;
            }
            return sResponse;
        }
    }
}
