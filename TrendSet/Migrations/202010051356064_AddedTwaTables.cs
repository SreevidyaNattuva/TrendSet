namespace TrendSet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTwaTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TailorDressCategoryMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        DressTypeId = c.Int(nullable: false),
                        UserName = c.String(),
                        TypeId = c.Int(nullable: false),
                        UserDetail_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.DressTypes", t => t.DressTypeId, cascadeDelete: true)
                .ForeignKey("dbo.TailorSelectTypes", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.UserDetails", t => t.UserDetail_UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.DressTypeId)
                .Index(t => t.TypeId)
                .Index(t => t.UserDetail_UserId);
            
            CreateTable(
                "dbo.TailorSelectTypes",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.TypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TailorDressCategoryMappings", "UserDetail_UserId", "dbo.UserDetails");
            DropForeignKey("dbo.TailorDressCategoryMappings", "TypeId", "dbo.TailorSelectTypes");
            DropForeignKey("dbo.TailorDressCategoryMappings", "DressTypeId", "dbo.DressTypes");
            DropForeignKey("dbo.TailorDressCategoryMappings", "CategoryId", "dbo.Categories");
            DropIndex("dbo.TailorDressCategoryMappings", new[] { "UserDetail_UserId" });
            DropIndex("dbo.TailorDressCategoryMappings", new[] { "TypeId" });
            DropIndex("dbo.TailorDressCategoryMappings", new[] { "DressTypeId" });
            DropIndex("dbo.TailorDressCategoryMappings", new[] { "CategoryId" });
            DropTable("dbo.TailorSelectTypes");
            DropTable("dbo.TailorDressCategoryMappings");
        }
    }
}
