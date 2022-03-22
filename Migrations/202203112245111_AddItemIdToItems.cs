namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemIdToItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "item_id", c => c.Int(nullable: false));
            DropColumn("dbo.Items", "placement");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "placement", c => c.Int(nullable: false));
            DropColumn("dbo.Items", "item_id");
        }
    }
}
