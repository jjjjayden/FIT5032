namespace tryass.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailtemplate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailTemplates",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmailTemplates");
        }
    }
}
