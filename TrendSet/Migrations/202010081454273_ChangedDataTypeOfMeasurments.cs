namespace TrendSet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDataTypeOfMeasurments : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "TopLengths", c => c.Double());
            AlterColumn("dbo.Orders", "Neck", c => c.Double());
            AlterColumn("dbo.Orders", "TopWaist", c => c.Double());
            AlterColumn("dbo.Orders", "Chest", c => c.Double());
            AlterColumn("dbo.Orders", "ShoulderLength", c => c.Double());
            AlterColumn("dbo.Orders", "BottomLength", c => c.Double());
            AlterColumn("dbo.Orders", "Hip", c => c.Double());
            AlterColumn("dbo.Orders", "KneeLength", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "KneeLength", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "Hip", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "BottomLength", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "ShoulderLength", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "Chest", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "TopWaist", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "Neck", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "TopLengths", c => c.Double(nullable: false));
        }
    }
}
