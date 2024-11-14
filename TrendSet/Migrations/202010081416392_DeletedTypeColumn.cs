namespace TrendSet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedTypeColumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TailorDressCategoryMappings", "TypeId", "dbo.TailorSelectTypes");
            DropIndex("dbo.TailorDressCategoryMappings", new[] { "TypeId" });
            RenameColumn(table: "dbo.TailorDressCategoryMappings", name: "TypeId", newName: "TailorSelectType_TypeId");
            AlterColumn("dbo.TailorDressCategoryMappings", "TailorSelectType_TypeId", c => c.Int());
            CreateIndex("dbo.TailorDressCategoryMappings", "TailorSelectType_TypeId");
            AddForeignKey("dbo.TailorDressCategoryMappings", "TailorSelectType_TypeId", "dbo.TailorSelectTypes", "TypeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TailorDressCategoryMappings", "TailorSelectType_TypeId", "dbo.TailorSelectTypes");
            DropIndex("dbo.TailorDressCategoryMappings", new[] { "TailorSelectType_TypeId" });
            AlterColumn("dbo.TailorDressCategoryMappings", "TailorSelectType_TypeId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.TailorDressCategoryMappings", name: "TailorSelectType_TypeId", newName: "TypeId");
            CreateIndex("dbo.TailorDressCategoryMappings", "TypeId");
            AddForeignKey("dbo.TailorDressCategoryMappings", "TypeId", "dbo.TailorSelectTypes", "TypeId", cascadeDelete: true);
        }
    }
}
