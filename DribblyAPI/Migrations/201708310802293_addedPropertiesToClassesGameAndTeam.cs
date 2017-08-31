namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPropertiesToClassesGameAndTeam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "allowedToJoinTeamA", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "allowedToJoinTeamB", c => c.Int(nullable: false));
            AddColumn("dbo.Teams", "maxPlayers", c => c.Int(nullable: false));
            AddColumn("dbo.Teams", "password", c => c.String());
            AddColumn("dbo.Teams", "requiresPassword", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "requiresPassword");
            DropColumn("dbo.Teams", "password");
            DropColumn("dbo.Teams", "maxPlayers");
            DropColumn("dbo.Games", "allowedToJoinTeamB");
            DropColumn("dbo.Games", "allowedToJoinTeamA");
        }
    }
}
