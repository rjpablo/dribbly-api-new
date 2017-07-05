namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedJoinTeamInvitationListItemAndJoinTeamRequestListItem : DbMigration
    {
        public override void Up()
        {
            string str = @"CREATE VIEW dbo.JoinTeamRequestListItems
                AS SELECT        dbo.JoinTeamRequests.playerId, dbo.JoinTeamRequests.teamId, dbo.JoinTeamRequests.position, dbo.JoinTeamRequests.dateRequested, dbo.UserPhotos.fileName AS profilePic
                FROM            dbo.JoinTeamRequests INNER JOIN
                         dbo.UserProfiles ON dbo.JoinTeamRequests.playerId = dbo.UserProfiles.userId LEFT OUTER JOIN
                         dbo.UserPhotos ON dbo.UserProfiles.profilePicId = dbo.UserPhotos.id";

            Sql(str);

            string str1 = @"CREATE VIEW dbo.JoinTeamInvitationListItems
                AS SELECT        dbo.JoinTeamInvitations.teamId, dbo.JoinTeamInvitations.playerId, dbo.JoinTeamInvitations.position, dbo.JoinTeamInvitations.dateInvited, dbo.UserProfiles.userId
                FROM            dbo.JoinTeamInvitations INNER JOIN
                         dbo.UserProfiles ON dbo.JoinTeamInvitations.playerId = dbo.UserProfiles.userId LEFT OUTER JOIN
                         dbo.UserPhotos ON dbo.UserProfiles.profilePicId = dbo.UserPhotos.id";

            Sql(str1);

        }
        
        public override void Down()
        {
            Sql("DROP VIEW dbo.JoinTeamRequestListItems");
            Sql("DROP VIEW dbo.JoinTeamInvitationListItems");
        }
    }
}
