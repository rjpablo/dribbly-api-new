using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    public class Team : BaseEntity
    {
        [Key]
        public int teamId { get; set; }

        [Required]
        [StringLength(15,MinimumLength = 5,ErrorMessage ="Team Name must be 5 to 15 characters long.")]
        public string teamName { get; set; }

        public bool isTemporary { get; set; }

        public string logoUrl { get; set; }

        public DateTime dateCreated { get; set; }
        
        [Required(ErrorMessage ="Creator is required but is missing.")]
        public string creatorId { get; set; }

        [Required(ErrorMessage = "Manager is required but is missing.")]
        public string managerId { get; set; }

        public bool isActive { get; set; }

        public int? cityId { get; set; }

        public int? homeCourtId { get; set; }

        public int? coachId { get; set; }

    }
}