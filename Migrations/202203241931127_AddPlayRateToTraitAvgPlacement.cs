namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlayRateToTraitAvgPlacement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TraitAvgPlacements", "playRate", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TraitAvgPlacements", "playRate");
        }
    }
}
