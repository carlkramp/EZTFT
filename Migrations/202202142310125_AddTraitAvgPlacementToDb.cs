namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTraitAvgPlacementToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraitAvgPlacements",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        tier_current = c.Int(nullable: false),
                        TraitMode = c.Int(nullable: false),
                        AvgPlacement = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TraitAvgPlacements");
        }
    }
}
