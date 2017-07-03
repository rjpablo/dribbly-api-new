namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedSProcGetUserToTeamRelationToReflectCorrectValueForHasRequestedColumn : DbMigration
    {
        public override void Up()
        {
            string query = @"ALTER PROCEDURE [dbo].[GetUserToTeamRelation]
	            @userId nvarchar(max),
	            @teamId int
            AS
            BEGIN
	            SELECT 
					CONVERT([nvarchar], @userId) AS userId,
		            CONVERT([int],a.teamId) AS teamId,
		            CONVERT([bit], CASE WHEN a.creatorId = @userId THEN 1 ELSE 0 END) AS isOwner,
		            CONVERT([bit], CASE WHEN b.playerId IS NOT NULL THEN 1 ELSE 0 END) AS isMember,
		            CONVERT([bit], CASE WHEN (b.hasLeft IS NULL OR b.hasLeft = 0) THEN 1 ELSE 0 END) AS isCurrentMember,
		            CONVERT([bit], CASE WHEN c.playerId IS NOT NULL THEN 1 ELSE 0 END) AS isInvited,
		            CONVERT([bit], CASE WHEN d.playerId IS NOT NULL THEN 1 ELSE 0 END) AS hasRequested
	            FROM dbo.Teams AS a
	            LEFT JOIN dbo.TeamPlayers AS b ON b.teamId = @teamId AND b.playerId = @userId
	            LEFT JOIN dbo.JoinTeamInvitations AS c ON c.teamId = a.teamId AND c.playerId = @userId
				LEFT JOIN dbo.JoinTeamRequests AS d ON d.teamId = @teamId AND d.playerId = @userId
	            WHERE a.teamId = @teamId
            END";

            Sql(query);

        }
        
        public override void Down()
        {
            string query = @"ALTER PROCEDURE [dbo].[GetUserToTeamRelation]
	            @userId nvarchar(max),
	            @teamId int
            AS
            BEGIN
	            SELECT 
					CONVERT([nvarchar], @userId) AS userId,
		            CONVERT([int],a.teamId) AS teamId,
		            CONVERT([bit], CASE WHEN a.creatorId = @userId THEN 1 ELSE 0 END) AS isOwner,
		            CONVERT([bit], CASE WHEN b.playerId IS NOT NULL THEN 1 ELSE 0 END) AS isMember,
		            CONVERT([bit], CASE WHEN (b.hasLeft IS NULL OR b.hasLeft = 0) THEN 1 ELSE 0 END) AS isCurrentMember,
		            CONVERT([bit], CASE WHEN c.playerId IS NOT NULL THEN 1 ELSE 0 END) AS isInvited,
		            CONVERT([bit], 0) AS hasRequested
	            FROM dbo.Teams AS a
	            LEFT JOIN dbo.TeamPlayers AS b ON b.teamId = @teamId AND b.playerId = @userId
	            LEFT JOIN dbo.JoinTeamInvitations AS c ON c.teamId = a.teamId AND c.playerId = @userId
	            WHERE a.teamId = @teamId
            END";

            Sql(query);
        }
    }
}
