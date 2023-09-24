namespace tryass.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppointmentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MapId = c.Int(nullable: false),
                        dateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Map", t => t.MapId, cascadeDelete: true)
                .Index(t => t.MapId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "MapId", "dbo.Map");
            DropIndex("dbo.Appointments", new[] { "MapId" });
            DropTable("dbo.Appointments");
        }
    }
}
