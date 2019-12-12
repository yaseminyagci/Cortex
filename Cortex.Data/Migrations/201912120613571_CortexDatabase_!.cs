namespace Cortex.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CortexDatabase_ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Personals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        department = c.String(),
                        task = c.String(),
                        password = c.String(),
                        email = c.String(),
                        phone = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Personals");
        }
    }
}
