namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAvgItemPlacementToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AvgItemPlacements",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        avgPlacement = c.Double(nullable: false),
                        itemMode = c.Int(nullable: false),
                        playRate = c.Double(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AvgItemPlacements");
        }
    }
}
