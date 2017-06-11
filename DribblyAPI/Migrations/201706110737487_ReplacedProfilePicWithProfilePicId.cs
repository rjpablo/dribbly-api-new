namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReplacedProfilePicWithProfilePicId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "profilePicId", c => c.Int());
            CreateIndex("dbo.UserProfiles", "profilePicId");
            AddForeignKey("dbo.UserProfiles", "profilePicId", "dbo.UserPhotos", "id");
            DropColumn("dbo.UserProfiles", "profilePic");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "profilePic", c => c.String());
            DropForeignKey("dbo.UserProfiles", "profilePicId", "dbo.UserPhotos");
            DropIndex("dbo.UserProfiles", new[] { "profilePicId" });
            DropColumn("dbo.UserProfiles", "profilePicId");
        }
    }
}
