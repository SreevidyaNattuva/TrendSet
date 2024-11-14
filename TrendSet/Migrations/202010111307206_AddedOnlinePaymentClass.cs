namespace TrendSet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOnlinePaymentClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OnlinePayments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        CardNumber = c.String(),
                        ExpireDate = c.String(),
                        CVV = c.String(),
                    })
                .PrimaryKey(t => t.PaymentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OnlinePayments");
        }
    }
}
