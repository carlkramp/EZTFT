namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMatchIdIdToInt : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MatchIds");
            AlterColumn("dbo.MatchIds", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.MatchIds", "id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MatchIds");
            AlterColumn("dbo.MatchIds", "id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.MatchIds", "id");
        }
    }
}
