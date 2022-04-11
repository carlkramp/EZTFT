namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUnitDtoIdToChamp : DbMigration
    {
        public override void Up()
        {
                       
            AddColumn("dbo.Champs", "unitDtoId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Champs", "unitDtoId");
           
        }
    }
}
