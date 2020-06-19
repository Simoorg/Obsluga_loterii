namespace ObslugaLoterii.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedEncryption : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.SuperSecureds");
        }
        
        public override void Down()
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
    }
}
