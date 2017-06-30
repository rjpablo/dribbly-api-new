namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedJoinTeamInvitationClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JoinTeamInvitations",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        teamId = c.Int(nullable: false),
                        playerId = c.String(),
                        position = c.Int(),
                        dateInvited = c.DateTime(nullable: false),
                        response = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JoinTeamInvitations");
        }
    }
}
