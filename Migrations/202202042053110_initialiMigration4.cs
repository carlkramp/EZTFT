namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialiMigration4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                        country = c.String(),
                        region = c.String(),
                        lat = c.String(),
                        lon = c.String(),
                        timezone_id = c.String(),
                        localtime = c.String(),
                        localtime_epoch = c.Long(nullable: false),
                        utc_offset = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UnitPlacements",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        placement = c.Int(nullable: false),
                        character_id = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UnitPlacements");
            DropTable("dbo.Locations");
        }
    }
}
