namespace tryass.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delmaprating : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MapRatings", "MapId", "dbo.Map");
            DropIndex("dbo.MapRatings", new[] { "MapId" });
            DropTable("dbo.MapRatings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MapRatings",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        MapId = c.Int(nullable: false),
                        UserId = c.String(nullable: false),
                        Score = c.Int(nullable: false),
                        Comment = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => t.RatingId);
            
            CreateIndex("dbo.MapRatings", "MapId");
            AddForeignKey("dbo.MapRatings", "MapId", "dbo.Map", "Id", cascadeDelete: true);
        }
    }
}
