namespace tryass.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xImageModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Annotations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        DoctorId = c.Int(nullable: false),
                        XrayImageId = c.Int(nullable: false),
                        Doctor_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Doctor_Id)
                .ForeignKey("dbo.XrayImages", t => t.XrayImageId, cascadeDelete: true)
                .Index(t => t.XrayImageId)
                .Index(t => t.Doctor_Id);
            
            CreateTable(
                "dbo.XrayImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.XrayImages", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Annotations", "XrayImageId", "dbo.XrayImages");
            DropForeignKey("dbo.Annotations", "Doctor_Id", "dbo.AspNetUsers");
            DropIndex("dbo.XrayImages", new[] { "User_Id" });
            DropIndex("dbo.Annotations", new[] { "Doctor_Id" });
            DropIndex("dbo.Annotations", new[] { "XrayImageId" });
            DropTable("dbo.XrayImages");
            DropTable("dbo.Annotations");
        }
    }
}
