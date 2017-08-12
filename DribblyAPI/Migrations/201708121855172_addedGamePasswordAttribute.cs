namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedGamePasswordAttribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "password", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "password");
        }
    }
}
