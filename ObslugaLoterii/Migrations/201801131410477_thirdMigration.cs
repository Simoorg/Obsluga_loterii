namespace ObslugaLoterii.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thirdMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kupony", "TypyToStr", c => c.String());
            AddColumn("dbo.Loterie", "WylosowaneToStr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Loterie", "WylosowaneToStr");
            DropColumn("dbo.Kupony", "TypyToStr");
        }
    }
}
