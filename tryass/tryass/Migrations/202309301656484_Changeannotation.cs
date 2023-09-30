namespace tryass.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changeannotation : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Annotations", name: "DoctorId", newName: "UserId");
            RenameIndex(table: "dbo.Annotations", name: "IX_DoctorId", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Annotations", name: "IX_UserId", newName: "IX_DoctorId");
            RenameColumn(table: "dbo.Annotations", name: "UserId", newName: "DoctorId");
        }
    }
}
