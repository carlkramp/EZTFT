namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemsToChamp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                        isUnique = c.Boolean(nullable: false),
                        isShadow = c.Boolean(nullable: false),
                        placement = c.Int(nullable: false),
                        Champ_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Champs", t => t.Champ_id)
                .Index(t => t.Champ_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Champ_id", "dbo.Champs");
            DropIndex("dbo.Items", new[] { "Champ_id" });
            DropTable("dbo.Items");
        }
    }
}
