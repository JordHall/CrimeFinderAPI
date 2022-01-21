using NUnit.Framework;
using Crime_Finder.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CrimeFinderNUnitTests
{
    class DateService_UnitTest
    {
        [Test]
        public void Valid_Date_String_Returns()
        {
            //Arrange
            DateService dateService = new DateService();
            DateModel date = new DateModel();
            date.date = "2021-01";
            //Act
            var result = dateService.GetDateAsString(date);
            //Assert
            Assert.AreEqual("2021-01", result);
        }

        [Test]
        public async Task Valid_Dates_Return_From_DateModel_List_Input()
        {
            DateService dateService = new DateService();
            DateModel date = new DateModel();
            date.date = "2021-01";
            var result = dateService.GetValidDates(date);
            Assert.IsNotNull(result);
            Assert.AreEqual(37, result.Count);
            Assert.AreEqual("2018-01", result[result.Count-1]);
        }
    }
}
