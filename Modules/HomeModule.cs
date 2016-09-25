// using  System.Collections.Generic;
// using Nancy;
// using Nancy.ViewEngines.Razor;
//
// namespace Appointment
// {
//   public class Homemodule : NancyModule
//   {
//     public Homemodule()
//     {
//       Get["/"] = _ => {
//         List<Doctor> allDoctors = Doctor.GetAll();
//         return View["index.cshtml"];
//       };
//       Get["/patients"] = _ => {
//         List<Patient> AllPatients = Patient.GetAll();
//         return View["patients.cshtml", AllPatients];
//       };
//     }
//
//   }
// }
