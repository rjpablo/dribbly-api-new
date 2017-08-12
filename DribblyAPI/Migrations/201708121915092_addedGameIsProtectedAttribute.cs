namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedGameIsProtectedAttribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "isProtected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "isProtected");
        }
    }
}
