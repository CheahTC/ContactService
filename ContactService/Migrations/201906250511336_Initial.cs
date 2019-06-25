namespace ContactService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                        ContactPersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactPersons", t => t.ContactPersonId, cascadeDelete: true)
                .Index(t => t.ContactPersonId);
            
            CreateTable(
                "dbo.ContactPersons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactNumbers", "ContactPersonId", "dbo.ContactPersons");
            DropIndex("dbo.ContactNumbers", new[] { "ContactPersonId" });
            DropTable("dbo.ContactPersons");
            DropTable("dbo.ContactNumbers");
        }
    }
}
