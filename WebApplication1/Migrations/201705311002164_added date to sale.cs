namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddatetosale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "SaleDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "SaleDate");
        }
    }
}
