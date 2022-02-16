namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUnitPlacementToChamp : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UnitPlacements", newName: "Champs");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Champs", newName: "UnitPlacements");
        }
    }
}
