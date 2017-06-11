using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    [Table("UserPhotos")]
    public class UserPhoto: BaseEntity
    {
        [Key]
        public int id { get; set; }

        public string fileName { get; set; }

        public string userId { get; set; }

        public DateTime uploadDate { get; set; }

    }
}