namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMatchIdKeyTomatchId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MatchIds");
            AlterColumn("dbo.MatchIds", "matchId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.MatchIds", "matchId");
            DropColumn("dbo.MatchIds", "id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MatchIds", "id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.MatchIds");
            AlterColumn("dbo.MatchIds", "matchId", c => c.String());
            AddPrimaryKey("dbo.MatchIds", "id");
        }
    }
}
