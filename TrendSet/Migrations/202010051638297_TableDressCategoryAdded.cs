namespace TrendSet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableDressCategoryAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TailorDressCategoryMappings", "UserDetail_UserId", "dbo.UserDetails");
            DropIndex("dbo.TailorDressCategoryMappings", new[] { "UserDetail_UserId" });
            RenameColumn(table: "dbo.TailorDressCategoryMappings", name: "UserDetail_UserId", newName: "UserId");
            AlterColumn("dbo.TailorDressCategoryMappings", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.TailorDressCategoryMappings", "UserId");
            AddForeignKey("dbo.TailorDressCategoryMappings", "UserId", "dbo.UserDetails", "UserId", cascadeDelete: true);
            DropColumn("dbo.TailorDressCategoryMappings", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TailorDressCategoryMappings", "UserName", c => c.String());
            DropForeignKey("dbo.TailorDressCategoryMappings", "UserId", "dbo.UserDetails");
            DropIndex("dbo.TailorDressCategoryMappings", new[] { "UserId" });
            AlterColumn("dbo.TailorDressCategoryMappings", "UserId", c => c.Int());
            RenameColumn(table: "dbo.TailorDressCategoryMappings", name: "UserId", newName: "UserDetail_UserId");
            CreateIndex("dbo.TailorDressCategoryMappings", "UserDetail_UserId");
            AddForeignKey("dbo.TailorDressCategoryMappings", "UserDetail_UserId", "dbo.UserDetails", "UserId");
        }
    }
}
