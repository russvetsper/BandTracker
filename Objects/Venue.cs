using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker.Objects
{
  public class Venue
  {
    private int _id;
    private string _venueName;

    public Venue(string venueName, int id=0)
    {
      _id = id;
      _venueName = venueName;
    }

    public string GetVenueName()
    {
      return _venueName;
    }

    public int GetId()
    {
      return _id;
    }

    public void SetName(string newVenueName)
    {
      _venueName = newVenueName;
    }


    public override bool Equals(System.Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        return this.GetVenueName().Equals(newVenue.GetVenueName());
      }
    }

    public override int GetHashCode()
    {
      return this.GetVenueName().GetHashCode();
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};

      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr  = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        Venue newVenue = new Venue(venueName, venueId);
        allVenues.Add(newVenue);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allVenues;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues(Name)OUTPUT INSERTED.id VALUES (@venuName);", conn );
      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@venuName";
      nameParameter.Value = this.GetVenueName();
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

    public static Venue Find(int id)
        {
          SqlConnection conn = DB.Connection();
          conn.Open();

          SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @venueId;", conn);
          SqlParameter venueIdParameter = new SqlParameter();
          venueIdParameter.ParameterName =  "@venueId";
          venueIdParameter.Value = id.ToString();
          cmd.Parameters.Add(venueIdParameter);
          SqlDataReader rdr = cmd.ExecuteReader();

          int findVenueId = 0;
          string findVenueName = null;
          while(rdr.Read())
          {
            findVenueId = rdr.GetInt32(0);
            findVenueName = rdr.GetString(1);;
          }
          Venue findVenue = new Venue(findVenueName,findVenueId);

          if (rdr != null)
          {
            rdr.Close();
          }
          if (conn != null)
          {
            conn.Close();
          }
          return findVenue;

        }





  }
}
