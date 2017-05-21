namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOwnerPropertyToCourt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courts", "userId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Courts", "userId");
            AddForeignKey("dbo.Courts", "userId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courts", "userId", "dbo.AspNetUsers");
            DropIndex("dbo.Courts", new[] { "userId" });
            AlterColumn("dbo.Courts", "userId", c => c.String());
        }
    }
}
