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

  }
}
