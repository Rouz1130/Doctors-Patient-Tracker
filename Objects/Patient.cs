using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Appointment
{
  public class Patient
  {
    //properties
    private int _id;
    private string _name;

    //constructor
    public Patient(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;

    }

    //getters and setters
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

    //methods: Equals method
    public override bool Equals(System.Object otherPatient)
    {
      if (!(otherPatient is Patient))
      {
        return false;
      }
      else
      {
        Patient newPatient = (Patient) otherPatient;
        bool idEquality = (this.GetId() == newPatient.GetId());
        bool nameEquality = (this.GetName() == newPatient.GetName());
        return (idEquality && nameEquality);
      }
    }
    //methods
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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO patients(name) OUTPUT INSERTED.id VALUES (@PatientsName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@PatientsName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn !=null)
      {
        conn.Close();
      }
    }

    public static Patient Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patients WHERE id= @PatientId;",conn);
      SqlParameter patientIdParameter = new SqlParameter();
      patientIdParameter.ParameterName = "@PatientId";
      patientIdParameter.Value = id.ToString();
      cmd.Parameters.Add(patientIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundPatientId = 0;
      string foundPatientName = null;
      while(rdr.Read())
      {
        foundPatientId = rdr.GetInt32(0);
        foundPatientName = rdr.GetString(1);
      }
      Patient foundPatient = new Patient(foundPatientName, foundPatientId);

      if (rdr !=null)
      {
        rdr.Close();
      }
      if (conn !=null)
      {
        conn.Close();
      }
      return foundPatient;
    }

    public void Update(string newName)
   {
     SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("UPDATE patients SET name = @NewName OUTPUT INSERTED.name WHERE id = @PatientId;", conn);

     SqlParameter newNameParameter = new SqlParameter();
     newNameParameter.ParameterName = "@NewName";
     newNameParameter.Value = newName;
     cmd.Parameters.Add(newNameParameter);

     SqlParameter patientIdParameter = new SqlParameter();
     patientIdParameter.ParameterName = "@PatientId";
     patientIdParameter.Value = this.GetId();
     cmd.Parameters.Add(patientIdParameter);
     SqlDataReader rdr = cmd.ExecuteReader();

     while(rdr.Read())
     {
       this._name = rdr.GetString(0);
     }

     if (rdr != null)
     {
       rdr.Close();
     }

     if (conn != null)
     {
       conn.Close();
     }
   }

    //DeleteAll method has to have IDisposable in test to run test
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
