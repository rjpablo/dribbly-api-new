﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class Court: BaseEntity
    {
        [Key]
        public int id { get; set; }
        
        public string name { get; set; }

        public string contactNo { get; set; }

        public string address { get; set; }

        public string description { get; set; }

        public double rate { get; set; }

        [EmailAddress]
        public string email { get; set; }

        /// <summary>
        /// The filename of the primary photo (with extension).
        /// </summary>
        public string imagePath { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }

        public int cityId { get; set; }

        /// <summary>
        /// The id of the user who registered this court.
        /// </summary>
        [ForeignKey("owner")]
        public string userId { get; set; }

        public IdentityUser owner { get; set; }

        public DateTime dateRegistered { get; set; }

        public List<CourtPhoto> photos { get; set; }

    }
}