namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedGamePlayerRequestAndGameTeamRequestEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GamePlayerRequests",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        playerId = c.String(),
                        gameId = c.Int(nullable: false),
                        teamId = c.Int(nullable: false),
                        dateRequested = c.DateTime(nullable: false),
                        isBanned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.GameTeamRequests",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        gameId = c.Int(nullable: false),
                        teamId = c.Int(nullable: false),
                        dateRequested = c.DateTime(nullable: false),
                        isBanned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GameTeamRequests");
            DropTable("dbo.GamePlayerRequests");
        }
    }
}
