using  System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Appointment
{
  public class Homemodule : NancyModule
  {
    public Homemodule()
    {
      Get["/"] = _ => {
        List<Doctor> AllDoctors = Doctor.GetAll();
        return View["index.cshtml", AllDoctors];
      };

      Get["/patients"] = _ => {
        List<Patient> AllPatients = Patient.GetAll();
        return View["patients.cshtml", AllPatients];
      };

      Get["/doctors"] = _ => {
        List<Doctor> AllDoctors = Doctor.GetAll();
        return View["doctors.cshtml", AllDoctors];
      };

      Get["/doctors/new"] = _ => {
     return View["doctors_form.cshtml"];
   };

   Post["/doctors/new"] = _ => {
        Doctor newDoctor = new Doctor(Request.Form["doctor-name"]);
        newDoctor.Save();
        return View["confirm.cshtml"];
      };

      Get["/patients/new"] = _ => {
       List<Doctor> AllDoctors = Doctor.GetAll();
       return View["patients_form.cshtml", AllDoctors];
     };
     Post["/patients/new"] = _ => {
      Patient newPatient = newPatient(Request.Form["patient-name"], Request.Form["doctor-id"]);
       newPatient.Save();
       return View["confirm.cshtml"];
     };








    }

  }
}
