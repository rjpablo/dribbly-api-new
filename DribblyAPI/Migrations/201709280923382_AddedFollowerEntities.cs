namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFollowerEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameFollowers",
                c => new
                    {
                        gameId = c.Int(nullable: false),
                        userId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.gameId, t.userId });
            
            CreateTable(
                "dbo.TeamFollowers",
                c => new
                    {
                        teamId = c.Int(nullable: false),
                        userId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.teamId, t.userId });
            
            CreateTable(
                "dbo.UserFollowers",
                c => new
                    {
                        userId = c.String(nullable: false, maxLength: 128),
                        followerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.userId, t.followerId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserFollowers");
            DropTable("dbo.TeamFollowers");
            DropTable("dbo.GameFollowers");
        }
    }
}
