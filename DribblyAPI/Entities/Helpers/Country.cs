using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class Country : BaseEntity
    {
        [Key]
        public int countryId { get; set; }

        public string shortName { get; set; }

        public string longName { get; set; }
    }
}