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

   public void Dispose()
   {
     Patient.DeleteAll();
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
     Patient firstPatient = new Patient("Doug", 1);
     Patient secondPatient = new Patient("Doug", 1);

     Assert.Equal(firstPatient, secondPatient);
   }

   [Fact]
   public void Test3_Save_AssignedIdTo_Object()
   {
     Patient testPatient = new Patient("Alex",1);
     testPatient.Save();
     Patient savedPatient = Patient.GetAll()[0];

     int result = savedPatient.GetId();
     int testId = testPatient.GetId();

     Assert.Equal(testId, result);
   }


   [Fact]
   public void Test4_Find_PatientInDatabase()
   {
     Patient testPatient = new Patient ("Buddy",1);
     testPatient.Save();

     Patient foundPatient = Patient.Find(testPatient.GetId());

     Assert.Equal(testPatient, foundPatient);
   }


    }
  }
