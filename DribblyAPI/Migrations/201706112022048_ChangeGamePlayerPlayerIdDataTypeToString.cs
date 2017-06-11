namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeGamePlayerPlayerIdDataTypeToString : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.GamePlayers");
            AlterColumn("dbo.GamePlayers", "playerId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.GamePlayers", new[] { "playerId", "gameTeamId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.GamePlayers");
            AlterColumn("dbo.GamePlayers", "playerId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.GamePlayers", new[] { "playerId", "gameTeamId" });
        }
    }
}
