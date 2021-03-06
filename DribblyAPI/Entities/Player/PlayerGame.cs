﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DribblyAPI.Entities
{
    /// <summary>
    /// A class that represent a player's game with a specific team.
    /// Properties like remaining fouls, points made, and assists
    /// will be added here
    /// </summary>
    public class GamePlayer:BaseEntity
    {
        [Key,Column(Order =1)]
        public string playerId { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("gameTeam")]
        public int gameTeamId { get; set; }

        public GameTeam gameTeam { get; set; }

    }
}