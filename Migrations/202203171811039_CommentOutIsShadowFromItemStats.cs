namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentOutIsShadowFromItemStats : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ItemStats", "isShadow");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemStats", "isShadow", c => c.Boolean(nullable: false));
        }
    }
}
