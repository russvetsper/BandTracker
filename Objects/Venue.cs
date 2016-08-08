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


    }
  }
