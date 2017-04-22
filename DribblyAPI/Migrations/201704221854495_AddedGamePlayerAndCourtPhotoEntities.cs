namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGamePlayerAndCourtPhotoEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourtPhotos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fileName = c.String(),
                        courtId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Courts", t => t.courtId, cascadeDelete: true)
                .Index(t => t.courtId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourtPhotos", "courtId", "dbo.Courts");
            DropIndex("dbo.CourtPhotos", new[] { "courtId" });
            DropTable("dbo.Games");
            DropTable("dbo.CourtPhotos");
        }
    }
}
