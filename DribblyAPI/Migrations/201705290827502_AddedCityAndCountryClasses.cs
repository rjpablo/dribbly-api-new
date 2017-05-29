namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCityAndCountryClasses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        cityId = c.Int(nullable: false, identity: true),
                        shortName = c.String(),
                        longName = c.String(),
                        countryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.cityId)
                .ForeignKey("dbo.Countries", t => t.countryId, cascadeDelete: true)
                .Index(t => t.countryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        countryId = c.Int(nullable: false, identity: true),
                        shortName = c.String(),
                        longName = c.String(),
                    })
                .PrimaryKey(t => t.countryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "countryId", "dbo.Countries");
            DropIndex("dbo.Cities", new[] { "countryId" });
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
