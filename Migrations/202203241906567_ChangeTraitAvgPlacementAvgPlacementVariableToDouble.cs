namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTraitAvgPlacementAvgPlacementVariableToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TraitAvgPlacements", "AvgPlacement", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TraitAvgPlacements", "AvgPlacement", c => c.Int(nullable: false));
        }
    }
}
