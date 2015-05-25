namespace MagicBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Decks",
                c => new
                    {
                        DeckID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DeckID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Decks");
        }
    }
}
