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
    private int _doctorId;

    //constructor
    public Patient(string Name, int DoctorId, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _doctorId = DoctorId;

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

    public int GetDoctorId()
    {
      return _doctorId;
    }

    public void SetDoctorId(int newDoctorId)
    {
      _doctorId = newDoctorId;
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
        bool doctorEquality = this.GetDoctorId() == newPatient.GetDoctorId();
        return (idEquality && nameEquality && doctorEquality);
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
        int patientDoctorId = rdr.GetInt32(2);
        Patient newPatient = new Patient(patientName,patientDoctorId, patientId);
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

      SqlCommand cmd = new SqlCommand("INSERT INTO patients(name, doctor_id) OUTPUT INSERTED.id VALUES (@PatientName,@PatientDoctorId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@PatientName";
      nameParameter.Value = this.GetName();

      SqlParameter doctorIdParameter = new SqlParameter();
      doctorIdParameter.ParameterName = "@PatientDoctorId";
      doctorIdParameter.Value = this.GetDoctorId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(doctorIdParameter);

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
      SqlParameter doctorIdParameter = new SqlParameter();
      doctorIdParameter.ParameterName = "@PatientId";
      doctorIdParameter.Value = id.ToString();
      cmd.Parameters.Add(doctorIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundPatientId = 0;
      string foundPatientName = null;
      int foundPatientDoctorId = 0;

      while(rdr.Read())
      {
        foundPatientId = rdr.GetInt32(0);
        foundPatientName = rdr.GetString(1);
        foundPatientDoctorId = rdr.GetInt32(2);
      }
      Patient foundPatient = new Patient(foundPatientName,foundPatientDoctorId, foundPatientId);

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

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
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
