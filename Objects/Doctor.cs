using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Appointment
{
  public class Doctor
  {
    //properties
    private int _id;
    private string _name;

    //constructor
    public Doctor(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    // Getters and Setters
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

    public override bool Equals(System.Object otherDoctor)
    {
      if (!(otherDoctor is Doctor))
      {
        return false;
      }
      else
      {
        Doctor newDoctor = (Doctor) otherDoctor;
        bool nameEquality = (this.GetName() == newDoctor.GetName());
        return (nameEquality);
      }
    }

    public static List<Doctor> GetAll()
    {
      List<Doctor> allDoctors = new List<Doctor>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM doctors;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int doctorId = rdr.GetInt32(0);
        string doctorName = rdr.GetString(1);
        Doctor newDoctor = new Doctor(doctorName, doctorId);
        allDoctors.Add(newDoctor);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allDoctors;
    }

    public List<Patient> GetPatients()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patients WHERE doctor_id = @DoctorId;", conn);
      SqlParameter doctorIdParameter = new SqlParameter();
      doctorIdParameter.ParameterName = "@DoctorId";
      doctorIdParameter.Value = this.GetId();
      cmd.Parameters.Add(doctorIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Patient> patients = new List<Patient>{};
      while(rdr.Read())
      {
        int patientId = rdr.GetInt32(0);
        string patientName = rdr.GetString(1);
        int patientDoctorId = rdr.GetInt32(2);
        Patient newPatient = new Patient(patientName, patientDoctorId, patientId);
      }
      if (rdr !=null)
      {
        rdr.Close();
      }
      if (conn !=null)
      {
        conn.Close();
      }
      return patients;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO doctors(name) OUTPUT INSERTED.id VALUES (@DoctorName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@DoctorName";
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
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Doctor Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM doctors WHERE id = @DoctorId;", conn);
      SqlParameter doctorIdParameter = new SqlParameter();
      doctorIdParameter.ParameterName = "@DoctorId";
      doctorIdParameter.Value = id.ToString();
      cmd.Parameters.Add(doctorIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundDoctorId = 0;
      string foundDoctorName = null;
      while(rdr.Read())
      {
        foundDoctorId = rdr.GetInt32(0);
        foundDoctorName = rdr.GetString(1);
      }
      Doctor foundDoctorList = new Doctor(foundDoctorName, foundDoctorId);
      if (rdr !=null)
      {
        rdr.Close();
      }
      if (conn !=null)
      {
        conn.Close();
      }
      return foundDoctorList;
      }

      public void Update(string newName)
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("UPDATE doctors SET name = @NewName OUTPUT INSERTED.name WHERE id = @DoctorId;", conn);

        SqlParameter newNameParameter = new SqlParameter();
        newNameParameter.ParameterName = "@NewName";
        newNameParameter.Value = newName;
        cmd.Parameters.Add(newNameParameter);

        SqlParameter doctorIdParameter = new SqlParameter();
        doctorIdParameter.ParameterName = "@DoctorId";
        doctorIdParameter.Value = this.GetId();
        cmd.Parameters.Add(doctorIdParameter);

        SqlDataReader rdr = cmd.ExecuteReader();

        while(rdr.Read())
        {
          this._name = rdr.GetString(0);
        }
        if(rdr !=null)
        {
          rdr.Close();
        }
        if(conn !=null)
        {
          conn.Close();
        }
      }


      public void Delete()
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("DELETE FROM doctors WHERE doctor_id = @DoctorId;",conn);

        SqlParameter doctorIdParameter = new SqlParameter();
        doctorIdParameter.ParameterName = "@DoctorId";
        doctorIdParameter.Value = this.GetId();

        cmd.Parameters.Add(doctorIdParameter);
        cmd.ExecuteNonQuery();

        if (conn != null)
        {
          conn.Close();
        }
      }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM doctors;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }


    }
  }
