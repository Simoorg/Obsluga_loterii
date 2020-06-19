namespace ObslugaLoterii.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctedMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Klienci", "nrDowodu", c => c.String());
            AddColumn("dbo.Klienci", "eMail", c => c.String());
            AddColumn("dbo.Klienci", "imie", c => c.String());
            AddColumn("dbo.Klienci", "nazwisko", c => c.String());
            AddColumn("dbo.Kupony", "wygrana", c => c.Double(nullable: false));
            AddColumn("dbo.Kupony", "czyWyplacono", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kupony", "czyWyplacono");
            DropColumn("dbo.Kupony", "wygrana");
            DropColumn("dbo.Klienci", "nazwisko");
            DropColumn("dbo.Klienci", "imie");
            DropColumn("dbo.Klienci", "eMail");
            DropColumn("dbo.Klienci", "nrDowodu");
        }
    }
}
