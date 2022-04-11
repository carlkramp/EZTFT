namespace EZTFT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUnitDtoIdToChamp2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UnitItems",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    itemOne = c.Int(),
                    itemTwo = c.Int(),
                    itemThree = c.Int(),
                })
                .PrimaryKey(t => t.id);

            DropTable("dbo.UnitItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UnitItems",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        itemOne = c.Int(),
                        itemTwo = c.Int(),
                        itemThree = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
        }
    }
}
