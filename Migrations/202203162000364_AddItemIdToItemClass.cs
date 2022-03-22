namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemIdToItemClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "item_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "item_id");
        }
    }
}
