using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DribblyAPI.Models
{
    public class PlayerSearchCriteria
    {
        public string playerName { get; set; }

        public string sex { get; set; }

        public int? heightFtMin { get; set; }

        public int? heightInMin { get; set; }

        public int? heightFtMax { get; set; }

        public int? heightInMax { get; set; }

        public int? mvpsMin { get; set; }

        public int? mvpsMax { get; set; }

        public int? shootingMin { get; set; }

        public int? shootingMax { get; set; }

        public int? dribblingMin { get; set; }

        public int? dribblingMax { get; set; }

        public int? passingMin { get; set; }

        public int? passingMax { get; set; }

        public int? defenceMin { get; set; }

        public int? defenceMax { get; set; }

        public int? blockingMin { get; set; }

        public int? blockingMax { get; set; }

        public int? threePtsMin { get; set; }

        public int? threePtsMax { get; set; }
    }
}