namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModeToAvgChampPlacementModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AvgChampPlacements", "champMode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AvgChampPlacements", "champMode");
        }
    }
}
