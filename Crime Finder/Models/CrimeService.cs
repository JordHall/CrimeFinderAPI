using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Crime_Finder.Models
{
    //Handles Crime Data
    public class CrimeService
    {
        public HttpClient Http;

        public CrimeService()
        {
            Http = new HttpClient();
        }

        //Create API Url
        public string APIUrl(string longitude, string latitude, string date)
        {
            return "https://data.police.uk/api/crimes-street/all-crime?lat=" + latitude + "&lng=" + longitude + "&date=" + date;
        }

        //Get Latest Updated Date JSON
        public async Task<DateModel> GetValidLatestUpdate()
        {
            return await Http.GetFromJsonAsync<DateModel>("https://data.police.uk/api/crime-last-updated");
        }

        //Get JSON as Crimes
        public async Task<List<Crime>> GetCrimesFromJson(string uri)
        {
            try
            {
                return await Http.GetFromJsonAsync<List<Crime>>(uri);
            }
            catch (HttpRequestException e) // Non success
            {
                var test = e;
                return null;
            }
            catch (NotSupportedException e) // When content type is not valid
            {
                var test = e;
                return null;
            }
            catch (JsonException e) // Invalid JSON
            {
                var test = e;
                return null;
            }
        }

        //Create Crime Categories Summary ViewModel
        public List<CrimeViewModel> GetCrimeCategories(List<Crime> crimes)
        {
            if (crimes == null) return new List<CrimeViewModel>();
            List<CrimeViewModel> CrimeCategories = new List<CrimeViewModel>();
            var categories = crimes.Select(i => i.category).Distinct().ToList();
            categories.ForEach(c =>
            {
                var viewmodel = new CrimeViewModel();
                viewmodel.Category = c;
                viewmodel.Count = crimes.Where(i => i.category == c).Count();
                CrimeCategories.Add(viewmodel);
            });
            return CrimeCategories.OrderByDescending(c => c.Count).ToList();
        }
    }
}
