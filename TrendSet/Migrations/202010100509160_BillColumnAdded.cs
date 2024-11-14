namespace TrendSet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BillColumnAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Bill", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Bill");
        }
    }
}
