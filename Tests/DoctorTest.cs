using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Appointment
{
  public class DoctorTest : IDisposable
  {
    public DoctorTest()
    {
       DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=doctor_appointment_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test1_Database_EmptyAtFirst()
    {
      int result = Doctor.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test2_Equal_ReturnsTrueIfNamesAreSame()
    {
      Doctor firstDoctor = new Doctor("John");
      Doctor secondDoctor = new Doctor("John");

      Assert.Equal(firstDoctor, secondDoctor);
    }

    [Fact]
    public void Test3_SavestoDatabase()
    {
      Doctor testDoctor = new Doctor("Bob");
      testDoctor.Save();
      List<Doctor> result = Doctor.GetAll();
      List<Doctor> testList = new List<Doctor>{testDoctor};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test4_Save_AssignIdToObject()
    {
      Doctor testDoctor = new Doctor("Andrew");
      testDoctor.Save();
      Doctor savedDoctor = Doctor.GetAll()[0];

      int result = savedDoctor.GetId();
      int testId = testDoctor.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test5_Find_NewDoctor()
    {
      Doctor testDoctor = new Doctor("Maggy");
      testDoctor.Save();

      Doctor foundDoctor = Doctor.Find(testDoctor.GetId());

      Assert.Equal(testDoctor, foundDoctor);
    }

      [Fact]
      public void Test6_Update_DoctorToDatabase()
      {
        string name = "Maggy";
        Doctor testDoctor = new Doctor(name);
        testDoctor.Save();
        string newName = "Steve";

        testDoctor.Update(newName);
        string result = testDoctor.GetName();

        Assert.Equal(newName, result);
      }
      
      public void Dispose()
      {
        Doctor.DeleteAll();
      }

    }
  }
