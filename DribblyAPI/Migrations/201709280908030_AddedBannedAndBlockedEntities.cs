namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBannedAndBlockedEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameBannedUsers",
                c => new
                    {
                        gameId = c.Int(nullable: false),
                        userId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.gameId, t.userId });
            
            CreateTable(
                "dbo.TeamBannedPlayers",
                c => new
                    {
                        teamId = c.Int(nullable: false),
                        userId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.teamId, t.userId });
            
            CreateTable(
                "dbo.UserBlockedUsers",
                c => new
                    {
                        userId = c.String(nullable: false, maxLength: 128),
                        bannedUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.userId, t.bannedUserId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserBlockedUsers");
            DropTable("dbo.TeamBannedPlayers");
            DropTable("dbo.GameBannedUsers");
        }
    }
}
