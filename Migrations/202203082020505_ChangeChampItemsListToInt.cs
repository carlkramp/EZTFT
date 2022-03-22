namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeChampItemsListToInt : DbMigration
    {
        public override void Up()
        {

            DropForeignKey("dbo.Items", "Champ_id", "dbo.Champs");
            DropIndex("dbo.Items", new[] { "Champ_id" });
      
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.id);
            
            CreateIndex("dbo.Items", "Champ_id");
            AddForeignKey("dbo.Items", "Champ_id", "dbo.Champs", "id");
        }
    }
}
