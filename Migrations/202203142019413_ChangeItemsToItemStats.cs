namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeItemsToItemStats : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Items", newName: "ItemStats");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ItemStats", newName: "Items");
        }
    }
}
