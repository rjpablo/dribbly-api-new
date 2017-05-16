namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSexFileTypeToStringFromChar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "sex", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "sex");
        }
    }
}
