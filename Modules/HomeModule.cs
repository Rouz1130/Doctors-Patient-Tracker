using System.Collections.Generic;
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
        return View["success.cshtml"];
      };

      Get["/patients/new"] = _ => {
        List<Doctor> AllDoctors = Doctor.GetAll();
        return View["patients_form.cshtml", AllDoctors];
      };

      Post["/patients/new"] = _ => {
        Patient newPatient = new Patient(Request.Form["patient-name"],Request.Form["doctor-id"]);
        newPatient.Save();
        return View["success.cshtml"];
      };

      Post["/patients/delete"] = _ => {
        Patient.DeleteAll();
        return View["cleared.cshtml"];
      };

      Get["/doctors/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var SelectedDoctor = Doctor.Find(parameters.id);
        var DoctorPatients = SelectedDoctor.GetPatients();
        model.Add("doctor", SelectedDoctor);
        model.Add("patients", DoctorPatients);
        return View["doctor.cshtml", model];
      };

      Get["doctor/edit/{id}"] = parameters => {
        Doctor SelectedDoctor = Doctor.Find(parameters.id);
        return View["doctor_edit.cshtml", SelectedDoctor];
      };

      Patch["doctor/edit/{id}"] = parameters => {
        Doctor SelectedDoctor = Doctor.Find(parameters.id);
        SelectedDoctor.Update(Request.Form["doctor-name"]);
        return View["success.cshtml"];
      };

      Get["doctor/delete/{id}"] = parameters => {
        Doctor SelectedDoctor = Doctor.Find(parameters.id);
        return View["doctor_delete.cshtml", SelectedDoctor];
      };

      Delete["doctor/delete/{id}"] = parameters => {
        Doctor SelectedDoctor = Doctor.Find(parameters.id);
        SelectedDoctor.Delete();
        return View["success.cshtml"];
      };


    }
   }
  }
