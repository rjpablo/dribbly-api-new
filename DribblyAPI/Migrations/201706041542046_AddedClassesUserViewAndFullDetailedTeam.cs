namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedClassesUserViewAndFullDetailedTeam : DbMigration
    {
        public override void Up()
        {
            string str1 = @"CREATE VIEW dbo.FullDetailedTeams
                AS SELECT a.teamId, b.gameCount, c.winCount, d.lossCount, CAST(c.winCount as float)/(c.winCount + d.lossCount) as winningRate, a.teamName, a.isTemporary, a.logoUrl, a.dateCreated,
	            a.creatorId, a.managerId, 1 as isActive	
	            FROM Teams a
	            INNER JOIN 
		            (SELECT COUNT(*)  as gameCount, teamId
		            FROM GameTeams
		            GROUP BY teamId) b
	            ON a.teamId = b.teamId
	            INNER JOIN 
		            (SELECT COUNT(*)  as winCount, winningTeamId
		            FROM Games
		            GROUP BY winningTeamId) c
	            ON a.teamId = c.winningTeamId
	            INNER JOIN 
		            (SELECT Count(*) as lossCount, d.teamId FROM Teams d INNER JOIN
		            Games e
		            ON (d.teamId = e.teamAId OR d.teamId = e.teamBId) AND e.winningTeamId IS NOT NULL AND e.winningTeamId <> d.teamId
		            GROUP BY d.teamId) d
	            ON a.teamId = d.teamId";

            Sql(str1);

            string str2 = @"CREATE VIEW dbo.UserViews
                AS SELECT a.Id as userId, a.UserName as userName, a.Email as email, 1 as isActive 
                FROM AspNetUsers a;";

            Sql(str2);


        }
        
        public override void Down()
        {
            Sql("DROP VIEW dbo.UserViews");
            Sql("DROP VIEW dbo.FullDetailedTeams");
        }
    }
}
