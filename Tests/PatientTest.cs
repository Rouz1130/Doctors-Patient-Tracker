using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Appointment
{
  public class PatientTest : IDisposable
  {
    public PatientTest()
    {
    DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=doctor_appointment_test;Integrated Security=SSPI;";
   }

   [Fact]
   public void Test1_DatabaseEmptyAtFirst()
   {
     int result = Patient.GetAll().Count;

     Assert.Equal(0, result);
   }


   public void Dispose()
   {
     Patient.DeleteAll();
   }

    }
  }
