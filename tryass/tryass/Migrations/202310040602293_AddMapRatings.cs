namespace tryass.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMapRatings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MapRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        Comment = c.String(),
                        MapId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Map", t => t.MapId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.MapId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MapRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MapRatings", "MapId", "dbo.Map");
            DropIndex("dbo.MapRatings", new[] { "UserId" });
            DropIndex("dbo.MapRatings", new[] { "MapId" });
            DropTable("dbo.MapRatings");
        }
    }
}
