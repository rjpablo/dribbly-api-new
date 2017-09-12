namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedGameEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "isClosed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Games", "isOver", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "isOver");
            DropColumn("dbo.Games", "isClosed");
        }
    }
}
