namespace tryass.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpSchema : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.AdminBoards");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AdminBoards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DateofBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
