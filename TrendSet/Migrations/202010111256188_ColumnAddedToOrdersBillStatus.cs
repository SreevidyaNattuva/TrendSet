namespace TrendSet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnAddedToOrdersBillStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Courier", c => c.String());
            AddColumn("dbo.Orders", "BillStatus", c => c.String());
            AlterColumn("dbo.Orders", "ExpectedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "ExpectedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "BillStatus");
            DropColumn("dbo.Orders", "Courier");
        }
    }
}
