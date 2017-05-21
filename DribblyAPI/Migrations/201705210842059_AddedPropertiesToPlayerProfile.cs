namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPropertiesToPlayerProfile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "PlayerProfile_userId", "dbo.PlayerProfiles");
            DropForeignKey("dbo.PlayerProfiles", "userId", "dbo.UserProfiles");
            DropIndex("dbo.Games", new[] { "PlayerProfile_userId" });
            DropIndex("dbo.PlayerProfiles", new[] { "userId" });
            AddColumn("dbo.PlayerProfiles", "dribblingSkills", c => c.Double(nullable: false));
            AddColumn("dbo.PlayerProfiles", "passingSkills", c => c.Double(nullable: false));
            AddColumn("dbo.PlayerProfiles", "threePointsSkills", c => c.Double(nullable: false));
            AddColumn("dbo.PlayerProfiles", "blockingSkills", c => c.Double(nullable: false));
            AddColumn("dbo.PlayerProfiles", "dateCreated", c => c.DateTime(nullable: false));
            DropColumn("dbo.Games", "PlayerProfile_userId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "PlayerProfile_userId", c => c.String(maxLength: 128));
            DropColumn("dbo.PlayerProfiles", "dateCreated");
            DropColumn("dbo.PlayerProfiles", "blockingSkills");
            DropColumn("dbo.PlayerProfiles", "threePointsSkills");
            DropColumn("dbo.PlayerProfiles", "passingSkills");
            DropColumn("dbo.PlayerProfiles", "dribblingSkills");
            CreateIndex("dbo.PlayerProfiles", "userId");
            CreateIndex("dbo.Games", "PlayerProfile_userId");
            AddForeignKey("dbo.PlayerProfiles", "userId", "dbo.UserProfiles", "userId");
            AddForeignKey("dbo.Games", "PlayerProfile_userId", "dbo.PlayerProfiles", "userId");
        }
    }
}
