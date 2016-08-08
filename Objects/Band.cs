using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker.Objects
{
  public class Band
  {
    private string _name;
    private int _id;

    public Band(string name, int id=0)
    {
      _name = name;
      _id = id;
    }

    public string GetName()
    {
      return _name;
    }


    public int GetId()
    {
      return _id;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }

    public override bool Equals(System.Object otherBand)
  {
    if (!(otherBand is Band))
    {
      return false;
    }
    else
    {
      Band newBand = (Band) otherBand;
      return this.GetName().Equals(newBand.GetName());
    }
  }

  public override int GetHashCode()
  {
    return this.GetName().GetHashCode();
  }

  public void Save()
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO bands(name)OUTPUT INSERTED.id VALUES (@bandName);", conn );
        SqlParameter nameParameter = new SqlParameter();
        nameParameter.ParameterName = "@bandName";
        nameParameter.Value = this.GetName();
        cmd.Parameters.Add(nameParameter);
        SqlDataReader rdr = cmd.ExecuteReader();

        while(rdr.Read())
        {
          this._id = rdr.GetInt32(0);
        }
        if (rdr !=null)
        {
          rdr.Close();
        }
        if (conn !=null)
        {
          conn.Close();
        }
      }

      public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band>{};

      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
      SqlDataReader rdr  = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        Band newBand = new Band(studentName, studentId);
        allBands.Add(newBand);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allBands;
    }

  }
}
