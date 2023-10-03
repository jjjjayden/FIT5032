namespace tryass.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maprating : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.Map", t => t.MapId, cascadeDelete: true)
                .Index(t => t.MapId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MapRatings", "MapId", "dbo.Map");
            DropIndex("dbo.MapRatings", new[] { "MapId" });
            DropTable("dbo.MapRatings");
        }
    }
}
