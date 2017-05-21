using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class PlayerProfile : BaseEntity
    {
        [Key]
        public string   userId { get; set; }

        public int mvps { get; set; }

        public double winRate { get; set; }

        public double rating { get; set; }

        public double dribblingSkills { get; set; }

        public double passingSkills { get; set; }

        public double threePointsSkills { get; set; }

        public double blockingSkills { get; set; }

        public DateTime dateCreated { get; set; }

    }
}