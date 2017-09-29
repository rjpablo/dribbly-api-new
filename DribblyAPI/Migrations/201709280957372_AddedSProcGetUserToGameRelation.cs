namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSProcGetUserToGameRelation : DbMigration
    {
        public override void Up()
        {
            string sql = @"
                CREATE PROCEDURE [dbo].[GetUserToGameRelation]
                @userId nvarchar(max),
                @gameId int
            AS
            BEGIN
                SET NOCOUNT ON;

                Select 
					@gameId AS gameId,
					g.isOver AS gameIsOver,
					(SELECT COUNT(*) FROM GameTeams WHERE gameId = @gameId) AS teamCount,
					CONVERT(nvarchar(max), @userId) AS userId,
					CONVERT([bit], CASE WHEN g.creatorId = @userId THEN 1 ELSE 0 END) AS isCreator,
					CONVERT([bit], CASE WHEN (SELECT COUNT(*) FROM GameTeamRequests WHERE gameId = @gameId AND teamId IN (SELECT teamId FROM teams WHERE managerId = @userId) ) > 0 THEN 1 ELSE 0 END) AS hasRequestedAsTeam,
					CONVERT([bit], (SELECT COUNT(*) FROM GameBannedUsers WHERE gameId = @gameId AND userId = @userId)) AS isBanned,
					CONVERT([bit], CASE WHEN(gp.playerId IS NOT NULL) THEN 1 ELSE 0 END) AS isPlaying,
					CONVERT([bit], (SELECT COUNT(*) FROM GameFollowers WHERE gameId = @gameId AND userId = @userId)) AS isFollowing,
					CONVERT([bit], CASE WHEN(SELECT COUNT(*) FROM teams WHERE managerId = @userId) > 0 THEN 1 ELSE 0 END) AS managesTeam,
					CONVERT([bit], CASE WHEN (
						Select COUNT(*)
						FROM Games
							INNER JOIN Teams
							ON teamId IN (teamAId, teamBId) AND managerId = @userId
						GROUP BY gameId
						Having gameId = @gameId
						) > 0 THEN 1 ELSE 0 END) AS managedTeamIsPlaying

				FROM (Select * FROM Games WHERE gameId = @gameId) AS g 
					LEFT JOIN GameTeams AS gt
						INNER JOIN GamePlayers as gp
						ON gp.gameTeamId = gt.gameTeamId AND gp.playerId = @userId AND gt.gameId = @gameId		 
					ON g.gameId = gt.gameId
            END";
            Sql(sql);
        }
        
        public override void Down()
        {
            string query = "DROP PROCEDURE [dbo].[GetUserToGameRelation]";
            Sql(query);
        }
    }
}
