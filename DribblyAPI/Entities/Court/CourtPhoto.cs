using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    [Table("CourtPhotos")]
    public class CourtPhoto : BaseEntity
    {
        [Key]
        public int id { get; set; }

        public string fileName { get; set; }

        public int courtId { get; set; }

        [ForeignKey("courtId")]
        public virtual Court court { get; set; }

    }
}