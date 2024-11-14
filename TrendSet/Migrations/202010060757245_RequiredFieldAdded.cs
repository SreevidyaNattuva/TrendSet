namespace TrendSet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredFieldAdded : DbMigration
    {
        public override void Up()
        {
           
            AlterColumn("dbo.DressTypes", "DressTypeName", c => c.String(nullable: false));
            AlterColumn("dbo.Roles", "RoleName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Roles", "RoleName", c => c.String());
            AlterColumn("dbo.DressTypes", "DressTypeName", c => c.String());
            
        }
    }
}
