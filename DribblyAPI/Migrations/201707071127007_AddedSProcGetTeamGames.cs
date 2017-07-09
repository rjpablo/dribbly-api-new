namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSProcGetTeamGames : DbMigration
    {
        public override void Up()
        {
            string query = @"CREATE PROCEDURE [dbo].[GetTeamGames]
	            @teamId int
            AS
            BEGIN
                SELECT a.gameId,
	                a.title,
	                CASE WHEN @teamId = b.teamId THEN b.teamId ELSE c.teamId END AS teamId,
	                CASE WHEN @teamId = b.teamId THEN b.teamName ELSE c.teamName END AS teamName,
	                CASE WHEN @teamId = b.teamId THEN a.teamAScore ELSE a.teamBScore END AS score,
	                CASE WHEN @teamId = b.teamId THEN c.teamId ELSE b.teamId END AS opponentTeamId,
	                CASE WHEN @teamId = b.teamId THEN c.teamName ELSE b.teamName END AS opponentTeamName,
	                CASE WHEN @teamId = b.teamId THEN a.teamBScore ELSE a.teamAScore END AS opponentScore,
	                CASE WHEN a.winningTeamId IS NOT NULL THEN
		                CONVERT([bit], CASE WHEN @teamId = a.winningTeamId THEN 1 ELSE 0 END)
	                ELSE NULL END AS isWon,
	                e.name AS courtName,
	                e.id AS courtId,
                    a.schedule
                FROM dbo.Games AS a LEFT OUTER JOIN
                    dbo.Teams AS b ON a.teamAId = b.teamId LEFT OUTER JOIN
                    dbo.Teams AS c ON a.teamBId = c.teamId LEFT OUTER JOIN
                    dbo.Teams AS d ON a.winningTeamId = d.teamId INNER JOIN
                    dbo.Courts AS e ON a.courtId = e.id
                WHERE a.teamAId = @teamId OR a.teamBId = @teamId
            END";

            Sql(query);
        }
        
        public override void Down()
        {
            string query = "DROP PROCEDURE [dbo].[GetTeamGames]";
            Sql(query);
        }
    }
}
