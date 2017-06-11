namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeSomePropertiesOfPlayerListItemAndFullDetailedTeamOptional : DbMigration
    {
        public override void Up()
        {
            // PlayerListItem and FullDetailedTeams are database views, so no changes needed
            //DropForeignKey("dbo.FullDetailedTeams", "cityId", "dbo.Cities");
            //DropForeignKey("dbo.PlayerListItem", "profilePicId", "dbo.UserPhotos");
            //DropIndex("dbo.FullDetailedTeams", new[] { "cityId" });
            //DropIndex("dbo.PlayerListItem", new[] { "profilePicId" });
            //AlterColumn("dbo.FullDetailedTeams", "cityId", c => c.Int());
            //AlterColumn("dbo.PlayerListItem", "profilePicId", c => c.Int());
            //CreateIndex("dbo.FullDetailedTeams", "cityId");
            //CreateIndex("dbo.PlayerListItem", "profilePicId");
            //AddForeignKey("dbo.FullDetailedTeams", "cityId", "dbo.Cities", "cityId");
            //AddForeignKey("dbo.PlayerListItem", "profilePicId", "dbo.UserPhotos", "id");
        }

        public override void Down()
        {
            // PlayerListItem and FullDetailedTeams are database views, so no changes needed
            //DropForeignKey("dbo.PlayerListItem", "profilePicId", "dbo.UserPhotos");
            //DropForeignKey("dbo.FullDetailedTeams", "cityId", "dbo.Cities");
            //DropIndex("dbo.PlayerListItem", new[] { "profilePicId" });
            //DropIndex("dbo.FullDetailedTeams", new[] { "cityId" });
            //AlterColumn("dbo.PlayerListItem", "profilePicId", c => c.Int(nullable: false));
            //AlterColumn("dbo.FullDetailedTeams", "cityId", c => c.Int(nullable: false));
            //CreateIndex("dbo.PlayerListItem", "profilePicId");
            //CreateIndex("dbo.FullDetailedTeams", "cityId");
            //AddForeignKey("dbo.PlayerListItem", "profilePicId", "dbo.UserPhotos", "id", cascadeDelete: true);
            //AddForeignKey("dbo.FullDetailedTeams", "cityId", "dbo.Cities", "cityId", cascadeDelete: true);
        }
    }
}
