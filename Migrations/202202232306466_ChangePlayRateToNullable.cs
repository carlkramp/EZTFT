namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePlayRateToNullable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AvgChampPlacements",
                c => new
                {
                    character_id = c.String(nullable: false, maxLength: 128),
                    avgPlacement = c.Double(nullable: false),
                    champMode = c.Int(nullable: false),
                    playRate = c.Double(nullable: true),
                })
                .PrimaryKey(t => t.character_id);

        }

        public override void Down()
        {
            DropTable("dbo.AvgChampPlacements");
        }
    }
}
