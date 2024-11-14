namespace TrendSet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Id = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        TopMaterialType = c.Double(nullable: false),
                        TopLengths = c.Double(nullable: false),
                        Neck = c.Double(nullable: false),
                        TopWaist = c.Double(nullable: false),
                        Chest = c.Double(nullable: false),
                        ShoulderLength = c.Double(nullable: false),
                        BottomMaterialType = c.String(),
                        BottomLength = c.Double(nullable: false),
                        Hip = c.Double(nullable: false),
                        KneeLength = c.Double(nullable: false),
                        ExpectedDate = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.TailorDressCategoryMappings", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Id", "dbo.TailorDressCategoryMappings");
            DropIndex("dbo.Orders", new[] { "Id" });
            DropTable("dbo.Orders");
        }
    }
}
