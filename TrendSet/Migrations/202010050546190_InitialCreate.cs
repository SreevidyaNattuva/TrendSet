namespace TrendSet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.DressTypeCategoryMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        DressTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.DressTypes", t => t.DressTypeId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.DressTypeId);
            
            CreateTable(
                "dbo.DressTypes",
                c => new
                    {
                        DressTypeId = c.Int(nullable: false, identity: true),
                        DressTypeName = c.String(),
                    })
                .PrimaryKey(t => t.DressTypeId);
            
            CreateTable(
                "dbo.RoleLoginMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.UserDetails", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DoB = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        ContactNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleLoginMappings", "UserId", "dbo.UserDetails");
            DropForeignKey("dbo.RoleLoginMappings", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.DressTypeCategoryMappings", "DressTypeId", "dbo.DressTypes");
            DropForeignKey("dbo.DressTypeCategoryMappings", "CategoryId", "dbo.Categories");
            DropIndex("dbo.RoleLoginMappings", new[] { "RoleId" });
            DropIndex("dbo.RoleLoginMappings", new[] { "UserId" });
            DropIndex("dbo.DressTypeCategoryMappings", new[] { "DressTypeId" });
            DropIndex("dbo.DressTypeCategoryMappings", new[] { "CategoryId" });
            DropTable("dbo.UserDetails");
            DropTable("dbo.Roles");
            DropTable("dbo.RoleLoginMappings");
            DropTable("dbo.DressTypes");
            DropTable("dbo.DressTypeCategoryMappings");
            DropTable("dbo.Categories");
        }
    }
}
