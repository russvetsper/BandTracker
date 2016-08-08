using System.Collections.Generic;
using Xunit;
using System;


namespace BandTracker.Objects
{
  public class BandTest
  {

    public BandTest()
   {
     DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Band_Tracker_test;Integrated Security=SSPI;";
   }



    [Fact]
    public void Test1_BandGetName()
    {
      // arrange
      Band newBand = new Band("Metallica");
      // act
      string result = newBand.GetName();
      // assert
      Assert.Equal("Metallica", result);
    }

    [Fact]
  public void Test2_SetName()
  {
    // arrange
    Band newBand = new Band("U2");
    newBand.SetName("U2");
    // act
    string result = newBand.GetName();

    Assert.Equal("U2", result);
  }

  [Fact]
      public void Test4_SaveBand()
      {
        //Arrange
      Band newBand = new Band("Beatles");
      newBand.Save();
        //ACt
      List<Band> allBands = Band.GetAll();
      //Console.WriteLine(allBands[0].GetName());
        //assert
      Assert.Equal(newBand, allBands[0]);
      }


     [Fact]
  public void Test5_FindId()
  {
    Band newBand = new Band ("Beatles");
    newBand.Save();

    Band findBand = Band.Find(newBand.GetId());

    Assert.Equal(findBand, newBand);
  }

  [Fact]
    public void Test6_UpdateBand()
    {
      Band newBand = new Band("Metallica");
      newBand.Save();
      newBand.Update("Metallica");
      string result = newBand.GetName();

      Assert.Equal("Metallica", result);
    }





  }
}
