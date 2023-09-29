namespace tryass.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIDinto : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Annotations", "XrayImageId", "dbo.XrayImages");
            DropIndex("dbo.Annotations", new[] { "XrayImageId" });
            DropIndex("dbo.Annotations", new[] { "Doctor_Id" });
            DropIndex("dbo.XrayImages", new[] { "User_Id" });
            DropColumn("dbo.Annotations", "DoctorId");
            DropColumn("dbo.XrayImages", "UserId");
            RenameColumn(table: "dbo.Annotations", name: "Doctor_Id", newName: "DoctorId");
            RenameColumn(table: "dbo.XrayImages", name: "User_Id", newName: "UserId");
            AddColumn("dbo.Annotations", "XrayImage_Id", c => c.Int());
            AlterColumn("dbo.Annotations", "DoctorId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Annotations", "XrayImageId", c => c.String());
            AlterColumn("dbo.XrayImages", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Annotations", "DoctorId");
            CreateIndex("dbo.Annotations", "XrayImage_Id");
            CreateIndex("dbo.XrayImages", "UserId");
            AddForeignKey("dbo.Annotations", "XrayImage_Id", "dbo.XrayImages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Annotations", "XrayImage_Id", "dbo.XrayImages");
            DropIndex("dbo.XrayImages", new[] { "UserId" });
            DropIndex("dbo.Annotations", new[] { "XrayImage_Id" });
            DropIndex("dbo.Annotations", new[] { "DoctorId" });
            AlterColumn("dbo.XrayImages", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Annotations", "XrayImageId", c => c.Int(nullable: false));
            AlterColumn("dbo.Annotations", "DoctorId", c => c.Int(nullable: false));
            DropColumn("dbo.Annotations", "XrayImage_Id");
            RenameColumn(table: "dbo.XrayImages", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Annotations", name: "DoctorId", newName: "Doctor_Id");
            AddColumn("dbo.XrayImages", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Annotations", "DoctorId", c => c.Int(nullable: false));
            CreateIndex("dbo.XrayImages", "User_Id");
            CreateIndex("dbo.Annotations", "Doctor_Id");
            CreateIndex("dbo.Annotations", "XrayImageId");
            AddForeignKey("dbo.Annotations", "XrayImageId", "dbo.XrayImages", "Id", cascadeDelete: true);
        }
    }
}
