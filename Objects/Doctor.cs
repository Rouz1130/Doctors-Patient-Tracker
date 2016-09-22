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
