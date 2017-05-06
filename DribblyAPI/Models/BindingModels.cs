using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Models
{
    public class CourtSearchCriteria
    {
        public string courtName { get; set; }

        public int rangeMin { get; set; }

        public int rangeMax { get; set; }

        public string address { get; set; }
    }
}