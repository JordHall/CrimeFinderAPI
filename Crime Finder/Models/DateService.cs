using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crime_Finder.Models
{
    public class DateService
    {
        private int StartYear;
        private int EndYear;
        private int StartMonth;
        private int EndMonth;

        public DateService()
        {
            StartYear = 2018; //API can only get crimes from this date onwards
        }

        //Create List of valid dates
        public List<string> GetValidDates(DateModel Date)
        {
            List<string> Dates = new List<string>();
            EndYear = Int32.Parse(Date.date.Substring(0, 4));
            StartMonth = 1;
            for (var year = EndYear; year >= StartYear; year --)
            {
                if (year == EndYear) EndMonth = Int32.Parse(Date.date.Substring(5, 2));
                else EndMonth = 12;
                for (var month = EndMonth; month >= StartMonth; month --)
                {
                    Dates.Add(year.ToString() + "-" + month.ToString("00"));
                }
            }
            return Dates;
        }

        //Get string date from Date Model
        public string GetDateAsString(DateModel Date)
        {
            return Date.date;
        }
    }
}
