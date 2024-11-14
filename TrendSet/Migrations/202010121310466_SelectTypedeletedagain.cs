namespace TrendSet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SelectTypedeletedagain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TailorDressCategoryMappings", "TailorSelectType_TypeId", "dbo.TailorSelectTypes");
            DropIndex("dbo.TailorDressCategoryMappings", new[] { "TailorSelectType_TypeId" });
            DropColumn("dbo.TailorDressCategoryMappings", "TailorSelectType_TypeId");
            DropTable("dbo.TailorSelectTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TailorSelectTypes",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.TypeId);
            
            AddColumn("dbo.TailorDressCategoryMappings", "TailorSelectType_TypeId", c => c.Int());
            CreateIndex("dbo.TailorDressCategoryMappings", "TailorSelectType_TypeId");
            AddForeignKey("dbo.TailorDressCategoryMappings", "TailorSelectType_TypeId", "dbo.TailorSelectTypes", "TypeId");
        }
    }
}
