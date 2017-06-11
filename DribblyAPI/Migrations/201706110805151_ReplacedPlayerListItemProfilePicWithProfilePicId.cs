namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReplacedPlayerListItemProfilePicWithProfilePicId : DbMigration
    {
        public override void Up()
        {
            string sql = @"ALTER VIEW PlayerListItem
                    AS SELECT a.userId, a.winRate, a.rating, a.mvps, a.dribblingSkill, a.passingSkill, a.threePointSkill, a.blockingSkill, a.dateCreated AS dateJoined, b.UserName, c.profilePicId, c.heightFt, c.heightIn, c.sex, a.isActive, a.shootingSkill, 
                         a.defensiveSkill
                    FROM            dbo.PlayerProfiles AS a INNER JOIN
                         dbo.AspNetUsers AS b ON a.userId = b.Id INNER JOIN
                         dbo.UserProfiles AS c ON c.userId = b.Id";

            Sql(sql);
        }

        public override void Down()
        {
            string sql = @"ALTER VIEW PlayerListItem
                    AS SELECT a.userId, a.winRate, a.rating, a.mvps, a.dribblingSkill, a.passingSkill, a.threePointSkill, a.blockingSkill, a.dateCreated AS dateJoined, b.UserName, c.profilePic, c.heightFt, c.heightIn, c.sex, a.isActive, a.shootingSkill, 
                         a.defensiveSkill
                    FROM            dbo.PlayerProfiles AS a INNER JOIN
                         dbo.AspNetUsers AS b ON a.userId = b.Id INNER JOIN
                         dbo.UserProfiles AS c ON c.userId = b.Id";

            Sql(sql);
        }
    }
}
