namespace ObslugaLoterii.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Klienci",
                c => new
                    {
                        KlientID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.KlientID);
            
            CreateTable(
                "dbo.Kupony",
                c => new
                    {
                        KuponID = c.Int(nullable: false, identity: true),
                        IdKlienta = c.Int(nullable: false),
                        IdLoterii = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KuponID)
                .ForeignKey("dbo.Klienci", t => t.IdKlienta, cascadeDelete: true)
                .ForeignKey("dbo.Loterie", t => t.IdLoterii, cascadeDelete: true)
                .Index(t => t.IdKlienta)
                .Index(t => t.IdLoterii);
            
            CreateTable(
                "dbo.Loterie",
                c => new
                    {
                        LoteriaID = c.Int(nullable: false, identity: true),
                        pula = c.Double(nullable: false),
                        data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LoteriaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kupony", "IdLoterii", "dbo.Loterie");
            DropForeignKey("dbo.Kupony", "IdKlienta", "dbo.Klienci");
            DropIndex("dbo.Kupony", new[] { "IdLoterii" });
            DropIndex("dbo.Kupony", new[] { "IdKlienta" });
            DropTable("dbo.Loterie");
            DropTable("dbo.Kupony");
            DropTable("dbo.Klienci");
        }
    }
}
