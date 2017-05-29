using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class Cities
    {
        [Key]
        public int cityId { get; set; }

        public string shortName { get; set; }

        public string longName { get; set; }

        [ForeignKey("country")]
        public int countryId { get; set; }

        public virtual Country country { get; set; }

    }
}