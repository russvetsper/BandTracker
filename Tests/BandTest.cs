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
    Band newBand = new Band("Metallica");
    newBand.SetName("Beatles");
    // act
    string result = newBand.GetName();

    Assert.Equal("Beatles", result);
  }

  [Fact]
     public void Test3_Save()
     {
       //arrange
       Band testBand = new Band("Beatles");
       //act
       testBand.Save();
       List<Band> result = Band.GetAll();
       List<Band> testList = new List<Band>{testBand};
       //Assert
       Assert.Equal(testList, result);
     }




  }
}
