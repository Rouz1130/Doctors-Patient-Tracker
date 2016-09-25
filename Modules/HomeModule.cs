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











    }
