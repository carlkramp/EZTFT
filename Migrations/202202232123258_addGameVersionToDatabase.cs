namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGameVersionToDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MatchIds", "gameVersion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MatchIds", "gameVersion");
        }
    }
}
