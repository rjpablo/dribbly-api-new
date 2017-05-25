namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsActivePropertyToPlayerProfileClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayerProfiles", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlayerProfiles", "isActive");
        }
    }
}
