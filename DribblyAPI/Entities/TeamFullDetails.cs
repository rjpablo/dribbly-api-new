using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DribblyAPI.Entities
{
    /// <summary>
    /// This class is mapped to a database view
    /// </summary>
    public class FullDetailedTeam
    {

        /** Fields from Team class - Start **/

        [Key]
        public int teamId { get; set; }

        [Required]
        public string teamName { get; set; }

        public bool isTemporary { get; set; }

        public string logoUrl { get; set; }

        public DateTime dateCreated { get; set; }
        
        [ForeignKey("creator")]
        public string creatorId { get; set; }

        [ForeignKey("manager")]
        public string managerId { get; set; }

        public bool isActive { get; set; }

        /** Fields from Team class - End **/

        public int winCount { get; set; }

        public int lossCount { get; set; }

        public int gameCount { get; set; }

        public double winningRate { get; set; }

        public UserView creator { get; set; }

        public UserView manager { get; set; }

    }
}