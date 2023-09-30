namespace tryass.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changeannotation2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Annotations", "XrayImage_Id", "dbo.XrayImages");
            DropIndex("dbo.Annotations", new[] { "XrayImage_Id" });
            DropColumn("dbo.Annotations", "XrayImageId");
            RenameColumn(table: "dbo.Annotations", name: "XrayImage_Id", newName: "XrayImageId");
            AlterColumn("dbo.Annotations", "XrayImageId", c => c.Int(nullable: false));
            AlterColumn("dbo.Annotations", "XrayImageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Annotations", "XrayImageId");
            AddForeignKey("dbo.Annotations", "XrayImageId", "dbo.XrayImages", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Annotations", "XrayImageId", "dbo.XrayImages");
            DropIndex("dbo.Annotations", new[] { "XrayImageId" });
            AlterColumn("dbo.Annotations", "XrayImageId", c => c.Int());
            AlterColumn("dbo.Annotations", "XrayImageId", c => c.String());
            RenameColumn(table: "dbo.Annotations", name: "XrayImageId", newName: "XrayImage_Id");
            AddColumn("dbo.Annotations", "XrayImageId", c => c.String());
            CreateIndex("dbo.Annotations", "XrayImage_Id");
            AddForeignKey("dbo.Annotations", "XrayImage_Id", "dbo.XrayImages", "Id");
        }
    }
}
