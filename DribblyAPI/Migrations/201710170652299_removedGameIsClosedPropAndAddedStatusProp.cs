namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedGameIsClosedPropAndAddedStatusProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "status", c => c.Int(nullable: false));
            DropColumn("dbo.Games", "isClosed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "isClosed", c => c.Boolean(nullable: false));
            DropColumn("dbo.Games", "status");
        }
    }
}
