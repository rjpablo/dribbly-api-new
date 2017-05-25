namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedSomeColumnsOfPlayerProfileAndPlayerListItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayerProfiles", "dribblingSkill", c => c.Double(nullable: false));
            AddColumn("dbo.PlayerProfiles", "passingSkill", c => c.Double(nullable: false));
            AddColumn("dbo.PlayerProfiles", "threePointSkill", c => c.Double(nullable: false));
            AddColumn("dbo.PlayerProfiles", "blockingSkill", c => c.Double(nullable: false));
            DropColumn("dbo.PlayerProfiles", "dribblingSkills");
            DropColumn("dbo.PlayerProfiles", "passingSkills");
            DropColumn("dbo.PlayerProfiles", "threePointsSkills");
            DropColumn("dbo.PlayerProfiles", "blockingSkills");

            string sql = @"ALTER VIEW PlayerListItem
                AS SELECT a.userId, a.winRate, a.rating, a.mvps, a.dribblingSkill, a.passingSkill, a.threePointSkill, a.blockingSkill, a.dateCreated, b.UserName, c.profilePic
                FROM dbo.PlayerProfiles AS a INNER JOIN
                dbo.AspNetUsers AS b ON a.userId = b.Id INNER JOIN
                dbo.UserProfiles AS c ON c.userId = b.Id";

            Sql(sql);
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlayerProfiles", "blockingSkills", c => c.Double(nullable: false));
            AddColumn("dbo.PlayerProfiles", "threePointsSkills", c => c.Double(nullable: false));
            AddColumn("dbo.PlayerProfiles", "passingSkills", c => c.Double(nullable: false));
            AddColumn("dbo.PlayerProfiles", "dribblingSkills", c => c.Double(nullable: false));
            DropColumn("dbo.PlayerProfiles", "blockingSkill");
            DropColumn("dbo.PlayerProfiles", "threePointSkill");
            DropColumn("dbo.PlayerProfiles", "passingSkill");
            DropColumn("dbo.PlayerProfiles", "dribblingSkill");

            string sql = @"ALTER VIEW dbo.PlayerListItem
                AS SELECT a.userId, a.winRate, a.rating, a.mvps, a.dribblingSkills, a.passingSkills, a.threePointsSkills, a.blockingSkills, a.dateCreated, b.UserName, c.profilePic
                FROM dbo.PlayerProfiles AS a INNER JOIN
                dbo.AspNetUsers AS b ON a.userId = b.Id INNER JOIN
                dbo.UserProfiles AS c ON c.userId = b.Id";

            Sql(sql);
        }
    }
}
