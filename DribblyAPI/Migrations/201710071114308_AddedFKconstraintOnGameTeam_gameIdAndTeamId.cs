namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFKconstraintOnGameTeam_gameIdAndTeamId : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.GameTeams", "gameId");
            CreateIndex("dbo.GameTeams", "teamId");
            AddForeignKey("dbo.GameTeams", "gameId", "dbo.Games", "gameId", cascadeDelete: true);
            AddForeignKey("dbo.GameTeams", "teamId", "dbo.Teams", "teamId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameTeams", "teamId", "dbo.Teams");
            DropForeignKey("dbo.GameTeams", "gameId", "dbo.Games");
            DropIndex("dbo.GameTeams", new[] { "teamId" });
            DropIndex("dbo.GameTeams", new[] { "gameId" });
        }
    }
}
