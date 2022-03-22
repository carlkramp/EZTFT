namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemsToChamp1 : DbMigration
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
                isUnique = c.Boolean(),
                isShadow = c.Boolean(),
                placement = c.Int(),
                Champ_id = c.Int(),
            })
            .PrimaryKey(t => t.id)
            .ForeignKey("dbo.Champs", t => t.Champ_id)
            .Index(t => t.Champ_id);

            //AddColumn("dbo.Items", "Champ_id", c => c.Int());
            //CreateIndex("dbo.Items", "Champ_id");
            //AddForeignKey("dbo.Items", "Champ_id", "dbo.Champs", "id");
        }
        
        public override void Down()
        {
            DropTable("dbo.Items");
            DropForeignKey("dbo.Items", "Champ_id", "dbo.Champs");
            DropIndex("dbo.Items", new[] { "Champ_id" });
            DropColumn("dbo.Items", "Champ_id");
        }
    }
}
