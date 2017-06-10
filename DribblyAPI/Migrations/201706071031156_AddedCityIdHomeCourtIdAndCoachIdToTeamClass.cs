namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCityIdHomeCourtIdAndCoachIdToTeamClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "cityId", c => c.Int(nullable: false));
            AddColumn("dbo.Teams", "homeCourtId", c => c.Int());
            AddColumn("dbo.Teams", "coachId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "coachId");
            DropColumn("dbo.Teams", "homeCourtId");
            DropColumn("dbo.Teams", "cityId");
        }
    }
}
