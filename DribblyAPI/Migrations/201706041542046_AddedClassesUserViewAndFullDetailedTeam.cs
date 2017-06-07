namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedClassesUserViewAndFullDetailedTeam : DbMigration
    {
        public override void Up()
        {
            string str1 = @"CREATE VIEW dbo.FullDetailedTeams
                AS SELECT        a.teamId, b.gameCount, c.winCount, d_1.lossCount, CAST(c.winCount AS float) / (c.winCount + d_1.lossCount) AS winningRate, a.teamName, a.isTemporary, a.logoUrl, a.dateCreated, a.creatorId, a.managerId, 
                         ISNULL(CONVERT([bit], CASE WHEN 1 > 1 THEN 1 ELSE 0 END), 0) AS isActive
                FROM            dbo.Teams AS a INNER JOIN
                             (SELECT        COUNT(*) AS gameCount, teamId
                               FROM            dbo.GameTeams
                               GROUP BY teamId) AS b ON a.teamId = b.teamId INNER JOIN
                             (SELECT        COUNT(*) AS winCount, winningTeamId
                               FROM            dbo.Games
                               GROUP BY winningTeamId) AS c ON a.teamId = c.winningTeamId INNER JOIN
                             (SELECT        COUNT(*) AS lossCount, d.teamId
                               FROM            dbo.Teams AS d INNER JOIN
                                                         dbo.Games AS e ON (d.teamId = e.teamAId OR
                                                         d.teamId = e.teamBId) AND e.winningTeamId IS NOT NULL AND e.winningTeamId <> d.teamId
                               GROUP BY d.teamId) AS d_1 ON a.teamId = d_1.teamId";

            Sql(str1);

            string str2 = @"CREATE VIEW dbo.UserViews
                AS SELECT        Id AS userId, UserName, Email, ISNULL(CONVERT([bit], CASE WHEN 1 = 1 THEN 1 ELSE 0 END), 0) AS isActive
                FROM            dbo.AspNetUsers AS a";

            Sql(str2);


        }
        
        public override void Down()
        {
            Sql("DROP VIEW dbo.UserViews");
            Sql("DROP VIEW dbo.FullDetailedTeams");
        }
    }
}
