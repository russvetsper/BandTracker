using System.Collections.Generic;
using Xunit;
using System;


namespace BandTracker.Objects
{
  public class VenueTest
  {

    public VenueTest()
   {
     DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
   }

   [Fact]
   public void Test1_GetVenueName()
   {
     // arrange
     Venue newVenue = new Venue("Largo");
     // act
     string result = newVenue.GetVenueName();

     Assert.Equal("Largo", result);
   }

   [Fact]
  public void Test2_SetVenueName()
  {
    // arrange
    Venue newVenue = new Venue("Maxwell");
    newVenue.SetName("Maxwell");
    // act
    string result = newVenue.GetVenueName();

    Assert.Equal("Maxwell", result);
  }

  [Fact]
  public void Test3_SaveVenueName()
  {
    //Arrange
  Venue newVenue = new Venue("Largo");
  newVenue.Save();
//ACt
  List<Venue> allVenues = Venue.GetAll();
  Console.WriteLine(allVenues.Count);
//assert
  Assert.Equal(newVenue, allVenues[0]);
  }





  }
}
