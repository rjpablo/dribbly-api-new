namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedJoinTeamRequestClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JoinTeamRequests",
                c => new
                    {
                        playerId = c.String(nullable: false, maxLength: 128),
                        teamId = c.Int(nullable: false),
                        position = c.Int(),
                        dateRequested = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.playerId, t.teamId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JoinTeamRequests");
        }
    }
}
