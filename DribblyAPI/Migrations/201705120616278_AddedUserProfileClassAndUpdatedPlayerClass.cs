namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserProfileClassAndUpdatedPlayerClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        userId = c.String(nullable: false, maxLength: 128),
                        profilePic = c.String(),
                        address = c.String(),
                        addressLat = c.Double(nullable: false),
                        addressLng = c.Double(nullable: false),
                        heightFt = c.Int(nullable: false),
                        heightIn = c.Int(nullable: false),
                        dateJoined = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.userId);
            
            CreateTable(
                "dbo.PlayerProfiles",
                c => new
                    {
                        userId = c.String(nullable: false, maxLength: 128),
                        mpvs = c.Int(nullable: false),
                        winRate = c.Double(nullable: false),
                        rating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.userId)
                .ForeignKey("dbo.UserProfiles", t => t.userId)
                .Index(t => t.userId);
            
            AddColumn("dbo.Games", "PlayerProfile_userId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Games", "PlayerProfile_userId");
            AddForeignKey("dbo.Games", "PlayerProfile_userId", "dbo.PlayerProfiles", "userId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerProfiles", "userId", "dbo.UserProfiles");
            DropForeignKey("dbo.Games", "PlayerProfile_userId", "dbo.PlayerProfiles");
            DropIndex("dbo.PlayerProfiles", new[] { "userId" });
            DropIndex("dbo.Games", new[] { "PlayerProfile_userId" });
            DropColumn("dbo.Games", "PlayerProfile_userId");
            DropTable("dbo.PlayerProfiles");
            DropTable("dbo.UserProfiles");
        }
    }
}
