namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsReadyPropToGameTeamRequestEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameTeamRequests", "isReady", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameTeamRequests", "isReady");
        }
    }
}
