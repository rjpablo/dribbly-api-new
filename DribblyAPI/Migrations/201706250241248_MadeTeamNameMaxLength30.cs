namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeTeamNameMaxLength30 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teams", "teamName", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teams", "teamName", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
