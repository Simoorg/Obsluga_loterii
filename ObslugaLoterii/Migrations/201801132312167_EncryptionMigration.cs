namespace ObslugaLoterii.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EncryptionMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SuperSecureds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TopSecret = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SuperSecureds");
        }
    }
}
