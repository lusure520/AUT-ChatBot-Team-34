using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Bot_Application2.Models
{
    [Serializable]

    public class AzureSearchService
    {
        private static readonly string QueryString = $"https://{WebConfigurationManager.AppSettings["SearchName"]}.search.windows.net/indexes/{WebConfigurationManager.AppSettings["IndexName"]}/docs?api-key={WebConfigurationManager.AppSettings["SearchKey"]}&api-version=2016-09-01&";

        public async Task<SearchResult> SearchByMajorName(string name)
        {
            using (var httpClient = new HttpClient())
            {
                string nameQuery = $"{QueryString}search={name}";
                try
                {
                    string response = await httpClient.GetStringAsync(nameQuery);
                    return JsonConvert.DeserializeObject<SearchResult>(response);
                }
                catch (Exception ex)
                {
                    string e = ex.Message;
                }
                return (new SearchResult());
            }
        }
    }
}