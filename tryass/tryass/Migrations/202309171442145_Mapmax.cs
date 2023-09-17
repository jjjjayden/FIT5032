namespace tryass.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mapmax : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Map", "Name", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Map", "Description", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Map", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Map", "Name", c => c.String(nullable: false));
        }
    }
}
