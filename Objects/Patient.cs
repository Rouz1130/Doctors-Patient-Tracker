using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Appointment
{
  public class Patient
  {
    private int _id;
    private string _name;

    public Patient(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;

    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public static List<Patient> GetAll()
    {
      List<Patient> allPatients = new List<Patient>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patients;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int patientId = rdr.GetInt32(0);
        string patientName = rdr.GetString(1);
        Patient newPatient = new Patient(patientName, patientId);
        allPatients.Add(newPatient);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allPatients;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM patients;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
