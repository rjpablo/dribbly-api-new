namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSomePropertiesToGameAndTeamClassesAddedGamePlayerAndGameTeamClasses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GamePlayers",
                c => new
                    {
                        playerId = c.Int(nullable: false),
                        gameTeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.playerId, t.gameTeamId });
            
            CreateTable(
                "dbo.GameTeams",
                c => new
                    {
                        gameTeamId = c.Int(nullable: false, identity: true),
                        gameId = c.Int(nullable: false),
                        teamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.gameTeamId);
            
            AddColumn("dbo.Games", "creatorId", c => c.String());
            AddColumn("dbo.Games", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Teams", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "isActive");
            DropColumn("dbo.Games", "dateCreated");
            DropColumn("dbo.Games", "creatorId");
            DropTable("dbo.GameTeams");
            DropTable("dbo.GamePlayers");
        }
    }
}
