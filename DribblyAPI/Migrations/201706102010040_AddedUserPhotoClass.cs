namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserPhotoClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPhotos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fileName = c.String(),
                        userId = c.String(),
                        uploadDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserPhotos");
        }
    }
}
