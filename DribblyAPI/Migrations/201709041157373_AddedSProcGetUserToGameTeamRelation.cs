namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSProcGetUserToGameTeamRelation : DbMigration
    {
        public override void Up()
        {
            string query = @"
            CREATE PROCEDURE GetUserToGameTeamRelation
                @userId nvarchar(max),
	            @teamId int,
                @gameId int
            AS
            BEGIN
                SET NOCOUNT ON;

                    SELECT
                    CONVERT([int],@gameId) AS gameId,
                    CONVERT([int], t.teamId) AS teamId,
                     t.isTemporary AS teamIsTemporary,
		            t.requiresPassword AS teamRequiresPassword,
		            CONVERT(nvarchar(max), @userId) AS userId,
                    CONVERT([bit], CASE WHEN t.managerId = @userId THEN 1 ELSE 0 END) AS isManager,
                    CONVERT([bit], CASE WHEN(tp.isCurrentMember IS NOT NULL) THEN tp.isCurrentMember ELSE 0 END) AS isCurrentMember,
                   CONVERT([bit], 0) AS isInvited,
                    CONVERT([bit], 0) AS hasRequested,
                     CONVERT([bit], CASE WHEN(gp.playerId IS NOT NULL) THEN 1 ELSE 0 END) AS isPlaying,
                    CONVERT([bit], CASE WHEN g.creatorId = @userId THEN 1 ELSE 0 END) AS isGameCreator

                FROM GameTeams AS gt

                    INNER JOIN Games AS g ON g.gameId = gt.gameId

                    LEFT JOIN GamePlayers as gp ON gp.gameTeamId = gt.gameTeamId AND gp.playerId = @userId

                    INNER JOIN teams as t ON gt.teamId = t.teamId

                    LEFT JOIN TeamPlayers as tp ON tp.teamId = gt.teamId AND tp.playerId = gp.playerId

                WHERE gt.gameId = @gameId AND gt.teamId = @teamId


            END";

            Sql(query);
        }
        
        public override void Down()
        {
            string query = "DROP PROCEDURE GetUserToGameTeamRelation";
            Sql(query);
        }
    }
}
