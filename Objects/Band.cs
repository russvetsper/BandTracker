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
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        Band newBand = new Band(bandName, bandId);
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

    public static Band Find(int id)
  {
    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @bandId;", conn);
    SqlParameter bandIdParameter = new SqlParameter();
    bandIdParameter.ParameterName =  "@bandId";
    bandIdParameter.Value = id.ToString();
    cmd.Parameters.Add(bandIdParameter);
    SqlDataReader rdr = cmd.ExecuteReader();

    int findBandId = 0;
    string findBandName = null;
    while(rdr.Read())
    {
      findBandId = rdr.GetInt32(0);
      findBandName = rdr.GetString(1);
    }
    Band findBand = new Band(findBandName,findBandId);

    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return findBand;

  }

  public void Update(string Name)
  {
    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("UPDATE bands SET name =@bandName output inserted.name WHERE id =@bandId;", conn);
    SqlParameter BandNameParameter = new SqlParameter();
    BandNameParameter.ParameterName = "@bandName";
    BandNameParameter.Value = Name;
    SqlParameter BandIdParameter = new SqlParameter();
    BandIdParameter.ParameterName = "@bandId";
    BandIdParameter.Value = _id;


    cmd.Parameters.Add(BandNameParameter);
    cmd.Parameters.Add(BandIdParameter);

    SqlDataReader rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      this._name = rdr.GetString(0);
    }

    if (rdr != null)
    {
      rdr.Close();
    }

    if (rdr != null)
    {
      conn.Close();
    }
  }



  }
}
