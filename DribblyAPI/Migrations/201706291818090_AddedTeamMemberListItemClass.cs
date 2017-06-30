namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTeamMemberListItemClass : DbMigration
    {
        public override void Up()
        {
        string str1 = @"CREATE VIEW TeamMemberListItems
                AS SELECT        TeamPlayers.playerId, TeamPlayers.teamId, UserPhotos.fileName AS profilePic, AspNetUsers.UserName AS userName, TeamPlayers.hasLeft, TeamPlayers.dateJoined, 
                         TeamPlayers.dateLeft
                FROM            Teams INNER JOIN
                                         TeamPlayers ON Teams.teamId = TeamPlayers.teamId INNER JOIN
                                         UserPhotos LEFT JOIN
                                         UserProfiles ON UserPhotos.id = UserProfiles.profilePicId ON TeamPlayers.playerId = UserProfiles.userId INNER JOIN
                                         AspNetUsers ON UserProfiles.userId = AspNetUsers.Id";

            Sql(str1);


    }

    public override void Down()
    {
        Sql("DROP VIEW TeamMemberListItems");
    }
}
}
