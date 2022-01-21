using NUnit.Framework;
using Crime_Finder.Models;
using System.Threading.Tasks;

namespace CrimeFinderNUnitTests
{
    public class CrimeService_Tests
    {
        [Test]
        public void Valid_User_Input_Creates_API_URL()
        {
            //Arrange
            CrimeService crimeService = new CrimeService();
            //Act
            var result = crimeService.APIUrl("-2.49810", "51.44237", "2021-01");
            //Assert
            Assert.AreEqual("https://data.police.uk/api/crimes-street/all-crime?lat=51.44237&lng=-2.49810&date=2021-01", result);
        }

        [Test]
        public async Task Get_Latest_Update_API()
        {
            CrimeService crimeService = new CrimeService();

            var result = await crimeService.GetValidLatestUpdate();
            Assert.IsNotNull(result);
            Assert.AreEqual("2021-11-01", result.date);
        }

        [Test]
        public async Task Get_Crimes_From_API_Returns_Correct_Valid()
        {
            CrimeService crimeService = new CrimeService();

            var result = await crimeService.GetCrimesFromJson(crimeService.APIUrl("-2.49810", "51.44237", "2021-01"));
            Assert.IsNotNull(result);
            Assert.AreEqual(130, result.Count);
        }

        [Test]
        public async Task Get_Crimes_From_API_Returns_Empty_Invalid_Coords()
        {
            CrimeService crimeService = new CrimeService();

            var result = await crimeService.GetCrimesFromJson(crimeService.APIUrl("0", "0", "2021-01"));
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task Get_Crimes_From_API_Returns_Null_Invalid_Date() 
        {
            CrimeService crimeService = new CrimeService();

            var result = await crimeService.GetCrimesFromJson(crimeService.APIUrl("0", "0", "4678-234"));
            Assert.IsNull(result);
        }

        [Test]
        public async Task Get_Crime_Categories_Returns_Correct_Valid()
        {
            CrimeService crimeService = new CrimeService();

            var crime = await crimeService.GetCrimesFromJson(crimeService.APIUrl("-2.49810", "51.44237", "2021-01"));
            var result = crimeService.GetCrimeCategories(crime);
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Count);
        }

    }
}