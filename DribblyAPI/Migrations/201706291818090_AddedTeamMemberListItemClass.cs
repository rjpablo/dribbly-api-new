namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTeamMemberListItemClass : DbMigration
    {
        public override void Up()
        {
            string str1 = @"CREATE VIEW TeamMemberListItems
                AS SELECT        TOP (100) PERCENT dbo.TeamPlayers.playerId, dbo.TeamPlayers.teamId, dbo.UserPhotos.fileName AS profilePic, dbo.AspNetUsers.UserName, dbo.TeamPlayers.hasLeft, dbo.TeamPlayers.dateJoined, 
                         dbo.TeamPlayers.dateLeft
                FROM            dbo.Teams INNER JOIN
                         dbo.TeamPlayers ON dbo.Teams.teamId = dbo.TeamPlayers.teamId INNER JOIN
                         dbo.UserProfiles ON dbo.UserProfiles.userId = dbo.TeamPlayers.playerId INNER JOIN
                         dbo.AspNetUsers ON dbo.UserProfiles.userId = dbo.AspNetUsers.Id LEFT OUTER JOIN
                         dbo.UserPhotos ON dbo.UserPhotos.id = dbo.UserProfiles.profilePicId";

            Sql(str1);


    }

    public override void Down()
    {
        Sql("DROP VIEW TeamMemberListItems");
    }
}
}
