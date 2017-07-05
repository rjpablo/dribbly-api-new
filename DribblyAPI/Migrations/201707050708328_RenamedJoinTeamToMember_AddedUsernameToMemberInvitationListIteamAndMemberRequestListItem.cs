namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedJoinTeamToMember_AddedUsernameToMemberInvitationListIteamAndMemberRequestListItem : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.JoinTeamInvitations", newName: "MemberInvitations");
            RenameTable(name: "dbo.JoinTeamRequests", newName: "MemberRequests");

            string renameViews = @"EXEC sp_rename 'dbo.JoinTeamInvitationListItems', 'MemberInvitationListItems'
                GO
                EXEC sp_rename 'dbo.JoinTeamRequestListItems', 'MemberRequestListItems'
                GO";

            string alterViews = @"
                ALTER VIEW dbo.MemberRequestListItems
                AS SELECT        a.playerId, a.teamId, b.UserName AS userName, a.position, a.dateRequested, d.fileName AS profilePic
                FROM dbo.MemberRequests AS a INNER JOIN
                    dbo.AspNetUsers AS b ON a.playerId = b.Id LEFT OUTER JOIN
                    dbo.UserProfiles AS c ON a.playerId = c.userId LEFT OUTER JOIN
                    dbo.UserPhotos AS d ON c.profilePicId = d.id
                GO
                ALTER VIEW dbo.MemberInvitationListItems
                AS SELECT a.teamId, a.playerId, c.UserName AS userName, a.position, a.dateInvited, d.fileName AS profilePic
                FROM dbo.MemberInvitations AS a INNER JOIN
                    dbo.UserProfiles AS b ON a.playerId = b.userId INNER JOIN
                    dbo.AspNetUsers AS c ON a.playerId = c.Id LEFT OUTER JOIN
                    dbo.UserPhotos AS d ON b.profilePicId = d.id
                GO";

            string alterSproc = @"
                ALTER PROCEDURE [dbo].[GetUserToTeamRelation]
	                @userId nvarchar(max),
	                @teamId int
                AS
                BEGIN
	                SELECT 
					    CONVERT([nvarchar], @userId) AS userId,
		                CONVERT([int],a.teamId) AS teamId,
		                CONVERT([bit], CASE WHEN a.creatorId = @userId THEN 1 ELSE 0 END) AS isOwner,
		                CONVERT([bit], CASE WHEN b.playerId IS NOT NULL THEN 1 ELSE 0 END) AS isMember,
		                CONVERT([bit], CASE WHEN (b.isCurrentMember IS NOT NULL) THEN b.isCurrentMember ELSE 0 END) AS isCurrentMember,
		                CONVERT([bit], CASE WHEN c.playerId IS NOT NULL THEN 1 ELSE 0 END) AS isInvited,
		                CONVERT([bit], CASE WHEN d.playerId IS NOT NULL THEN 1 ELSE 0 END) AS hasRequested
	                FROM dbo.Teams AS a
	                LEFT JOIN dbo.TeamPlayers AS b ON b.teamId = @teamId AND b.playerId = @userId
	                LEFT JOIN dbo.MemberInvitations AS c ON c.teamId = a.teamId AND c.playerId = @userId
				    LEFT JOIN dbo.MemberRequests AS d ON d.teamId = @teamId AND d.playerId = @userId
	                WHERE a.teamId = @teamId
                END
                GO";

            Sql(renameViews);
            Sql(alterViews);
            Sql(alterSproc);
        }
        
        public override void Down()
        {

            string str = @"ALTER PROCEDURE [dbo].[GetUserToTeamRelation]
	                @userId nvarchar(max),
	                @teamId int
                AS
                BEGIN
	                SELECT 
					    CONVERT([nvarchar], @userId) AS userId,
		                CONVERT([int],a.teamId) AS teamId,
		                CONVERT([bit], CASE WHEN a.creatorId = @userId THEN 1 ELSE 0 END) AS isOwner,
		                CONVERT([bit], CASE WHEN b.playerId IS NOT NULL THEN 1 ELSE 0 END) AS isMember,
		                CONVERT([bit], CASE WHEN (b.isCurrentMember IS NOT NULL) THEN b.isCurrentMember ELSE 0 END) AS isCurrentMember,
		                CONVERT([bit], CASE WHEN c.playerId IS NOT NULL THEN 1 ELSE 0 END) AS isInvited,
		                CONVERT([bit], CASE WHEN d.playerId IS NOT NULL THEN 1 ELSE 0 END) AS hasRequested
	                FROM dbo.Teams AS a
	                LEFT JOIN dbo.TeamPlayers AS b ON b.teamId = @teamId AND b.playerId = @userId
	                LEFT JOIN dbo.JoinTeamInvitations AS c ON c.teamId = a.teamId AND c.playerId = @userId
				    LEFT JOIN dbo.JoinTeamRequests AS d ON d.teamId = @teamId AND d.playerId = @userId
	                WHERE a.teamId = @teamId
                END
                GO
                ALTER VIEW dbo.MemberRequestListItems
                AS SELECT dbo.JoinTeamRequests.playerId, dbo.JoinTeamRequests.teamId, dbo.JoinTeamRequests.position, dbo.JoinTeamRequests.dateRequested, dbo.UserPhotos.fileName AS profilePic
                FROM dbo.JoinTeamRequests INNER JOIN
                    dbo.UserProfiles ON dbo.JoinTeamRequests.playerId = dbo.UserProfiles.userId LEFT OUTER JOIN
                    dbo.UserPhotos ON dbo.UserProfiles.profilePicId = dbo.UserPhotos.id
                GO
                ALTER VIEW dbo.MemberInvitationListItems
                AS SELECT dbo.JoinTeamInvitations.teamId, dbo.JoinTeamInvitations.playerId, dbo.JoinTeamInvitations.position, dbo.JoinTeamInvitations.dateInvited, dbo.UserProfiles.userId
                FROM dbo.JoinTeamInvitations INNER JOIN
                    dbo.UserProfiles ON dbo.JoinTeamInvitations.playerId = dbo.UserProfiles.userId LEFT OUTER JOIN
                    dbo.UserPhotos ON dbo.UserProfiles.profilePicId = dbo.UserPhotos.id
                GO
                EXEC sp_rename 'dbo.MemberInvitationListItems', 'JoinTeamInvitationListItems'
                GO
                EXEC sp_rename 'dbo.MemberRequestListItems', 'JoinTeamRequestListItems'
                GO";

            Sql(str);
            RenameTable(name: "dbo.MemberRequests", newName: "JoinTeamRequests");
            RenameTable(name: "dbo.MemberInvitations", newName: "JoinTeamInvitations");

        }
    }
}
