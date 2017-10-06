namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedForeignKeyConstraintOnPlayerGame_GameTeam : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.GamePlayers", "gameTeamId");
            AddForeignKey("dbo.GamePlayers", "gameTeamId", "dbo.GameTeams", "gameTeamId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GamePlayers", "gameTeamId", "dbo.GameTeams");
            DropIndex("dbo.GamePlayers", new[] { "gameTeamId" });
        }
    }
}
