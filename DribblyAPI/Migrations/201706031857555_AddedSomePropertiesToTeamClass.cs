namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSomePropertiesToTeamClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "logoUrl", c => c.String());
            AddColumn("dbo.Teams", "dateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Teams", "creatorId", c => c.String(nullable: false));
            AddColumn("dbo.Teams", "managerId", c => c.String(nullable: false));
            AlterColumn("dbo.Teams", "teamName", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teams", "teamName", c => c.String());
            DropColumn("dbo.Teams", "managerId");
            DropColumn("dbo.Teams", "creatorId");
            DropColumn("dbo.Teams", "dateCreated");
            DropColumn("dbo.Teams", "logoUrl");
        }
    }
}
