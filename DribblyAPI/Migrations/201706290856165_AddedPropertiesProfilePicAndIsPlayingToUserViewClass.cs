namespace DribblyAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPropertiesProfilePicAndIsPlayingToUserViewClass : DbMigration
    {
        public override void Up()
        {

            string query = @"ALTER VIEW UserViews
                AS SELECT        a.Id AS userId, UserName, Email, ISNULL(CONVERT([bit], CASE WHEN 1 = 1 THEN 1 ELSE 0 END), 0) AS isActive,
			                c.fileName AS profilPic, ISNULL(CONVERT([bit],CASE WHEN d.userId IS NOT NULL THEN 1 ELSE 0 END),0) as isPlaying
                FROM            AspNetUsers AS a
	                LEFT JOIN UserProfiles b ON a.Id = b.userId
	                LEFT JOIN UserPhotos c ON b.profilePicId = c.id
	                LEFT JOIN PlayerProfiles d ON b.userId = d.userId";

            Sql(query);
        }
        
        public override void Down()
        {
            string query = @"ALTER VIEW UserViews
                AS SELECT        Id AS userId, UserName, Email, ISNULL(CONVERT([bit], CASE WHEN 1 = 1 THEN 1 ELSE 0 END), 0) AS isActive
                FROM            dbo.AspNetUsers AS a";

            Sql(query);
        }
    }
}
