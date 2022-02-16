namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMatchIdsToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatchIds",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        matchId = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MatchIds");
        }
    }
}
