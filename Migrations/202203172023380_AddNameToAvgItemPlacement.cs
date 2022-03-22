namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameToAvgItemPlacement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AvgItemPlacements", "name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AvgItemPlacements", "name");
        }
    }
}
