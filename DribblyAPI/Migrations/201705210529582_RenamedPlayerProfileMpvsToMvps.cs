namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedPlayerProfileMpvsToMvps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayerProfiles", "mvps", c => c.Int(nullable: false));
            DropColumn("dbo.PlayerProfiles", "mpvs");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlayerProfiles", "mpvs", c => c.Int(nullable: false));
            DropColumn("dbo.PlayerProfiles", "mvps");
        }
    }
}
