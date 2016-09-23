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

   [Fact]
   public void Test2_Equals_ReturnsTrueIfNameAreSame()
   {
     Patient firstPatient = new Patient("Doug");
     Patient secondPatient = new Patient("Doug");

     Assert.Equal(firstPatient, secondPatient);
   }

   [Fact]
   public void Test3_Save_AssignedIdTo_Object()
   {
     Patient testPatient = new Patient("Alex");
     testPatient.Save();
     Patient savedPatient = Patient.GetAll()[0];

     int result = savedPatient.GetId();
     int testId = testPatient.GetId();

     Assert.Equal(testId, result);
   }


   public void Dispose()
   {
     Patient.DeleteAll();
   }

    }
  }
