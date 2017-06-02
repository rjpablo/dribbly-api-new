namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPropertiesToGameClassAddedTeamAndTeamPlayerClasses : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Games");
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        teamId = c.Int(nullable: false, identity: true),
                        teamName = c.String(),
                        isTemporary = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.teamId);
            
            CreateTable(
                "dbo.TeamPlayers",
                c => new
                    {
                        playerId = c.String(nullable: false, maxLength: 128),
                        teamId = c.Int(nullable: false),
                        hasLeft = c.Boolean(nullable: false),
                        dateJoined = c.DateTime(),
                        dateLeft = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.playerId, t.teamId })
                .ForeignKey("dbo.PlayerProfiles", t => t.playerId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.teamId, cascadeDelete: true)
                .Index(t => t.playerId)
                .Index(t => t.teamId);

            DropColumn("dbo.Games", "id");
            AddColumn("dbo.Games", "gameId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Games", "schedule", c => c.DateTime(nullable: false));
            AddColumn("dbo.Games", "teamAScore", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "teamBScore", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "courtId", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "teamAId", c => c.Int());
            AddColumn("dbo.Games", "teamBId", c => c.Int());
            AddColumn("dbo.Games", "winningTeamId", c => c.Int());
            AlterColumn("dbo.Games", "title", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Games", "gameId");
            CreateIndex("dbo.Games", "courtId");
            CreateIndex("dbo.Games", "teamAId");
            CreateIndex("dbo.Games", "teamBId");
            CreateIndex("dbo.Games", "winningTeamId");
            AddForeignKey("dbo.Games", "courtId", "dbo.Courts", "id", cascadeDelete: true);
            AddForeignKey("dbo.Games", "teamAId", "dbo.Teams", "teamId");
            AddForeignKey("dbo.Games", "teamBId", "dbo.Teams", "teamId");
            AddForeignKey("dbo.Games", "winningTeamId", "dbo.Teams", "teamId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamPlayers", "teamId", "dbo.Teams");
            DropForeignKey("dbo.TeamPlayers", "playerId", "dbo.PlayerProfiles");
            DropForeignKey("dbo.Games", "winningTeamId", "dbo.Teams");
            DropForeignKey("dbo.Games", "teamBId", "dbo.Teams");
            DropForeignKey("dbo.Games", "teamAId", "dbo.Teams");
            DropForeignKey("dbo.Games", "courtId", "dbo.Courts");
            DropIndex("dbo.TeamPlayers", new[] { "teamId" });
            DropIndex("dbo.TeamPlayers", new[] { "playerId" });
            DropIndex("dbo.Games", new[] { "winningTeamId" });
            DropIndex("dbo.Games", new[] { "teamBId" });
            DropIndex("dbo.Games", new[] { "teamAId" });
            DropIndex("dbo.Games", new[] { "courtId" });
            DropPrimaryKey("dbo.Games");
            AlterColumn("dbo.Games", "title", c => c.String());
            DropColumn("dbo.Games", "winningTeamId");
            DropColumn("dbo.Games", "teamBId");
            DropColumn("dbo.Games", "teamAId");
            DropColumn("dbo.Games", "courtId");
            DropColumn("dbo.Games", "teamBScore");
            DropColumn("dbo.Games", "teamAScore");
            DropColumn("dbo.Games", "schedule");
            DropColumn("dbo.Games", "gameId");
            DropTable("dbo.TeamPlayers");
            DropTable("dbo.Teams");
            AddColumn("dbo.Games", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Games", "id");
        }
    }
}
