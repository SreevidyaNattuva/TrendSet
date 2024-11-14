namespace TrendSet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnlinePaymentMadeMandatory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OnlinePayments", "CardNumber", c => c.String(nullable: false));
            AlterColumn("dbo.OnlinePayments", "ExpireDate", c => c.String(nullable: false));
            AlterColumn("dbo.OnlinePayments", "CVV", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OnlinePayments", "CVV", c => c.String());
            AlterColumn("dbo.OnlinePayments", "ExpireDate", c => c.String());
            AlterColumn("dbo.OnlinePayments", "CardNumber", c => c.String());
        }
    }
}
