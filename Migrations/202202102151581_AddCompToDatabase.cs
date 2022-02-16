namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comps",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        placement = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TraitDtoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        num_units = c.Int(nullable: false),
                        style = c.Int(nullable: false),
                        tier_current = c.Int(nullable: false),
                        tier_total = c.Int(nullable: false),
                        Comp_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Comps", t => t.Comp_id)
                .Index(t => t.Comp_id);
            
            CreateTable(
                "dbo.UnitDtoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        character_id = c.String(),
                        chosen = c.String(),
                        name = c.String(),
                        rarity = c.Int(nullable: false),
                        tier = c.Int(nullable: false),
                        Comp_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Comps", t => t.Comp_id)
                .Index(t => t.Comp_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UnitDtoes", "Comp_id", "dbo.Comps");
            DropForeignKey("dbo.TraitDtoes", "Comp_id", "dbo.Comps");
            DropIndex("dbo.UnitDtoes", new[] { "Comp_id" });
            DropIndex("dbo.TraitDtoes", new[] { "Comp_id" });
            DropTable("dbo.UnitDtoes");
            DropTable("dbo.TraitDtoes");
            DropTable("dbo.Comps");
        }
    }
}
