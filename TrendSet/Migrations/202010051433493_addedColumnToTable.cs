namespace TrendSet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedColumnToTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TailorDressCategoryMappings", "Cost", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TailorDressCategoryMappings", "Cost");
        }
    }
}
