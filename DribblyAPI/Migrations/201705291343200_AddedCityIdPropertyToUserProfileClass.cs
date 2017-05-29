namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCityIdPropertyToUserProfileClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "cityId", c => c.Int());
            CreateIndex("dbo.UserProfiles", "cityId");
            AddForeignKey("dbo.UserProfiles", "cityId", "dbo.Cities", "cityId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "cityId", "dbo.Cities");
            DropIndex("dbo.UserProfiles", new[] { "cityId" });
            DropColumn("dbo.UserProfiles", "cityId");
        }
    }
}
