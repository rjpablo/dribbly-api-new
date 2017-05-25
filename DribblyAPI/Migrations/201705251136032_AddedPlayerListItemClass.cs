namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPlayerListItemClass : DbMigration
    {
        public override void Up()
        {
            string sql = @"CREATE VIEW dbo.PlayerListItem
                AS SELECT a.userId, a.winRate, a.rating, a.mvps, a.dribblingSkills, a.passingSkills, a.threePointsSkills, a.blockingSkills, a.dateCreated, b.UserName, c.profilePic
                FROM dbo.PlayerProfiles AS a INNER JOIN
                dbo.AspNetUsers AS b ON a.userId = b.Id INNER JOIN
                dbo.UserProfiles AS c ON c.userId = b.Id";

            Sql(sql);

        }
        
        public override void Down()
        {
            Sql("DROP VIEW dbo.PlayerListItem");
        }
    }
}
