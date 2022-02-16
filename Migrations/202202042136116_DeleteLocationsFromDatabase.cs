namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteLocationsFromDatabase : DbMigration
    {
        public override void Up()
        {
            Sql("DROP TABLE Locations");
        }
        
        public override void Down()
        {
        }
    }
}
