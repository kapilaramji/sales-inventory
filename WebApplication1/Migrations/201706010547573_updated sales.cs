namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedsales : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "Amount");
        }
    }
}
