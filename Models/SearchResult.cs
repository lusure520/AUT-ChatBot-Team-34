using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot_Application2.Models
{
    public class SearchResult
    {
        [JsonProperty("@odata.context")]

        public string odatacontext { get; set; }

        public value[] value { get; set; }
    }

    public class value
    {
        [JsonProperty("@search.score")]

        public float searchscore { get; set; }

        public string Name { get; set; }

        public string Level { get; set; }

        public string id { get; set; }

        public string Credit { get; set; }

        public string Year { get; set; }

        public string Semester { get; set; }

        public string Type { get; set; }

        public string Requisite { get; set; }

        public string rid { get; set; }
    }
}