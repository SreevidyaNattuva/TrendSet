namespace TrendSet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalmigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "ExpectedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "Courier", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Courier", c => c.String());
            AlterColumn("dbo.Orders", "ExpectedDate", c => c.DateTime());
        }
    }
}
